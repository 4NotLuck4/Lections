using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lection1202.Contexts;
using Lection1202.Models;

namespace Lection1202.Pages
{
    public class LoginModel : PageModel
    {
        private readonly Lection1202.Contexts.GamesDbContext _context;

        public LoginModel(Lection1202.Contexts.GamesDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnGetLogout()
        {
            return RedirectToPage("/Categories/Index");
        }


        [BindProperty]
        public User User { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        public IActionResult OnPostGuest()
        {
            return RedirectToPage("/Games/Index");
        }
    }
}
