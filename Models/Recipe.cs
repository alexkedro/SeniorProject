using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class Recipe
    {
        public int ID{ get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Instructions { get; set; }

        [Required]
        public string Notes { get; set;}

        public string History { get; set; }
        
        public string ImagePath { get; set; }

        public string VideoLink { get; set; }
    }
}
