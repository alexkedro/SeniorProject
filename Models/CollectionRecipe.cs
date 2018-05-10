using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class CollectionRecipe
    {
        public int ID { get; set; }

        public int CollectionID { get; set; }

        [ForeignKey("CollectionID")]
        public Collection Collection { get; set; }

        public int RecipeID { get; set; }

        [ForeignKey("RecipeID")]
        public Recipe Recipe { get; set; }
    }
}
