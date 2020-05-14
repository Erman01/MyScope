using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyScope.Core.Models
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        [Display(Name ="Created At")]
        public DateTime CreatedAt { get; set; }
        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedAt = DateTime.Now;
        }
    }
}
