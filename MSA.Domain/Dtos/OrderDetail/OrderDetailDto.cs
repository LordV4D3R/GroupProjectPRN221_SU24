using MSA.Domain.Common;
using MSA.Domain.Dtos.Order;
using MSA.Domain.Dtos.Product;
using MSA.Domain.Enums;

namespace MSA.Domain.Dtos.OrderDetail
{
    public class OrderDetailDto : BaseEntity
    {
        public int Quantity { get; set; } = 0;
        public double Price { get; set; } = 0;
        public virtual OrderDto Orders { get; set; } = null;
        public virtual ProductDto Products { get; set; } = null;
        
    }
}
