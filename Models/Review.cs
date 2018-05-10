using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class Review
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public Member Writer { get; set; }

        public int Rating { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int RecipeID { get; set; }

        [ForeignKey("RecipeID")]
        public Recipe Target { get; set; }
    }
}
