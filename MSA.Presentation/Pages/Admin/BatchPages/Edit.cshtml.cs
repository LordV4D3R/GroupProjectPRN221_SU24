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
using Services;

namespace MSA.Presentation.Pages.Admin.BatchPages
{
    public class EditModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IBatchService _batchService;

        public EditModel(IProductService productService, IBatchService batchService)
        {
            _productService = productService;
            _batchService = batchService;
        }

        [BindProperty]
        public Batch Batch { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var batch =  _batchService.GetById(id);
            if (batch == null)
            {
                return NotFound();
            }
            Batch = batch;
           ViewData["ProductId"] = new SelectList(_productService.GetAll(), "Id", "ProductName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            _batchService.Update2(Batch);
            return RedirectToPage("./Index");
        }
    }
}
