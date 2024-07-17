using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSA.Application.IServices;
using MSA.Domain.Dtos.Order;
using MSA.Domain.Dtos.Session;
using MSA.Domain.Entities;
using MSA.Domain.Enums;
using MSA.Presentation.Extensions;

namespace MSA.Presentation.Pages.GuestPages
{
    public class OrderHistoryModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountService _accountService;
        public OrderHistoryModel(IOrderService orderService,
            IAccountService accountService,
            IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }
        [BindProperty(SupportsGet = true)]
        public IList<Order> ListOrder { get; set; } = default!;
        public Order Order { get; set; } = default!;
        public List<string> AccountName { get; set; } = new List<string>();
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            AccountSession current = _httpContextAccessor.HttpContext!.Session.GetObject<AccountSession>("CurrentUser");
            if (current == null || current.Role == RoleType.Staff)
            {
                _httpContextAccessor.HttpContext.Session.Clear();
                return Redirect("/LoginPage");
            }
            else
            {
                ListOrder = _orderService.GetOrderProcessingStatusByAccountId(current.Id).ToList();
                return Page();
            }
        }
    }
}
