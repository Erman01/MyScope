using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyScope.Core.Models
{
    public class ProductCategory:BaseEntity
    { 
        [Display(Name="Category Name")]
        public string Name { get; set; }
     
    }
}
