using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MSA.Application.IServices;
using MSA.Domain.Entities;
using MSA.Infrastructure;

namespace MSA.Presentation.Pages.OrderPages
{
    public class EditModel : PageModel
    {
        private readonly IOrderService _context;
        private readonly IAccountService _accountService;

        public EditModel(IOrderService context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order =  _context.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            Order = order;
           ViewData["CustomerId"] = new SelectList(_accountService.GetAll(), "Id", "Address");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }           
            _context.Update(Order);
            _context.Save();
            return RedirectToPage("./Index");
        }

        /*private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }*/
    }
}
