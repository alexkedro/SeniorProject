using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Models
{
    public class Collection
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public Member Owner { get; set; }

        public string Name { get; set; }
    }
}
