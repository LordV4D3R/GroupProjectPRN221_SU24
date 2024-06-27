﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSA.Application.IServices;
using MSA.Domain.Dtos.Account;
using MSA.Domain.Dtos.Batch;
using MSA.Domain.Entities;
using MSA.Domain.Enums;
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

        public IActionResult OnGet(Guid id)
        {
        ViewData["ProductId"] = new SelectList(_productService.GetAll().Where(x => x.Id == id), "Id", "ProductName");
            return Page();
        }

        [BindProperty]
        public BatchVM request { get; set; } = new BatchVM();

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(BatchVM request)
        {
            Batch newBacth = new Batch
            {
                Quantity = request.Quantity,
                ExpOn = request.ExpOn,
                CreatedOn = DateTime.Now,
                Status = BatchStatus.Active,
                ProductId = request.ProductId
            };
            _batchService.Add(newBacth);
            _batchService.Save();

            return RedirectToPage("./Index", new { id = request.ProductId });
        }
    }
}
