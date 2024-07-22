using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSA.Application.IServices;
using MSA.Domain.Dtos.Order;
using MSA.Domain.Dtos.OrderDetail;
using MSA.Domain.Dtos.Product;
using MSA.Domain.Dtos.Session;
using MSA.Domain.Entities;
using MSA.Domain.Enums;
using MSA.Presentation.Extensions;
using Services;

namespace MSA.Presentation.Pages.GuestPages
{
    public class CartModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IProductService _productService;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBatchService _batchService;

        public CartModel(IOrderService orderService,
            IOrderDetailService orderDetailService,
            IProductService productService,
            IAccountService accountService,
            IHttpContextAccessor httpContextAccessor,
            IBatchService batchService)
        {
            _orderService = orderService;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            _orderDetailService = orderDetailService;
            _productService = productService;
            _batchService = batchService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            AccountSession current = _httpContextAccessor.HttpContext!.Session.GetObject<AccountSession>("CurrentUser");
            if (current == null || current.Role == RoleType.Staff)
            {
                _httpContextAccessor.HttpContext.Session.Clear();
                return Redirect("/LoginPage");
            }
            LoadCart(current);

            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;
        public ProductViewModel ProductViewModels { get; set; } = default!;
        public IEnumerable<OrderDetailDto> OrderDetailDtos { get; set; } = default!;
        public IList<OrderDetail> OrderDetail { get; set; } = default!;
        public IList<OrderDetailViewModel> OrderDetailViewModels { get; set; } = default!;
        public OrderViewModel OrderViewModel { get; set; } = default!;
        public List<string> ProductName { get; set; } = new List<string>();


        private void LoadCart(AccountSession current)
        {
            if (current != null && current.Role == RoleType.Customer)
            {
                Order = _orderService.GetOrderInCartStatusByAccountId(current.Id);
                if (Order != null)
                {
                    OrderDetail = _orderDetailService.GetAll().Where(x => x.OrderId == Order.Id).ToList();
                    foreach (var item in OrderDetail)
                    {
                        var name = _productService.GetById(item.ProductId);
                        ProductName.Add(name?.ProductName ?? "Unknown");
                    }
					OrderViewModel = new OrderViewModel
					{
						OrderId = Order.Id,
						TotalPrice = Order.TotalPrice,
						TotalQuantity = Order.TotalQuantity,
					};
				}
            }
        }
		public async Task<IActionResult> OnPostAsync(string handler)
		{
			if (handler == "ProcessPayment")
			{
                AccountSession current = _httpContextAccessor.HttpContext!.Session.GetObject<AccountSession>("CurrentUser");

                Order = _orderService.GetOrderInCartStatusByAccountId(current.Id);
                if (Order != null)
                {
                    OrderDetail = _orderDetailService.GetAll().Where(x => x.OrderId == Order.Id).ToList();
                    foreach (var item in OrderDetail)
                    {
                        var name = _productService.GetById(item.ProductId);
                        ProductName.Add(name?.ProductName ?? "Unknown");
                        var batch = _batchService.GetAllByProductId(item.ProductId).OrderBy(b => b.ExpOn).ToList();
                        var currentQuantity = Order.TotalQuantity;
                        foreach (var item2 in batch)
                        {
                            if (item2.Quantity >= currentQuantity)
                            {
                                item2.Quantity -= currentQuantity;
                                currentQuantity = 0;
                                _batchService.Update2(item2);
                                break;
                            }
                            else
                            {
                                currentQuantity -= item2.Quantity;
                                item2.Quantity = 0;
                                _batchService.Update2(item2);
                            }
                        }
                    }
                }
                Order.OrderStatus = OrderStatus.Pending;
				 _orderService.Update(Order);

				return RedirectToPage("SuccessOrder");
			}
			return Page();
		}
    }
}
