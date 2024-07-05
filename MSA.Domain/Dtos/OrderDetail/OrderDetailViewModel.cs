using MSA.Domain.Dtos.Order;
using MSA.Domain.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Domain.Dtos.OrderDetail
{
    public class OrderDetailViewModel
    {
        public int Quantity { get; set; } = 0;
        public double Price { get; set; } = 0;
        public virtual OrderViewModel Orders { get; set; } = null;
        public virtual ProductViewModel Product { get; set; } = null;
    }
}
