using MSA.Domain.Common;
using MSA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Domain.Entities
{
    [Table("order")]
    public class Order : BaseEntity
    {
        [Column("total_price")]
        public double TotalPrice { get; set; }
        [Column("total_quantity")]
        public int TotalQuantity { get; set; }
        [Column("order_status")]
        public OrderStatus OrderStatus { get; set; }
        [Column("OrderRefundStatus")]
        public OrderRefundStatus OrderRefundStatus { get; set; }
    }
}
