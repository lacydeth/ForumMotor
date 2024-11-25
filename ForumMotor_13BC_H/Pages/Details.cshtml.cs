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
    public class DetailsModel : PageModel
    {
        private readonly ForumMotor_13BC_H.Data.ApplicationDbContext _context;

        public DetailsModel(ForumMotor_13BC_H.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Category = category;
            }
            return Page();
        }
    }
}
