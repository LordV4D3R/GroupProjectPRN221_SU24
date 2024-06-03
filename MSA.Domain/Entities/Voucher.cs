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
    [Table("voucher")]
    public class Voucher : BaseEntity
    {
        [Column("voucher_name")]
        [Required]
        public string VoucherName { get; set; } = string.Empty;
        [Column("voucher_code")]
        [Required]
        public string VoucherCode { get; set;} = string.Empty;
        [Column("valid_date")]
        public DateTime ValidDate { get; set; }
        [Column("expire_date")]
        public DateTime ExpireDate { get; set; }
        [Column("voucher_status")]
        [EnumDataType(typeof(VoucherStatus))]
        public VoucherStatus Status { get; set; } 
    }
}
