using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeniorProject.Models;

namespace SeniorProject.Models.RecipeViewModels
{
    public class SearchViewModel
    {
        public List<Recipe> Recipes { get; set; }

        public List<Amount> Amounts { get; set; }

        public string Parameter { get; set; }

        public int Off { get; set; }
    }
}
