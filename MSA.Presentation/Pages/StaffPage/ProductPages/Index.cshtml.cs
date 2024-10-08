﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MSA.Application.IServices;
using MSA.Application.Services;
using MSA.Domain.Entities;
using MSA.Domain.Enums;
using MSA.Infrastructure;
using Services;

namespace MSA.Presentation.Pages.ProductPages
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBatchService _batchService;

        public IndexModel(IProductService productService, ICategoryService categoryService,IBatchService batchService )
        {
            _productService = productService;
            _categoryService = categoryService;
            _batchService = batchService;
        }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public IList<Product> Product { get;set; } = default!;
        public List<string> CategoryName { get; set; } = new List<string>();
        public List<string> Quantity { get; set; } = new List<string>();
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.Session.GetString("role");
            if (role != "Staff" || role == null)
            {
                return RedirectToPage("/AccessDenied");
            }
            const int pageSize = 5;

            IEnumerable<Product> productsQuery = _productService.GetAll().Where(x => !x.IsDeleted);

            if (!string.IsNullOrEmpty(SearchString))
            {
                productsQuery = productsQuery.Where(p => p.ProductName.Contains(SearchString));
            }

            int totalProducts = productsQuery.Count();
            TotalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

            Product = productsQuery
                .Skip((CurrentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            foreach (var product in Product)
            {
                var category = _categoryService.GetById(product.CategoryId);
                CategoryName.Add(category?.CategoryName ?? "Unknown");
                var quantity = _batchService.GetAllByProductId(product.Id).Sum(x => x.Quantity);
                Quantity.Add(quantity.ToString() ?? "Unknown");
            }
            return Page();                
        }   
    }
}
