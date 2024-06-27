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
    public class DetailsModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IBatchService _batchService;

        public DetailsModel(IProductService productService, IBatchService batchService)
        {
            _productService = productService;
            _batchService = batchService;
        }

        public Batch Batch { get; set; } = default!;
        public string ProductName { get; set; } = string.Empty;

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
                var product = _productService.GetById(batch.ProductId);
                ProductName = product?.ProductName ?? "Unknown";
            }
            return Page();
        }
    }
}
