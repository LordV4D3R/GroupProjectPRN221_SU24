using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSA.Application.IServices;
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
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartModel(IOrderService orderService,
            IAccountService accountService,
			IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }
		public async Task<IActionResult> OnGetAsync()
		{
			AccountSession current = _httpContextAccessor.HttpContext!.Session.GetObject<AccountSession>("CurrentUser");
			if (current == null || current.Role == RoleType.Staff)
			{
				_httpContextAccessor.HttpContext.Session.Clear();
				return Redirect("/Login");
			}

			ViewData["CustomerId"] = new SelectList( _accountService.GetAll(), "Id", "Address");
			return Page();
		}

        [BindProperty]
        public Order Order { get; set; } = default!;


    }
}
