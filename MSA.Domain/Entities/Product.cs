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
        public string ProductName { get; set; } = string.Empty;
       
        [Column("price")]
        [Required]
        public double Price { get; set; } = 0;
       
        [Column("description")]
        public string Description { get; set; } = string.Empty;
      
        [Column("img_url")]
        public string ImageUrl { get; set; } = string.Empty;
      
        [Column("quantity")]
        [Required]
        public int Quantity { get; set; } = 0;
      
        [Column("expired_on")]
        public DateTime ExpOn { get; set; } = DateTime.Now;
      
        [Column("status")]
        [EnumDataType(typeof(ProductStatus))]
        public ProductStatus Status { get; set; } = ProductStatus.OutOfStock;

        [ForeignKey("OrderDetail")]
        public ICollection<OrderDetail> OrderDetails { get; set; }

        [Column("category_id")]
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
