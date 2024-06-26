using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MSA.Domain.Entities;
using MSA.Infrastructure;
using Services;

namespace MSA.Presentation.Pages.CategoryPages
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _context;

        public IndexModel(ICategoryService context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                Category = _context.SearchByName(SearchString).ToList();
            }
            else
            {
                Category = _context.GetAll().ToList();
            }
        }
    }
}
