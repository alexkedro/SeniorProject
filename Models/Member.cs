using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

namespace SeniorProject.Models
{
    public class Member 
    {
        public int ID { set; get;}

        public string UserID { get; set; }

        [ForeignKey("UserID")]
        public Users User { get; set; }
        
        public string Name { get; set; }

    }

}