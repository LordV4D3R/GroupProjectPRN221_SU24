﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
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

            var batch =  _batchService.GetById(id);
            if (batch == null)
            {
                return NotFound();
            }
            Batch = batch;
            Guid productId = Batch.ProductId;
           ViewData["ProductName"] = new SelectList(_productService.GetAll().Where(x => x.Id == productId), "Id", "ProductName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (Batch.ExpOn <= DateTime.Now)
            {
                ModelState.AddModelError("Batch.ExpOn", "Expiration date must be in the future.");
                ViewData["ProductName"] = new SelectList(_productService.GetAll().Where(x => x.Id == Batch.ProductId), "Id", "ProductName");
                return Page();
            }
            _batchService.Update2(Batch);
            return RedirectToPage("/StaffPage/ProductPages/Details", new { id = Batch.ProductId });
        }
    }
}
