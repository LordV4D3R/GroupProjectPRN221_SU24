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
        public double TotalPrice { get; set; } = 0;

        [Column("total_quantity")]
        public int TotalQuantity { get; set; } = 0;

        [Column("order_status")]
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        [Column("OrderRefundStatus")]
        public OrderRefundStatus OrderRefundStatus { get; set; } = OrderRefundStatus.None;

        [Column("customer_id")]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public virtual Account Customer { get; set; } = null!;

        //OrderDetail
        [InverseProperty("Order")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
