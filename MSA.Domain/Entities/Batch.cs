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
    [Table("batch")]
    public class Batch : BaseEntity
    {
        [Column("quantity")]
        public int Quantity { get; set; } = 0;

        [Column("expired_on")]
        public DateTime ExpOn { get; set; } = DateTime.Now;

        [Column("status")]
        [EnumDataType(typeof(BatchStatus))]
        public BatchStatus Status { get; set; } = BatchStatus.Active;

        [Column("product_id")]
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;

    }
}
