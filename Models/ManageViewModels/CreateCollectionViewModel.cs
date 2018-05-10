using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProject.Models.ManageViewModels
{
    public class CreateCollectionViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
