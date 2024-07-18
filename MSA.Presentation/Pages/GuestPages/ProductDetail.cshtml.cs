using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.IdentityModel.Tokens;
using MSA.Application.IServices;
using MSA.Application.Services;
using MSA.Domain.Dtos.Product;
using MSA.Domain.Dtos.Session;
using MSA.Domain.Entities;
using MSA.Domain.Enums;
using MSA.Presentation.Extensions;
using Services;

namespace MSA.Presentation.Pages.GuestPages
{
    public class ProductDetailModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBatchService _batchService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;

        public ProductDetailModel(IProductService productService,
            ICategoryService categoryService,
            IBatchService batchService,
            IHttpContextAccessor httpContextAccessor,
            IOrderService orderService,
            IOrderDetailService orderDetailService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _batchService = batchService;
            _httpContextAccessor = httpContextAccessor;
            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        [BindProperty]
        public ProductViewModel ProductViewModel { get; set; } = default!;
        public List<string> CategoryName { get; set; } = new List<string>();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            LoadData(id);

            return Page();
        }
        private void LoadData(Guid id)
        {
            Product = _productService.GetById(id);

            var quantity = _batchService.GetAllByProductId(Product.Id).Sum(x => x.Quantity);
            if (quantity == 0 && Product.Status == ProductStatus.InStock)
            {
                Product.Status = ProductStatus.OutOfStock;
                _productService.Update2(Product);
            }
            ProductViewModel = new ProductViewModel
            {
                ProductId = Product.Id,
                ProductName = Product.ProductName,
                Price = Product.Price,
                Description = Product.Description,
                ImageUrl = Product.ImageUrl,
                TotalQuantity = _batchService.GetAllByProductId(Product.Id).Sum(x => x.Quantity),
                Status = Product.Status
            };
        }

        public async Task<IActionResult> OnPostCartAsync()
        {

            var batches = _batchService.GetAllByProductId(ProductViewModel.ProductId).OrderBy(b => b.ExpOn).ToList();
            var productTotalQuantity = _batchService.GetAllByProductId(ProductViewModel.ProductId).Sum(x => x.Quantity);
            if (productTotalQuantity < ProductViewModel.Quantity)
            {
                ModelState.AddModelError("ProductViewModel.Quantity", "Invalid Quantity!!!");
                LoadData(ProductViewModel.ProductId);
                return Page();
            }
            int remainingQuantity = ProductViewModel.Quantity;
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
            Product product = _productService.GetById(ProductViewModel.ProductId);

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
                        TotalPrice = product.Price * ProductViewModel.Quantity,
                        TotalQuantity = ProductViewModel.Quantity,
                        OrderDetails = new List<OrderDetail>(),
                        CreatedOn = DateTime.Now,
                        CreatedBy = current.DisplayName,
                    }; 

                    OrderDetail newOrderDetail = new OrderDetail
                    {
                        Quantity = ProductViewModel.Quantity,
                        Price = product.Price * ProductViewModel.Quantity,
                        ProductId = product.Id,
                        CreatedOn = DateTime.Now,
                    };

                    order.OrderDetails.Add(newOrderDetail);

                    _orderService.Add(order);
                    _orderService.Save();
                }
                else
                {
                    var list = _orderDetailService.GetAll().Where(x => x.OrderId == order.Id && x.ProductId == ProductViewModel.ProductId);
                    if (!list.IsNullOrEmpty())
                    {
                        OrderDetail orderDetail = list.First();
                        orderDetail.Quantity += ProductViewModel.Quantity;
                        orderDetail.Price = orderDetail.Quantity * product.Price;
                        orderDetail.OrderId = order.Id;
                        order.TotalPrice += product.Price * ProductViewModel.Quantity;
                        order.TotalQuantity += ProductViewModel.Quantity;
                        order.UpdatedOn = DateTime.Now;
                        _orderDetailService.Update(orderDetail);
                        _orderService.Update(order);
                    }
                    else
                    {
                        OrderDetail newOrderDetail = new OrderDetail
                        {
                            Quantity = ProductViewModel.Quantity,
                            Price = product.Price * ProductViewModel.Quantity,
                            ProductId = product.Id,
                            OrderId = order.Id,
                        };
                        order.TotalPrice += newOrderDetail.Price;
                        order.TotalQuantity += newOrderDetail.Quantity;
                        _orderDetailService.Add(newOrderDetail);
                        _orderDetailService.Save();
                        _orderService.Update(order);
                    }
                }
            }
            LoadData(ProductViewModel.ProductId);
            return Page();
        }

    }
}
