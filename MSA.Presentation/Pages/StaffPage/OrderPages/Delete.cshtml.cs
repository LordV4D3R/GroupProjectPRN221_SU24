using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MSA.Application.IServices;
using MSA.Application.Services;
using MSA.Domain.Entities;
using MSA.Infrastructure;

namespace MSA.Presentation.Pages.OrderPages
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IAccountService _accountService;
        public DeleteModel(IOrderService orderService, IAccountService accountService)
        {
            _orderService = orderService;
            _accountService = accountService;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;
        public string AccountName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var role = HttpContext.Session.GetString("role");
            if (role != "Staff" || role == null)
            {
                return RedirectToPage("/AccessDenied");
            }
            else
            if (id == null)
            {
                return NotFound();
            }

            var order = _orderService.GetById(id);

            if (order == null)
            {
                return NotFound();
            }
            else
            {
                Order = order;
                var customer = _accountService.GetById(Order.CustomerId);
                AccountName = customer?.Username ?? "Unknown";
                Address = customer?.Address ?? "Unknown";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _orderService.GetById(id);
            if (order != null)
            {
                order.IsDeleted = true;
                Order = order;
                _orderService.Update(Order);
                _orderService.Save();
            }

            return RedirectToPage("./Index");
        }
    }
}
