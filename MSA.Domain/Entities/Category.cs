using MSA.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Domain.Entities
{
    [Table("category")]
    public class Category : BaseEntity
    {
        [Column("category_name")]
        [Required]
        public string CategoryName { get; set; } = string.Empty;
       
        [Column("description")]
        [Required]
        public string Description { get; set; } = string.Empty;


        [InverseProperty("Category")]
        public virtual ICollection<Product> Products { get; set; }

    }
}
