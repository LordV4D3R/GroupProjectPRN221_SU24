using MSA.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Domain.Entities
{
    public class Feedback : BaseEntity
    {
        public string Context {  get; set; } = string.Empty;
        public string img_Url { get; set; } = string.Empty;
    }
}
