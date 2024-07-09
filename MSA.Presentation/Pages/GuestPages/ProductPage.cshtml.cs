using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using MSA.Application.IRepositories;
using MSA.Application.IServices;
using MSA.Application.Services;
using MSA.Domain.Dtos.Product;
using MSA.Domain.Dtos.Session;
using MSA.Domain.Entities;
using MSA.Domain.Enums;
using MSA.Infrastructure.Repositories;
using MSA.Presentation.Extensions;
using Services;

namespace MSA.Presentation.Pages.GuestPages
{
    public class ProductPageModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderDetailService _orderDetailService;

        public ProductPageModel(ILogger<IndexModel> logger,
            IProductService productService,
            IOrderService orderService,
            IHttpContextAccessor httpContextAccessor,
            IOrderDetailService orderDetailService)
        {
            _logger = logger;
            _productService = productService;
            _orderService = orderService;
            _httpContextAccessor = httpContextAccessor;
            _orderDetailService = orderDetailService;
        }

        [BindProperty]
        public IList<ProductViewModel> ProductViewModel { get; set; } = default!;
        public IList<Product> Product { get; set; } = default!;
        public IList<Order> Order { get; set; } = default!;
        public Account Account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            LoadData();
            return Page();
        }

        public async Task<IActionResult> OnGetCartAsync(Guid id)
        {
            Product product = _productService.GetById(id);
            if (product != null)
            {
                //Check Session
                AccountSession current = _httpContextAccessor.HttpContext!.Session.GetObject<AccountSession>("CurrentUser");
                if (current != null && current.Role == RoleType.Customer)
                {
                    //Find Order
                    Order order = _orderService.GetOrderInCartStatusByAccountId(current.Id);
                    if (order == null)
                    {
                        order = new Order
                        {

                            OrderStatus = OrderStatus.InCart,
                            CustomerId = current.Id,
                            OrderDetails = new List<OrderDetail>(),
                            CreatedOn = DateTime.Now,
                        };

                        OrderDetail newOrderDetail = new OrderDetail
                        {
                            Quantity = 1,
                            Price = product.Price,
                            ProductId = product.Id,
                            CreatedOn = DateTime.Now,
                        };

                        order.OrderDetails.Add(newOrderDetail);

                        _orderService.Add(order);
                        _orderService.Save();
                    } else
                    {
                        var list = _orderDetailService.GetAll().Where(x => x.OrderId == order.Id && x.ProductId == id);
                        if (!list.IsNullOrEmpty())
                        {
							OrderDetail orderDetail = list.First();
                            orderDetail.Quantity++;
                            orderDetail.Price = order.TotalQuantity * product.Price;
                            orderDetail.OrderId = order.Id;
                            order.TotalPrice += product.Price;
                            order.TotalQuantity += orderDetail.Quantity;
                            _orderDetailService.Update(orderDetail);
							_orderService.Update(order);

						}
						else
                        {
                            OrderDetail newOrderDetail = new OrderDetail
                            {
                                Quantity = 1,
                                Price = product.Price,
                                ProductId = product.Id,
                            };
                            order.TotalPrice += product.Price;
                            order.OrderDetails.Add(newOrderDetail);
							_orderService.Add(order);
							_orderService.Save();
						}
                    }
                }
                
            }
            LoadData();
            return Page();
        }

        private void LoadData()
        {
            Product = _productService.GetAll().ToList();
            ProductViewModel = Product.Select(product => new ProductViewModel
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Status = product.Status
            }).ToList();
        }
    }
}
