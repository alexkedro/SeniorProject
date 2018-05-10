using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeniorProject.Models;

namespace SeniorProject.Models.RecipeViewModels
{
    public class DisplayViewModel
    {
        public Recipe Recipe { get; set; }

        public List<Amount> Amounts { get; set; }

        public List<Amount> Variations { get; set; }
    }
}
