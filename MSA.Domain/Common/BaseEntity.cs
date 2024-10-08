﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSA.Domain.Common
{
    public abstract class BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("created_on")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [Column("updated_on")]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        [Column("created_by")]
        public string? CreatedBy { get; set; }

        [Column("updated_by")]
        public string? UpdatedBy { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
