using BlogApi.Models;
using BlogApi.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloggerController : ControllerBase
    {
        [HttpPost]
        public ActionResult AddNewBlogger(AddBloggerDto addBloggerDto)
        {
            try
            {
                var blogger = new Blogger
                {
                    Name = addBloggerDto.Name,
                    Password = addBloggerDto.Password,
                    Email = addBloggerDto.Email
                };

                if (blogger != null)
                {
                    using (var context = new BlogDbContext())
                    {
                        context.bloggers.Add(blogger);
                        context.SaveChanges();
                        return StatusCode(201, new { message = "Sikeres hozzáadás", result= blogger});
                    }
                }

                return StatusCode(404, new { message = "Sikertelen hozzáadás", result = blogger });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = ex.Message, result = "" });
            }
        }

        [HttpGet]
        public ActionResult GetAllBlogger()
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var bloggers = context.bloggers.ToList();

                    if (bloggers != null)
                        return Ok(new { message = "Sikeres lekérdezés", result = bloggers});

                    return NotFound(new { message = "Sikertelen lekérdezés", result = bloggers });
                }
                   
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, result = "" });
            }
        }

        [HttpGet("byid")]
        public ActionResult GetBloggerById(int id) 
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var blogger = context.bloggers.FirstOrDefault(x => x.Id == id);

                    if (blogger != null)
                    {
                        return Ok(new { message = "Sikeres lekérdezés", result = blogger });
                    }

                    return NotFound(new { message = "Sikertelen lekérdezés", result = blogger });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, result = "" });
            }
        }
    }
}
