using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MSA.Application.IServices;
using MSA.Domain.Entities;
using MSA.Infrastructure;

namespace MSA.Presentation.Pages.OrderPages
{
    public class DetailsModel : PageModel
    {
        private readonly IOrderService _context;
        private readonly IAccountService _accountService;

        public DetailsModel(IOrderService context)
        {
            _context = context;
        }

        public Order Order { get; set; } = default!;
        public string customerName { get; set; } = string.Empty;
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _context.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            else
            {
                Order = order;
                var custoomer = _accountService.GetById(order.CustomerId);
                customerName = custoomer?.Username ?? "Unknown";
            }
            return Page();
        }
    }
}
