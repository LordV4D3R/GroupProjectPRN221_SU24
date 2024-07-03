using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MSA.Domain.Entities;
using MSA.Infrastructure;
using MSA.Infrastructure.Services;

namespace MSA.Presentation.Pages.VoucherPages
{
    public class IndexModel : PageModel
    {
        private readonly IVoucherService _context;

        public IndexModel(IVoucherService context)
        {
            _context = context;
        }

        public IList<Voucher> Voucher { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Voucher = _context.GetAll().ToList() ;
        }
    }
}
