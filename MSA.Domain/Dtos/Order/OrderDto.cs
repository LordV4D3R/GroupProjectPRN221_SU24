using MSA.Domain.Common;
using MSA.Domain.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Domain.Dtos.Order
{
	public class OrderDto : BaseEntity
	{
		public double TotalPrice { get; set; } = 0;
		public int TotalQuantity { get; set; } = 0;
		public virtual ICollection<AccountDto> AccountDetails { get; set; } = new List<AccountDto>;
		public virtual ICollection<OrderDto> Orders { get; set; } = new List<OrderDto>;

	}
}
