using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeniorProject.Models;

namespace SeniorProject.Models.ManageViewModels
{
    public class ViewCollectionViewModel
    {
        public Collection Collection { get; set; }
        public List<Row> Rows { get; set; }
    }

    public class Row
    {
        public Recipe r1 { get; set; }
        public Recipe r2 { get; set; }
        public Recipe r3 { get; set; }
        public Recipe r4 { get; set; }
    }
}
