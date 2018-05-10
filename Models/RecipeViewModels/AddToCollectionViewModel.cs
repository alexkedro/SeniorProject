using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeniorProject.Models;

namespace SeniorProject.Models.RecipeViewModels
{
    public class AddToCollectionViewModel
    {
        public Recipe Recipe { get; set; }

        public List<Collection> Collections { get; set; }

        public int ChoiceID { get; set; }

        public int RecipeID { get; set; }
    }
}
