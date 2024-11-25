using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ForumMotor_13BC_H.Data;
using ForumMotor_13BC_H.Models;

namespace ForumMotor_13BC_H.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ForumMotor_13BC_H.Data.ApplicationDbContext _context;

        public IndexModel(ForumMotor_13BC_H.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Category = await _context.Categories
                .Include(c => c.User).ToListAsync();
        }
    }
}
