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
    public class PostListModel : PageModel
    {
        private readonly ForumMotor_13BC_H.Data.ApplicationDbContext _context;

        public PostListModel(ForumMotor_13BC_H.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty(SupportsGet = true)]
        public int TopicId { get; set; }
        public IList<Post> Post { get;set; } = default!;
        public Category Category { get; set; }
        public Topic Topic { get; set; }
        public async Task OnGetAsync()
        {
            Topic = _context.Topics.Find(TopicId);
            Category = _context.Categories.Find(Topic.CategoryId);
            Post = await _context.Posts
                .Where(x => x.TopicId == TopicId)
                .Include(p => p.Topic)
                .Include(p => p.User).ToListAsync();
        }
    }
}
