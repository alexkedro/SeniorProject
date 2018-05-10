using SeniorProject.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SeniorProject.Data
{
    public class DbInitializer
    {

        // Seed database
        public static async Task Initialize(SiteContext context, UserManager<Users> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            if (!context.Recipes.Any())
            {
                var recipes = new Recipe[]
                {
                    new Recipe
                    {
                        ID = 7,
                        Name = "Swimming Pool",
                        Notes = "<p>When making a Swimming Pool, you will probably need</p><ul><li>Electric blender</li><li>Measuring glass or jigger</li><li>Straws</li></ul>",
                        ImagePath = "~/Media/Recipes/swimming-pool.jpg",
                        History = "The Swimming Pool Cocktail was invented 1979 by Charles Schumann in Munich.",
                        Instructions = "<p> To mix a Swimming Pool Cocktail use the following instructions. </p><ol><li>Chill a hurricane glass with ice and pour it out when you are ready to pour the ingredients in the glass.</li><li>Pour pineapple juice, vodka, cream and cream of coconut with ice into a blender.</li><li>Blend until smooth.</li><li>Pour into a Hurricane glass.</li><li>Float the blue curacao on top and serve it.</li><li>Add a straw and garish with a slice of pineapple and a cherry.</li></ol><p>Enjoy your Swimming Pool!</p>",
                        VideoLink = "https://www.youtube.com/embed/qgo0HBhLNQE"
                    },
                    new Recipe
                    {
                        ID = 1,
                        Name = "Screaming Orgasm",
                        Notes = "<p>When making a creaming Orgasm, you will probably need</p><ul><li>Bar Spoon</li><li>Measuring glass or jigger</li></ul>",
                        ImagePath = "~/Media/Recipes/screaming-orgasm.jpg",
                        History = "",
                        Instructions = "<p>To make a Screaming Orgasm cocktail, use the following recipe instructions. You're checking out this cocktail because you love vodka and Kahlua, and not because of the name, right?</p><ol><li>Pour vodka, Bailey's and Kahlua into a shaker with ice.*</li><li>Shake well, strain into a cocktail glass, and serve.</li></ol><p>Be sure to use a higher quality vodka, to ensure it mixes well with the Bailey's.</p><p>You can experiment with a chocolate sauce squeeze bottle to decorate the glass before you pour the ingredients into the glass, for an extra touch of flair and flavor. </p><p>Enjoy your Screaming Orgasm cocktail!</p>",
                        VideoLink = "https://www.youtube.com/embed/HAXUxBmoXTs"
                    },
                    new Recipe
                    {
                        ID = 2,
                        Name = "Cosmopolitan",
                        Notes = "<p>When making a Cosmopolitan, you will probably need</p><ul><li>Cocktail Shaker</li><li>Picks for Garnishes</li><li>Saucer for Salt or Sugar</li><li>Cocktail Strainer</li></ul>",
                        ImagePath = "~/Media/Recipes/cosmopolitan.jpg",
                        History = "The history of the Cosmopolitan is disputed. It's believed the Cosmopolitan has it's roots in a cocktail with the same name in the book, \"Pioneers of Mixing at Elite Bars\" published in 1934, with slightly different ingredients: gin, Cointreau, lemon juice and raspberry syrup. There are many other stories of creation, with a recipe closer to the modern Cosmopolitan, throughout the 1970s and 1980s",
                        Instructions = "<p>There are several ways to make a Cosmopolitan, also known as a Cosmo, here is one of our favorite recipes. </p><p>You will want to have your ingredients, accessories and garnishes ready.</p><ol><li>Chill a cocktail glass with ice and pour it out when you are ready to pour the ingredients in the glass.</li><li>Pour the cointreau, cranberry juice, lime juice, and lemon vodka into a cocktail shaker filled with ice.</li><li>Shake well, and strain in large, cocktail glass.</li><li>Garnish with lemon slice or lime wedge.</li></ol><p>Optional: chilling (or frosting) the glass, and/or adding sugar to the rim of the glass, can have a nice effect. </p><p>Enjoy your Cosmopolitan cocktail!</p>",
                        VideoLink = "https://www.youtube.com/embed/lgImZafO9g8"
                    },
                    new Recipe
                    {
                        ID = 3,
                        Name = "Almond Joy Martini",
                        Notes = "<p>When making an Almond Joy Martini, you will probably need</p><ul><li>Cocktail Shaker</li><li>Measuring glass or jigger</li><li>Cocktail Strainer</li></ul>",
                        ImagePath = "~/Media/Recipes/almond-joy-martini.jpg",
                        History = "",
                        Instructions = "<p>Also known as a Almond Joy Cocktail, while not technically a martini, is delicious dessert style cocktail. To make an Almond Joy Martini follow these simple instructions.</p> <ol> <li>Pour the creme de cacao and coconut rum into a shaker with ice, including a generous splash of Frangelico, and shake well.</li><li>Strain into a chilled martini glass and serve.</li><li>Garnish with shaved coconut if desired.</li> </ol> <p>As an optional touch, you can decorate the inside of the glass with chocolate syrup before you gently pour the drink in the glass. <br> <br>Enjoy your Almond Joy Cocktail!</p>",
                        VideoLink = "https://www.youtube.com/embed/CKfJJ7X5tW0"
                    },
                    new Recipe
                    {
                        ID = 4,
                        Name = "Blue Hawaiian",
                        Notes = "<p>When making a Blue Hawaiian, you will probably need</p><ul><li>Electric Blender</li><li>Measuring glass or jigger</li></ul>",
                        ImagePath = "~/Media/Recipes/blue-hawaiian.jpg",
                        History = "Per wikipedia: \"The Blue Hawaii was invented in 1957 by Harry Yee, legendary head bartender of the Hilton Hawaiian Village (formerly the Kaiser Hawaiian Village) in Waikiki, Hawaii when a sales representative of Dutch distiller Bols asked him to design a drink that featured their blue color of Curaçao liqueur. After experimenting with several variations he settled on a version somewhat different than the most popular version today, but with the signature blue color, pineapple wedge, and cocktail umbrella.\"",
                        Instructions = " <p>There are very few people who don't like the Blue Hawaiian, it's a vacation in liquid form. This festive blue cocktail recipe will help you get in the vacation mood! A Blue Hawaii is typically served on the rocks, but here is one of our favorite Blue Hawaiian recipes pureed in a blender. We like our vacation drinks smooth.</p> <ol> <li>Chill a hurricane or highball glass with ice and pour it out when you are ready to pour the ingredients in the glass.</li><li>Combine rum, blue Curacao, pineapple juice, cream of coconut, and 1 cup crushed ice in blender.</li><li>Puree on high speed until smooth.</li><li>Pour into your chilled glass.</li><li>Garnish with a slice of pineapple and a maraschino cherry.</li> </ol> <p>Enjoy your Blue Hawaiian and start dreaming of beaches and Elvis.</p>",
                        VideoLink = "https://www.youtube.com/embed/PJ_hAUtuI8c"
                    },
                    new Recipe
                    {
                        ID = 5,
                        Name = "Blue Lady",
                        Notes = "<p>When making a Blue Lady, you will probably need</p><ul><li>Cocktail Shaker</li><li>Picks for Garnishes</li><li>Saucer for Salt or Sugar</li><li>Cocktail Strainer</li></ul>",
                        ImagePath = "~/Media/Recipes/blue-lady.jpg",
                        History = "The original Lady was white, with a mix of creme de menthe and Curacao. In 1929, Harry MacElhone replaced the mint with gin and created a very popular cocktail. ",
                        Instructions = "<p>Here's our favorite version of a Blue Lady cocktail. You will want to have your ingredients and accessories ready.</p> <ol> <li>Chill the glass with fresh ice and pour it out when ready to pour the ingredients in the glass.</li><li>Pour the gin, lemon juice and blue curaçao in a shaker with ice.*</li><li>Shake well, and strain in chilled cocktail glass.</li><li>We recommend adding sugar to the rim of the glass.</li> </ol> <p>Enjoy your Blue Lady!</p>",
                        VideoLink = "https://www.youtube.com/embed/EJdFv8KFLOE"
                    },
                    new Recipe
                    {
                        ID = 6,
                        Name = "Bubble Gum Shot",
                        Notes = "<p>When making a Bubble Gum Shot, you will probably need</p><ul><li>Cocktail Shaker</li><li>Measuring Glass or Jigger</li><li>Speed Pourers</li><li>Cocktail Strainer</li></ul>",
                        ImagePath = "~/Media/Recipes/default.jpg",
                        History = "",
                        Instructions = "<p>To make a Bubble Gum shooter:</p> <ol> <li>Pour the vodka, creme de bananes, peach schnapps, and orange juice into a shaker filled with ice.</li><li>Shake well.</li><li>Pour into 2 shot glasses.</li> </ol> <p>Enjoy your Bubble Gum shot!</p>",
                        VideoLink = ""
                    }

                };

                foreach (Recipe r in recipes)
                {
                    context.Recipes.Add(r);
                }
                context.SaveChanges();
            }

            if (!context.Ingredients.Any())
            {
                var ingredients = new Ingredient[]
                {
                    new Ingredient
                    {
                        ID = 20,
                        Name = "Vodka",
                        Type = "Spirit"
                    },
                    new Ingredient
                    {
                        ID = 1,
                        Name = "Cream",
                        Type = "Dairy/Water/Other"
                    },
                    new Ingredient
                    {
                        ID = 2,
                        Name = "Cream of Coconut",
                        Type = "Dairy/Water/Other"
                    },
                    new Ingredient
                    {
                        ID = 3,
                        Name = "Blue Curacao",
                        Type = "Liqueur"
                    },
                    new Ingredient
                    {
                        ID = 4,
                        Name = "Pineapple Juice",
                        Type = "Juice"
                    },
                    new Ingredient
                    {
                        ID = 5,
                        Name = "Amaretto",
                        Type = "Liqueur"
                    },
                    new Ingredient
                    {
                        ID = 6,
                        Name = "Bailey's Irish Cream",
                        Type = "Liqueur"
                    },
                    new Ingredient
                    {
                        ID = 7,
                        Name = "Kahlúa",
                        Type = "Liqueur"
                    },
                    new Ingredient
                    {
                        ID = 8,
                        Name = "Cranberry Juice",
                        Type = "Juice"
                    },
                    new Ingredient
                    {
                        ID = 9,
                        Name = "Lime Juice",
                        Type = "Juice"
                    },
                    new Ingredient
                    {
                        ID = 10,
                        Name = "Cointreau",
                        Type = "Liqueur"
                    },
                    new Ingredient
                    {
                        ID = 11,
                        Name = "Godiva",
                        Type = "Liqueur"
                    },
                    new Ingredient
                    {
                        ID = 12,
                        Name = "Frangelico",
                        Type = "Liqueur"
                    },
                    new Ingredient
                    {
                        ID = 13,
                        Name = "Rum",
                        Type = "Spirit"
                    },
                    new Ingredient
                    {
                        ID = 14,
                        Name = "Ice",
                        Type = "Dairy/Water/Other"
                    },
                    new Ingredient
                    {
                        ID = 15,
                        Name = "Gin",
                        Type = "Spirit"
                    },
                    new Ingredient
                    {
                        ID = 16,
                        Name = "Lemon Juice",
                        Type = "Juice"
                    },
                    new Ingredient
                    {
                        ID = 17,
                        Name = "Cream Of Banana",
                        Type = "Dairy/Water/Other"
                    },
                    new Ingredient
                    {
                        ID = 18,
                        Name = "Peach Liqueur",
                        Type = "Liqueur"
                    },
                    new Ingredient
                    {
                        ID = 19,
                        Name = "Orange Juice",
                        Type = "Juice"
                    }
                };

                foreach (Ingredient i in ingredients)
                {
                    context.Ingredients.Add(i);
                }
                context.SaveChanges();
            }

            if (!context.Amounts.Any())
            {
                var amounts = new Amount[]
                {
                    #region RecipeID == 7
                    new Amount
                    {
                        Core = true,
                        RecipeID = 7,
                        IngredientID = 20,
                        Ounces = "1.25 oz (3.75 cl)"
                    },
                    new Amount
                    {
                        Core = true,
                        RecipeID = 7,
                        IngredientID = 1,
                        Ounces = "0.75 oz (2.25 cl)"
                    },
                    new Amount
                    {
                        Core = true,
                        RecipeID = 7,
                        IngredientID = 2,
                        Ounces = "0.75 oz (2.25 cl)"
                    },
                    new Amount
                    {
                        Core = true,
                        RecipeID = 7,
                        IngredientID = 3,
                        Ounces = "0.75 oz (2.25 cl)"
                    },
                    new Amount
                    {
                        Core = true,
                        RecipeID = 7,
                        IngredientID = 4,
                        Ounces = "3.25 oz (9.75 cl)"
                    },
                    #endregion

                    #region RecipeID == 1
                    new Amount
                    {
                        Core = true,
                        RecipeID = 1,
                        IngredientID = 20,
                        Ounces = "1 oz (3 cl)"
                    },
                    new Amount
                    {
                        Core = true,
                        RecipeID = 1,
                        IngredientID = 5,
                        Ounces = "1 oz (3 cl)"
                    },
                    new Amount
                    {
                        Core = true,
                        RecipeID = 1,
                        IngredientID = 6,
                        Ounces = "1 oz (3 cl)"
                    },
                    new Amount
                    {
                        Core = true,
                        RecipeID = 1,
                        IngredientID = 7,
                        Ounces = "0.5 oz (1.5 cl)"
                    },
                    #endregion

                    #region RecipeID == 2
                    new Amount
                    {
                        Core = true,
                        RecipeID = 2,
                        IngredientID = 8,
                        Ounces = "1 oz (3 cl)"
                    },
                    new Amount
                    {
                        Core = true,
                        RecipeID = 2,
                        IngredientID = 9,
                        Ounces = "0.5 oz (1.5 cl)"
                    },
                    new Amount
                    {
                        Core = true,
                        RecipeID = 2,
                        IngredientID = 10,
                        Ounces = "0.5 oz (1.5 cl)"
                    },
                    new Amount
                    {
                        Core = true,
                        RecipeID = 2,
                        IngredientID = 20,
                        Ounces = "1.25 oz (3.75 cl)"
                    },
                    #endregion

                    #region RecipeID == 3
                    new Amount
                    {
                        Core = true,
                        RecipeID = 3,
                        IngredientID = 11,
                        Ounces = "2 oz (6 cl)"
                    },
                    new Amount
                    {
                        Core = true,
                        RecipeID = 3,
                        IngredientID = 12,
                        Ounces = "1 splash (0.37 cl/0.13 oz)"
                    },
                    new Amount
                    {
                        Core = true,
                        RecipeID = 3,
                        IngredientID = 13,
                        Ounces = "3 oz (9 cl)"
                    },
                    #endregion

                    #region RecipeID == 4
                    new Amount
                    {
                        Core = true,
                        RecipeID = 4,
                        IngredientID = 2,
                        Ounces = "1 oz (3 cl)"
                    },
                    new Amount
                    {
                        Core = true,
                        RecipeID = 4,
                        IngredientID = 3,
                        Ounces = "1 oz (3 cl)"
                    },
                    new Amount
                    {
                        Core = true,
                        RecipeID = 4,
                        IngredientID = 14,
                        Ounces = "1 cup (25.7 cl/8 oz)"
                    },
                    new Amount
                    {
                        Core = true,
                        RecipeID = 4,
                        IngredientID = 4,
                        Ounces = "2 oz (6 cl)"
                    },
                    new Amount
                    {
                        Core = true,
                        RecipeID = 4,
                        IngredientID = 13,
                        Ounces = "1 oz (3 cl)"
                    },
                    #endregion

                    #region RecipeID == 5
                    new Amount
                    {
                        Core = true,
                        RecipeID = 5,
                        IngredientID = 15,
                        Ounces = "1 oz (3 cl)"
                    },
                    new Amount
                    {
                        Core = true,
                        RecipeID = 5,
                        IngredientID = 4,
                        Ounces = "0.5 oz (1.5 cl)"
                    },
                    new Amount
                    {
                        Core = true,
                        RecipeID = 5,
                        IngredientID = 16,
                        Ounces = "1 oz (3 cl)"
                    },
                    #endregion

                    #region RecipeID == 6
                    new Amount
                    {
                        Core = true,
                        RecipeID = 6,
                        IngredientID = 20,
                        Ounces = "0.75 oz (2.25 cl)"
                    },
                    new Amount
                    {
                        Core = true,
                        RecipeID = 6,
                        IngredientID = 17,
                        Ounces = "0.75 oz (2.25 cl)"
                    },
                    new Amount
                    {
                        Core = true,
                        RecipeID = 6,
                        IngredientID = 18,
                        Ounces = "0.75 oz (2.25 cl)"
                    },
                    new Amount
                    {
                        Core = true,
                        RecipeID = 6,
                        IngredientID = 19,
                        Ounces = "0.75 oz (2.25 cl)"
                    },
                    #endregion

                    #region Variations
                    new Amount
                    {
                        Core = false,
                        RecipeID = 7,
                        IngredientID = 19,
                        Ounces = "3.25 oz (9.75 cl)",
                        ReplaceeID = 4
                    },
                    new Amount
                    {
                        Core = false,
                        RecipeID = 7,
                        IngredientID = 17,
                        Ounces = "0.75 oz (2.25 cl)",
                        ReplaceeID = 2
                    }
                    #endregion
                };

                foreach (Amount a in amounts)
                {
                    context.Amounts.Add(a);
                }
                context.SaveChanges();
            }

            //Create admin user and roles
            await CreateAdmin(context, userManager, roleManager);

        }

        public static async Task CreateAdmin(SiteContext context, UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Add roles
            string[] roles = new string[] { "Admin", "User" };
            foreach (string role in roles)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Add admin user
            if (!context.Users.Any(u => u.UserName == "admin@admin.com"))
            {
                var user = new Users
                {
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    UserName = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    EmailConfirmed = true,
                };
                var result = await userManager.CreateAsync(user, "Password123");

                var adminuser = await userManager.FindByEmailAsync("admin@admin.com");
                if (result.Succeeded)
                {
                    var addresult = await userManager.AddToRoleAsync(adminuser, "Admin");
                }
            }
        }
       
    }
}