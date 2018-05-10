using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProject.Models.RecipeViewModels
{
    public class ListViewModel
    {
        public Recipe Recipe { get; set; }

        public List<Amount> Amounts { get; set; }
    }
}
