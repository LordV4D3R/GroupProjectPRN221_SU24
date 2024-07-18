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
using MSA.Domain.Enums;
using MSA.Infrastructure;

namespace MSA.Presentation.Pages.OrderPages
{
    public class EditModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IAccountService _accountService;

        public EditModel(IOrderService orderService, IAccountService accountService)
        {
            _orderService = orderService;
            _accountService = accountService;
        }

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
            switch (order.OrderStatus)
            {
                case OrderStatus.Pending:
                    order.OrderStatus = OrderStatus.Processing;
                    break;
                case OrderStatus.Processing:
                    order.OrderStatus = OrderStatus.Completed;
                    break;
                case OrderStatus.Cancelled:
                    order.OrderStatus = OrderStatus.Cancelled;
                    break;
                case OrderStatus.Completed:
                    order.OrderStatus = OrderStatus.Completed;
                    break;
                default:
                    order.OrderStatus = OrderStatus.InCart;
                    break;
            }
            _orderService.Update(order);
            return RedirectToPage("./Index");
        }
    }
}
