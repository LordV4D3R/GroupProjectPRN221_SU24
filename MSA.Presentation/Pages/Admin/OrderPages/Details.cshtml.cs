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
using MSA.Infrastructure;

namespace MSA.Presentation.Pages.OrderPages
{
    public class DetailsModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IAccountService _accountService;
        public DetailsModel(IOrderService orderService, IAccountService accountService)
        {
            _orderService = orderService;
            _accountService = accountService;
        }

        public Order Order { get; set; } = default!;
        public string AccountName { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
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
                var name = _accountService.GetById(order.CustomerId);
                AccountName = name?.FullName ?? "Unknown";
            }
            return Page();
        }
    }
}
