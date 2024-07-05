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

        public CartModel(IOrderService orderService,
            IOrderDetailService orderDetailService,
            IProductService productService,
            IAccountService accountService,
            IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            _orderDetailService = orderDetailService;
            _productService = productService;
        }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            AccountSession current = _httpContextAccessor.HttpContext!.Session.GetObject<AccountSession>("CurrentUser");
            if (current == null || current.Role == RoleType.Staff)
            {
                _httpContextAccessor.HttpContext.Session.Clear();
                return Redirect("/LoginPage");
            }
            LoadCart(current);
            ViewData["CustomerId"] = new SelectList(_accountService.GetAll(), "Id", "Address");
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;
        public ProductViewModel ProductViewModels { get; set; } = default!;
        public IEnumerable<OrderDetailDto> OrderDetailDtos { get; set; } = default!;
		public IEnumerable<OrderDetail> OrderDetails { get; set; } = default!;
		public IList<OrderDetailViewModel> OrderDetailViewModels { get; set; } = default!;
        public OrderViewModel OrderViewModel { get; set; } = default!;


        private void LoadCart(AccountSession current)
        {
            if (current != null && current.Role == RoleType.Customer)
            {
                Order = _orderService.GetOrderInCartStatusByAccountId(current.Id);
                if (Order != null)
                {

                    OrderDetails = _orderDetailService.GetAllOrderDetailOrderId(Order.Id);
                    if (OrderDetails != null)
                    {
                        OrderDetailViewModels = OrderDetails.Select(x => new OrderDetailViewModel
                        {
                            Quantity = x.Quantity,
                            Price = x.Price * x.Quantity,
                            Product = new ProductViewModel
                            {
                                ProductName = x.Product.ProductName,
                                Description = x.Product.Description,
                                ImageUrl = x.Product.ImageUrl,
                            }
                        }).ToList();
                    }
                }
                else {
                
                }
            } else 
            { 
            
            }
        }
    }
}
