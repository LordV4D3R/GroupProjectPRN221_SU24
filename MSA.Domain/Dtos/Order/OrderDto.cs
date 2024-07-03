using MSA.Domain.Common;
using MSA.Domain.Dtos.Account;
using MSA.Domain.Dtos.OrderDetail;


namespace MSA.Domain.Dtos.Order
{
	public class OrderDto : BaseEntity
	{
		public double TotalPrice { get; set; } = 0;
		public int TotalQuantity { get; set; } = 0;
		public virtual AccountViewModel Account { get; set; } = null;
		public virtual ICollection<OrderDetailDto> OrderDetails { get; set; } = new List<OrderDetailDto>();

	}
}
