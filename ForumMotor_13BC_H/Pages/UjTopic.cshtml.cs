using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ForumMotor_13BC_H.Data;
using ForumMotor_13BC_H.Models;
using Microsoft.AspNetCore.Identity;

namespace ForumMotor_13BC_H.Pages
{
    public class UjTopicModel : PageModel
    {
        private readonly ForumMotor_13BC_H.Data.ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UjTopicModel(ForumMotor_13BC_H.Data.ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty(SupportsGet = true)]
        public int CategoryId { get; set; }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Topic Topic { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid)
            {
                return Page();
            }*/
            Topic.CategoryId = CategoryId;
            Topic.CreateDate = DateTime.Now;
            Topic.UserId = _userManager.GetUserId(User);
            _context.Topics.Add(Topic);
            await _context.SaveChangesAsync();

            return RedirectToPage($"./TopicList", new { CategoryId = CategoryId});
        }
    }
}
