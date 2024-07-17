using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using MSA.Application.IServices;
using MSA.Application.Services;
using MSA.Domain.Dtos.Product;
using MSA.Domain.Dtos.Session;
using MSA.Domain.Entities;
using MSA.Domain.Enums;
using MSA.Presentation.Extensions;
using Services;

namespace MSA.Presentation.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IBatchService _batchService;
        private readonly ICategoryService _categoryService;

        public IndexModel(ILogger<IndexModel> logger,
            IProductService productService,
            IOrderService orderService,
            IHttpContextAccessor httpContextAccessor,
            IOrderDetailService orderDetailService,
            IBatchService batchService,
            ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _orderService = orderService;
            _httpContextAccessor = httpContextAccessor;
            _orderDetailService = orderDetailService;
            _batchService = batchService;
            _categoryService = categoryService;
        }

        [BindProperty]
        public IList<ProductViewModel> ProductViewModel { get; set; } = default!;
        public IList<Product> Product { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync()
        {
            LoadData();
            return Page();
        }

        public async Task<IActionResult> OnGetCartAsync(Guid productId)
        {
            var batches = _batchService.GetAllByProductId(productId).OrderBy(b => b.ExpOn).ToList();
            int remainingQuantity = 1;
            foreach (var batch in batches)
            {
                if (batch.Quantity >= remainingQuantity)
                {
                    batch.Quantity -= remainingQuantity;
                    remainingQuantity = 0;
                    _batchService.Update2(batch);
                    break;
                }
                else
                {
                    remainingQuantity -= batch.Quantity;
                    batch.Quantity = 0;
                    _batchService.Update2(batch);
                }
            }
            Product product = _productService.GetById(productId);
            if (product != null && product.Status == ProductStatus.InStock)
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
                            TotalPrice = product.Price,
                            TotalQuantity = 1,
                            OrderDetails = new List<OrderDetail>(),
                            CreatedOn = DateTime.Now,
                            CreatedBy = current.DisplayName,
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
                    }
                    else
                    {
                        var list = _orderDetailService.GetAll().Where(x => x.OrderId == order.Id && x.ProductId == productId);
                        if (!list.IsNullOrEmpty())
                        {
                            OrderDetail orderDetail = list.First();
                            orderDetail.Quantity++;
                            orderDetail.Price = orderDetail.Quantity * product.Price;
                            orderDetail.OrderId = order.Id;
                            order.TotalPrice += product.Price;
                            order.TotalQuantity++;
                            order.UpdatedOn = DateTime.Now;
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
                                OrderId = order.Id,
                            };
                            order.TotalPrice += product.Price;
                            order.TotalQuantity += 1;
                            _orderDetailService.Add(newOrderDetail);
                            _orderDetailService.Save();
                            _orderService.Update(order);
                        }
                    }
                }

            }
            LoadData();
            return Page();
        }

        private void LoadData()
        {
            Product = _productService.GetAll().Where(x => x.IsDeleted == false).ToList();
            foreach (var product in Product)
            {
                var quantity = _batchService.GetAllByProductId(product.Id).Sum(x => x.Quantity);
                if (quantity == 0 && product.Status == ProductStatus.InStock)
                {
                    product.Status = ProductStatus.OutOfStock;
                    _productService.Update2(product);
                }
            }
            ProductViewModel = Product.Select(product => new ProductViewModel
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                Quantity = _batchService.GetAllByProductId(product.Id).Sum(x => x.Quantity),
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Status = product.Status
            }).ToList();
        }
    }
}
