using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MSA.Application.IServices;
using MSA.Application.Services;
using MSA.Domain.Entities;
using MSA.Domain.Enums;
using MSA.Infrastructure;

namespace MSA.Presentation.Pages.OrderPages
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IAccountService _accountService;
        public IndexModel(IOrderService orderService, IAccountService accountService)
        {
            _orderService = orderService;
            _accountService = accountService;
        }
        [BindProperty(SupportsGet = true)]

        public IList<Order> ListOrder { get;set; } = default!;
        public List<string> AccountName { get; set; } = new List<string>();
        //public List<string> Username { get; set; } = new List<string>();
        public List<string> Address { get; set; } = new List<string>();

        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.Session.GetString("role");
            if (role != "Staff" || role == null)
            {
                return RedirectToPage("/AccessDenied");
            }
            else
            {
                ListOrder = _orderService.GetAll().Where(o => o.IsDeleted == false).ToList();
                foreach (var order in ListOrder)
                {
                    var customer = _accountService.GetById(order.CustomerId);
                    AccountName.Add(customer?.FullName ?? "Unknown");
                    //Username.Add(customer?.Username ?? "Unknown");
                    Address.Add(customer?.Address ?? "Unknown");
                }
                return Page();
            }
        }
    }
}
