using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Domain.Dtos.Order
{
    public class OrderViewModel
    {
        public double TotalPrice { get; set; } = 0;
        public int TotalQuantity { get; set; } = 0;
    }
}
