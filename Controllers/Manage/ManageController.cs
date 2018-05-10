using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SeniorProject.Models;
using SeniorProject.Models.ManageViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SeniorProject.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly ILogger _logger;
        private readonly UrlEncoder _urlEncoder;
        private readonly SiteContext _context;

        private const string AuthenicatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public ManageController(
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

        [TempData]
        public string StatusMessage { get; set; }

        #region Account Settings Stuff
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);
            foreach (string s in roles)
            {
                if (s.Equals("Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
            }

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var member = await _context.Members.SingleOrDefaultAsync(m => m.User == user);
            if (member == null)
            {
                member = new Member { Name = "", User = user };
                _context.Add(member);
                await _context.SaveChangesAsync();

                var fav = new Collection { Owner = member, Name = "Favorites" };
                _context.Add(fav);
                await _context.SaveChangesAsync();
            }

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                Name = member.Name,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

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
            var name = member.Name;
            if(model.Name != name)
            {
                member.Name = model.Name;
                _context.Update(member);
                await _context.SaveChangesAsync();
            }

            var email = user.Email;
            if (model.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            StatusMessage = "Your profile has been updated";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult DeleteAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDeleteAccount()
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


            _context.Remove(member);
            await _context.SaveChangesAsync();
            await _signInManager.SignOutAsync();
            await _userManager.DeleteAsync(user);

            _logger.LogInformation("User Deleted.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCancel()
        {
            return RedirectToAction(nameof(Index));
        }

        #endregion

        public IActionResult ViewRecipe(int id)
        {
            int _id = id;
            return RedirectToAction(nameof(RecipeController.Display), "Recipe", new { id = _id});
        }

        #region Inventory Stuff
        public async Task<IActionResult> Inventory()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var member = await _context.Members.SingleOrDefaultAsync(m => m.User == user);

            var inventory = await _context.MemberIngredients.Where(mi => mi.Owner == member).Include(mi=> mi.Element).ToListAsync();

            List<Ingredient> ingredients = new List<Ingredient>();

            foreach (MemberIngredient mi in inventory)
            {
                ingredients.Add(mi.Element);
            }

            return View(ingredients);
        }

        public async Task<IActionResult> AddIngredient(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var member = await _context.Members.SingleOrDefaultAsync(m => m.User == user);
            
            if (id == null)
            {
                var inventory = await _context.MemberIngredients.Where(mi => mi.Owner == member).Include(mi => mi.Element).ToListAsync();
                var whole = await _context.Ingredients.ToListAsync();

                List<Ingredient> ingredients = new List<Ingredient>();

                foreach (MemberIngredient mi in inventory)
                {
                    if (whole.Contains(mi.Element)) whole.Remove(mi.Element);
                }

                return View(whole);
            }
            else
            {
                var ingredient = await _context.Ingredients.SingleOrDefaultAsync(i => i.ID == id);

                MemberIngredient addee = new MemberIngredient { Owner = member, Element = ingredient };
                _context.Add(addee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Inventory), new { });
            }
        }

        public async Task<IActionResult> RemoveIngredient(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var member = await _context.Members.SingleOrDefaultAsync(m => m.User == user);

            var inventory_element = await _context.MemberIngredients.SingleOrDefaultAsync(mi => mi.Owner == member && mi.Element.ID == id);

            _context.Remove(inventory_element);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Inventory));
        }
        #endregion

        #region Collection Stuff
        public async Task<IActionResult> Collections()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var member = await _context.Members.SingleOrDefaultAsync(m => m.User == user);

            var collections = await _context.Collections.Where(c => c.Owner == member).ToListAsync();

            List<CollectionViewModel> cvms = new List<CollectionViewModel>();

            int count;

            foreach (Collection c in collections)
            {
                count = 0;
                var connections = await _context.CollectionRecipes.Where(cr => cr.Collection == c).ToListAsync();
                foreach (CollectionRecipe cr in connections)
                {
                    count++;
                }
                cvms.Add(new CollectionViewModel { ID = c.ID, Name = c.Name, Count = count });
            }

            return View(cvms);
        }

        public async Task<IActionResult> ViewCollection(int id)
        {
            var collection = await _context.Collections.SingleOrDefaultAsync(c => c.ID == id);
            var connections = await _context.CollectionRecipes.Where(cr => cr.Collection == collection).Include(cr => cr.Recipe).ToListAsync();

            ViewCollectionViewModel vcvm = new ViewCollectionViewModel { Collection = collection, Rows = new List<Row>() };

            int count = 0;
            Row r = new Row();

            foreach(CollectionRecipe cr in connections)
            {
                switch (count)
                {
                    case 0:
                        r.r1 = cr.Recipe;
                        count++;
                        break;
                    case 1:
                        r.r2 = cr.Recipe;
                        count++;
                        break;
                    case 2:
                        r.r3 = cr.Recipe;
                        count++;
                        break;
                    case 3:
                        r.r4 = cr.Recipe;
                        count++;
                        break;
                    case 4:
                        vcvm.Rows.Add(r);
                        count = 0;
                        r = new Row();
                        break;
                }
            }

            if(count != 0) vcvm.Rows.Add(r);

            return View(vcvm);
        }

        public ActionResult CreateCollection(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCollection(CreateCollectionViewModel ccvm)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var member = await _context.Members.SingleOrDefaultAsync(m => m.User == user);

            Collection c = new Collection { Name = ccvm.Name, Owner = member };
            _context.Add(c);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Collections));
        }

        public async Task<IActionResult> RemoveCollection(int id)
        {
            var collection = await _context.Collections.SingleOrDefaultAsync(c => c.ID == id);

            _context.Remove(collection);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Collections));
        }
        #endregion

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        #endregion
    }
}
