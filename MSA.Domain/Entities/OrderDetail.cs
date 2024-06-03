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
    [Table("order_detail")]
    public class OrderDetail : BaseEntity
    {
        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("price")]
        public double Price { get; set; }

        [Column("order_id")]
        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }

    }
}
