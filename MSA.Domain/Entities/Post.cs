using MSA.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Domain.Entities
{
    [Table("post")]
    public class Post : BaseEntity
    {
        [Column("title")]
        [Required]
        public string Title { get; set; }
        [Column("content")]
        [Required]
        public string Content { get; set; }
        [Column("image_url")]
        [StringLength(500)]
        public string ImageUrl { get; set; }
        [Column("staff_id")]
        [ForeignKey("account")]
        public Guid StaffId { get; set; }
        public Account Account { get; set; }
    }
}
