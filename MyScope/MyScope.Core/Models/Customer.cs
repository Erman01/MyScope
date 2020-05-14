using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyScope.Core.Models
{
    public class Customer:BaseEntity
    {
        public string UserId { get; set; }
        [Display(Name="First Name")]
        public string Name { get; set; }
        [Display(Name="Last Name")]
        public string LastName { get; set; }
        [Display(Name="E-Mail")]
        public string EMail { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name="Zip Code")]
        public string ZipCode { get; set; }

    }
}
