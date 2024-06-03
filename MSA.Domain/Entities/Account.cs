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
    [Table("account")]
    public class Account : BaseEntity
    {
        [Column("username")]
        [Required]
        public string Username { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Column("fullname")]
        [StringLength(500)]
        public string FullName { get; set; } = string.Empty;

        [Column("email")]
        [Required]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [Column("phone_number")]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Column("address")]
        [StringLength(500)]
        public string Address { get; set; } = string.Empty;

        [Column("image_url")]
        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        [Column("status")]
        [EnumDataType(typeof(AccountStatus))]
        public AccountStatus Status { get; set; } = AccountStatus.Active;

        [Column("role")]
        [EnumDataType(typeof(RoleType))]
        public RoleType Role { get; set; } = RoleType.Customer;

        [InverseProperty("Staff")]
        public ICollection<Post> Posts { get; set; }

        [InverseProperty("Staff")]
        public ICollection<Voucher> StaffVouchers { get; set; }
        
        [InverseProperty("Customer")]
        public ICollection<Voucher> CustomerVouchers { get; set; }

        [InverseProperty("Customer")]
        public ICollection<Order> CustomerOrders { get; set; }


    }
}
