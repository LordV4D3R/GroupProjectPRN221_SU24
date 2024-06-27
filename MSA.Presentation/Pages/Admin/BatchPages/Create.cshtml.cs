using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSA.Application.IServices;
using MSA.Domain.Entities;
using MSA.Infrastructure;
using Services;

namespace MSA.Presentation.Pages.Admin.BatchPages
{
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IBatchService _batchService;

        public CreateModel(IProductService productService, IBatchService batchService)
        {
            _productService = productService;
            _batchService = batchService;
        }

        public IActionResult OnGet()
        {
        ViewData["ProductId"] = new SelectList(_productService.GetAll(), "Id", "ProductName");
            return Page();
        }

        [BindProperty]
        public Batch Batch { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            _batchService.Add(Batch);
            _batchService.Save();

            return RedirectToPage("./Index");
        }
    }
}
