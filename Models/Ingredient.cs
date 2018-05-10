using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class Ingredient
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }
}
