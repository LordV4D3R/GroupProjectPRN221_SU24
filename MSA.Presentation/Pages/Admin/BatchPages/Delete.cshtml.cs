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
using Services;

namespace MSA.Presentation.Pages.Admin.BatchPages
{
    public class DeleteModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IBatchService _batchService;

        public DeleteModel(IProductService productService, IBatchService batchService)
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

            var batch = _batchService.GetById(id);

            if (batch == null)
            {
                return NotFound();
            }
            else
            {
                Batch = batch;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var batch = _batchService.GetById(id);
            if (batch != null)
            {
                Batch = batch;
                _batchService.Delete(batch);
                _batchService.Save();
            }

            return RedirectToPage("./Index");
        }
    }
}
