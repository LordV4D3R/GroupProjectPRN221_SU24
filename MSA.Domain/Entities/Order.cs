using MSA.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Domain.Entities
{
    public class Order : BaseEntity
    {
        public double TotalPrice {  get; set; }
        public double VoucherId { get; set; }
        public int Status { get; set; }
    }
}
