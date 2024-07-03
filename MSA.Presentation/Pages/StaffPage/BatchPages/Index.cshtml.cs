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
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IBatchService _batchService;

        public IndexModel(IProductService productService, IBatchService batchService)
        {
            _productService = productService;
            _batchService = batchService;
        }

        public IList<Batch> Batch { get;set; } = default!;
        public List<string> ProductName { get; set; } = new List<string>();
        public Guid productId { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var role = HttpContext.Session.GetString("role");
            if (role != "Staff" || role == null)
            {
                return RedirectToPage("/AccessDenied");
            }
            else
            {
                Batch = _batchService.GetAll().Where(x => x.ProductId == id && x.IsDeleted == false).ToList();
                productId = id;
                foreach (var batch in Batch)
                {
                    var product = _productService.GetById(batch.ProductId);
                    ProductName.Add(product?.ProductName ?? "Unknown");
                }
                return Page();
            }

        }
    }
}
