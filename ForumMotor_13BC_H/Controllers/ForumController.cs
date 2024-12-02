using ForumMotor_13BC_H.Data;
using ForumMotor_13BC_H.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumMotor_13BC_H.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ForumController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ForumController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Category Endpoints

        // Get all categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }

        // Get category by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Topics)  // Include related topics
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // Create a new category
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        #endregion

        #region Topic Endpoints

        // Get all topics in a category
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<IEnumerable<Topic>>> GetTopicsByCategory(int categoryId)
        {
            var topics = await _context.Topics
                .Where(t => t.CategoryId == categoryId)
                .Include(t => t.Posts)  // Include posts in the topic
                .ToListAsync();

            return Ok(topics);
        }

        // Get a specific topic
        [HttpGet("{id}")]
        public async Task<ActionResult<Topic>> GetTopic(int id)
        {
            var topic = await _context.Topics
                .Include(t => t.Posts) // Include related posts
                .Include(t => t.Category) // Include category info
                .FirstOrDefaultAsync(t => t.Id == id);

            if (topic == null)
            {
                return NotFound();
            }

            return Ok(topic);
        }

        // Create a new topic
        [HttpPost]
        public async Task<ActionResult<Topic>> CreateTopic([FromBody] Topic topic)
        {
            if (topic == null)
            {
                return BadRequest();
            }

            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTopic), new { id = topic.Id }, topic);
        }

        #endregion

        #region Post Endpoints

        // Get all posts in a topic
        [HttpGet("{topicId}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByTopic(int topicId)
        {
            var posts = await _context.Posts
                .Where(p => p.TopicId == topicId)
                .Include(p => p.User)  // Include user info who created the post
                .ToListAsync();

            return Ok(posts);
        }

        // Create a new post
        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost([FromBody] Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }

            post.CreateDate = DateTime.UtcNow;
            post.UpdateDate = DateTime.UtcNow;

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPostsByTopic), new { topicId = post.TopicId }, post);
        }

        #endregion

    }
}
