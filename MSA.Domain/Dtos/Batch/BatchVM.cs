using MSA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Domain.Dtos.Batch
{
    public class BatchVM
    {
        [Required]
        public int Quantity { get; set; } = 0;

        [Required]
        public DateTime ExpOn { get; set; } = DateTime.Now;

        public Guid ProductId { get; set; }

    }
}
