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
        public string SearchString { get; set; }
        public IList<Order> Order { get;set; } = default!;
        public List<string> AccountName { get; set; } = new List<string>();
        public async Task OnGetAsync()
        {
            Order = _orderService.GetAll().ToList();
            foreach(var order in Order)
            {
                var customer = _accountService.GetById(order.CustomerId);
                AccountName.Add(customer?.Username ?? "Unknown");
            }
        }
    }
}
