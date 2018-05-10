using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SeniorProject.Models
{
    public class Amount
    {
        public int ID { get; set; }

        public int RecipeID { get; set; }

        [ForeignKey("RecipeID")]
        public Recipe Parent { get; set; }

        public int IngredientID { get; set; }

        [ForeignKey("IngredientID")]
        public Ingredient Child { get; set; }

        public int? ReplaceeID { get; set; }

        [ForeignKey("ReplaceeID")]
        public Ingredient Replacee { get; set; }

        public bool Core { get; set; }

        public string Ounces { get; set; }
    }
}
