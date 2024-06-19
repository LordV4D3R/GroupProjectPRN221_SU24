using MSA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Domain.Dtos.Product
{
    public class ProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public double Price { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int TotalQuantity { get; set; } = 0;
        public ProductStatus Status { get; set; } = ProductStatus.ComingSoon;
    }
}
