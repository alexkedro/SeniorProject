using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProject.Models.RecipeViewModels
{
    public class AddIngredientViewModel
    {
        public Recipe Recipe { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public int IngredientID { get; set; }

        public int RecipeID { get; set; }

        public string Amount { get; set; }
    }
}
