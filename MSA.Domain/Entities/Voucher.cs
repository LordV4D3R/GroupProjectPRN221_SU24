using MSA.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Domain.Entities
{
    public class Voucher : BaseEntity
    {
        public string VoucherName { get; set; } = string.Empty;
        public string VoucherCode { get; set;} = string.Empty;
        public string VoucherType { get; set; } = string.Empty;
        public DateTime ValidDate { get; set; }
        public DateTime ExpireTime { get; set; }
        public int Status { get; set; } 
    }
}
