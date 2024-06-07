using MSA.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Domain.Entities
{
    [Table("feedback")]
    public class Feedback : BaseEntity
    {
        [Column("context")]
        public string Context {  get; set; } = string.Empty;

    }
}
