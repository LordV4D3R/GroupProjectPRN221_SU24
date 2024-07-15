using MSA.Domain.Common;
using MSA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Domain.Entities
{
    [Table("product")]
    public class Product : BaseEntity
    {
        [Column("product_name")]
        [Required]
        [MinLength(1), MaxLength(50)]    
        public string ProductName { get; set; } = string.Empty;
       
        [Column("price")]
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double Price { get; set; } = 0;
       
        [Column("description")]
        public string Description { get; set; } = string.Empty;
      
        [Column("img_url")]        
        public string ImageUrl { get; set; } = string.Empty;
            
        [Column("status")]
        [EnumDataType(typeof(ProductStatus))]
        public ProductStatus Status { get; set; } = ProductStatus.ComingSoon;

        [InverseProperty("Product")]
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        [Column("category_id")]
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        [InverseProperty("Product")]
        public virtual ICollection<Batch> Batches { get; set; } = new List<Batch>();

    }
}
