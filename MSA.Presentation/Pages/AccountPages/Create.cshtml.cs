﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSA.Application.IServices;
using MSA.Domain.Account;
using MSA.Domain.Dtos.Account;
using MSA.Domain.Entities;
using MSA.Domain.Enums;
using MSA.Infrastructure;

namespace MSA.Presentation.Pages.AccountPages
{
    public class CreateModel : PageModel
    {
        private readonly IAccountService _accountService;
        public CreateModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; } = default!;
        public CreateAccountRequest request { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(CreateAccountRequest request)
        {
            Account newAccount = new Account
            {
                Username = request.Username,              
                Password = request.Password,
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                ImageUrl = request.ImageUrl,
                Role = request.Role,
                Status = AccountStatus.Active,
            };
            _accountService.Add(newAccount);
            _accountService.Save();

            return RedirectToPage("./Index");
        }
    }
}
