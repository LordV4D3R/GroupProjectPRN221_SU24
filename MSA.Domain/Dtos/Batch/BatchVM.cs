using MSA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSA.Domain.Common;

namespace MSA.Domain.Dtos.Batch
{
    public class BatchVM
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; } = 0;

        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpOn { get; set; } = DateTime.Now;

        public Guid ProductId { get; set; }
    }
}
