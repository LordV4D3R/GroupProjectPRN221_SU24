using MSA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Domain.Dtos.Session
{
    public class AccountSession
    {
        public Guid Id { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Email { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public RoleType Role { get; set; } = RoleType.Customer;

    }
}
