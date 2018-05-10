using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Models.RecipeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SeniorProject.Controllers
{
    public class RecipeController : Controller
    {

        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly ILogger _logger;
        private readonly UrlEncoder _urlEncoder;
        private readonly SiteContext _context;

        private const string AuthenicatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public RecipeController(
          UserManager<Users> userManager,
          SignInManager<Users> signInManager,
          ILogger<ManageController> logger,
          UrlEncoder urlEncoder,
          SiteContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _urlEncoder = urlEncoder;
            _context = context;
        }
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Search));
        }

        public async Task<IActionResult> Display(int id)
        {
            var recipe = await _context.Recipes.SingleOrDefaultAsync(r => r.ID == id);

            var mainAmounts = await _context.Amounts.Where(a => a.Parent == recipe && a.Core == true).Include(a => a.Child).Include(a => a.Parent).ToListAsync();
            var variations = await _context.Amounts.Where(a => a.Parent == recipe && a.Core == false).Include(a => a.Child).Include(a => a.Parent).ToListAsync();

            DisplayViewModel dvm = new DisplayViewModel { Amounts = mainAmounts, Variations = variations, Recipe = recipe };


            return View(dvm);
        }

        #region Functions to Add Recipe To a Collection

        [Authorize]
        public async Task<IActionResult> AddToCollection(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var member = await _context.Members.SingleOrDefaultAsync(m => m.User == user);

            if (member == null)
            {
                throw new ApplicationException("Failed to find member");
            }
            var recipe = await _context.Recipes.SingleOrDefaultAsync(r => r.ID == id);
            var collections = await _context.Collections.Where(c => c.Owner == member).ToListAsync();

            AddToCollectionViewModel atcvm = new AddToCollectionViewModel { Recipe = recipe, Collections = collections };

            return View(atcvm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToCollection(AddToCollectionViewModel atcvm)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var member = await _context.Members.SingleOrDefaultAsync(m => m.User == user);

            if (member == null)
            {
                throw new ApplicationException("Failed to find member");
            }
            
            CollectionRecipe cr = new CollectionRecipe { CollectionID = atcvm.ChoiceID, RecipeID = atcvm.RecipeID };

            _context.Add(cr);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Display), new { id = atcvm.RecipeID});
        }
        #endregion

        #region Search Functions
        public async Task<IActionResult> Search(string parameter = "", int off = -1)
        {
            var user = await _userManager.GetUserAsync(User);
            if(user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                foreach (string s in roles)
                {
                    if (s.Equals("Admin"))
                    {
                        return RedirectToAction(nameof(List));
                    }
                }
            }            

            if (off != -1) return RedirectToAction(nameof(MemberSearch), new { parameter = parameter, off = off });
            if (parameter == null) parameter = "";
            var recipes = await _context.Recipes.Where(r => r.Name.ToLower().Contains(parameter.ToLower())).ToListAsync();

            List<Amount> amounts = new List<Amount>();
            foreach(Recipe r in recipes)
            {
                var temp = await _context.Amounts.Include(a => a.Child).Include(a => a.Parent).Where(a => a.Parent == r).ToListAsync();
                foreach(Amount a in temp)
                {
                    amounts.Add(a);
                }
            } 
            SearchViewModel svm = new SearchViewModel { Recipes = recipes, Amounts = amounts, Parameter = parameter, Off = off };

            return View(svm);
        }

        [Authorize]
        public async Task<IActionResult> MemberSearch(string parameter = "", int off = -1)
        {
            if (off == -1)
            {
                string _parameter = parameter;
                return RedirectToAction(nameof(Search), new { parameter = _parameter });
            }
            
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            foreach (string s in roles)
            {
                if (s.Equals("Admin"))
                {
                    return RedirectToAction(nameof(List));
                }
            }

            var member = await _context.Members.SingleOrDefaultAsync(m => m.User == user);

            if (member == null)
            {
                throw new ApplicationException("Failed to find member");
            }

            var temp = await _context.MemberIngredients.Include(mi => mi.Owner).Include(mi => mi.Element).Where(mi => mi.Owner == member).ToListAsync();

            List<Ingredient> inventory = new List<Ingredient>();

            foreach(MemberIngredient mi in temp)
            {
                inventory.Add(mi.Element);
            }

            var recipesTemp = await _context.Recipes.Where(r => r.Name.ToLower().Contains(parameter.ToLower())).ToListAsync();

            List<Recipe> recipes = new List<Recipe>();

            foreach (Recipe r in recipesTemp)
            {
                recipes.Add(r);
            }
           
            foreach (Recipe r in recipesTemp)
            {
                int count = 0;
                var temp2 = await _context.Amounts.Include(a => a.Child).Include(a => a.Parent).Where(a => a.Parent == r && a.Core == true).ToListAsync();
                foreach (Amount a in temp2)
                {                    
                    if (!inventory.Contains(a.Child)) count++;

                    if (count > off)
                    {
                        recipes.Remove(r);
                        break;
                    }
                }                
            }

            List<Amount> amounts = new List<Amount>();
            foreach (Recipe r in recipes)
            {
                var temp2 = await _context.Amounts.Include(a => a.Child).Include(a => a.Parent).Where(a => a.Parent == r && a.Core == true).ToListAsync();
                foreach (Amount a in temp2)
                {
                    amounts.Add(a);
                }
            }

            SearchViewModel svm = new SearchViewModel { Recipes = recipes, Amounts = amounts, Parameter = parameter, Off = off };

            return View("Search", svm);
        }
        #endregion

        #region Admin Functions

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Instructions, Notes, History, ImagePath, VideoLink")] Models.Recipe recipe)
        {
            if (recipe.History == null) recipe.History = "";
            if (recipe.ImagePath == null) recipe.ImagePath = "~/Media/Recipes/default.jpg";
            else recipe.ImagePath = "~/Media/Recipes/" + recipe.ImagePath;
            if(recipe.VideoLink == null) recipe.VideoLink = "";

            if (ModelState.IsValid)
            {
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }

            return View(recipe);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {
            var recipes = await _context.Recipes.ToListAsync();
            var amounts = await _context.Amounts.Include(a => a.Child).Include(a => a.Parent).Where(a => a.Core == true).ToListAsync();

            List<ListViewModel> lvmList = new List<ListViewModel>();
            
            foreach(Recipe r in recipes)
            {
                lvmList.Add(new ListViewModel { Recipe = r, Amounts = amounts });
            }

            return View(lvmList);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _context.Recipes.SingleOrDefaultAsync(r => r.ID == id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("ID, Name, Instructions, Notes, History, ImagePath, VideoLink")] Models.Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Recipes.Any(r => r.ID == recipe.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List));
            }

            return View(recipe);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _context.Recipes.SingleOrDefaultAsync(r => r.ID == id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipes.SingleOrDefaultAsync(r => r.ID == id);

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return  RedirectToAction(nameof(List));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddIngredient(int id)
        {
            var recipe = await _context.Recipes.SingleOrDefaultAsync(r => r.ID == id);
            var ingredientsAll = await _context.Ingredients.ToListAsync();
            var amounts = await _context.Amounts.Where(a => a.RecipeID == id).Include(a => a.Child).Include(a => a.Parent).ToListAsync();

            List<Ingredient> ingredients = new List<Ingredient>();

            foreach (Ingredient i in ingredientsAll)
            {
                ingredients.Add(i);
            }

            foreach (Ingredient i in ingredientsAll)
            {
                foreach(Amount a in amounts)
                {
                    if (a.Child == i) ingredients.Remove(i); 
                }
            }

            AddIngredientViewModel aivm = new AddIngredientViewModel { Recipe = recipe, Ingredients = ingredients};

            return View(aivm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddIngredient(AddIngredientViewModel aivm)
        {
            Amount a = new Amount { Ounces = aivm.Amount, RecipeID = aivm.RecipeID, IngredientID = aivm.IngredientID, Core = true };

            _context.Add(a);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(List));
        }

        #endregion
    }
}