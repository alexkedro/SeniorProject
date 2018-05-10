using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace SeniorProject.Models
{
    public class MemberIngredient
    {
        public int ID { get; set; }

        public int MemberID { get; set; }

        [ForeignKey("MemberID")]
        public Member Owner { get; set; }

        public int IngredientID { get; set; }

        [ForeignKey("IngredientID")]
        public Ingredient Element { get; set; }
    }
}
