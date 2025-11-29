using BlogApi.Models;
using BlogApi.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                        return StatusCode(201, new { message = "Sikeres hozzáadás", result = blogger });
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
                        return Ok(new { message = "Sikeres lekérdezés", result = bloggers });

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

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var blogger = context.bloggers.FirstOrDefault(x => x.Id == id);

                    if (blogger != null)
                    {
                        context.bloggers.Remove(blogger);
                        context.SaveChanges();
                        return Ok(new { message = "Sikeres törlés", result = blogger });
                    }

                    return NotFound(new { message = "Sikertelen törlés", result = blogger });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, result = "" });
            }
        }

        [HttpPut]
        public ActionResult Put(int id, UpdateBloggerDto updateBloggerDto)
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var exstingBlogger = context.bloggers.FirstOrDefault(x => x.Id == id);

                    if (exstingBlogger != null)
                    {
                        exstingBlogger.Name = updateBloggerDto.Name;
                        exstingBlogger.Password = updateBloggerDto.Password;
                        exstingBlogger.Email = updateBloggerDto.Email;
                        exstingBlogger.ModDate = DateTime.Now;

                        context.bloggers.Update(exstingBlogger);
                        context.SaveChanges();

                        return Ok(new { message = "Sikeres módosítás", result = exstingBlogger });
                    }

                    return NotFound(new { message = "Sikertelen módosítás", result = exstingBlogger });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, result = "" });
            }
        }

        //Összetett lekérdezés
        //Összes blogger adat összes post-al

        [HttpGet("allBloggerAllPost")]
        public ActionResult GetAllBloggerAllPosts()
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var bloggersWithPosts = context.bloggers.Include(x => x.Posts).ToList();

                    if (bloggersWithPosts != null)
                    {
                        return Ok(new { message = "Sikeres lkérdezés", result = bloggersWithPosts });
                    }

                    return NotFound(new { message = "Sikertelen lekérdezés", result = bloggersWithPosts });

                }

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, result = "" });
            }
        }

        [HttpGet("bloggerWithOwnPost")]
        public ActionResult GetBloggerWithOwnPost(int id) 
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var bloggerWithOwnPost = context.bloggers.Include(x => x.Posts).FirstOrDefault(x => x.Id == id);

                    if (bloggerWithOwnPost != null)
                    {
                        return Ok(new { message = "Sikeres lkérdezés", result = bloggerWithOwnPost });
                    }

                    return NotFound(new { message = "Sikertelen lekérdezés", result = bloggerWithOwnPost });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, result = "" });
            }
        }

        [HttpGet("bloggerNameAndCategories")]
        public ActionResult GetBloggerNameAndCategories(int id)
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var bloggerNameAndCategories = context.bloggers
                        .Include(x => x.Posts)
                        .FirstOrDefault(y => y.Id == id);

                 
                    if (bloggerNameAndCategories != null)
                    {
                        var result = new
                        {
                            BloggerName = bloggerNameAndCategories.Name,
                            Contents = bloggerNameAndCategories.Posts
                              .Select(x => x.Content)
                              .Distinct()
                              .ToList()
                        };
                        return Ok(new { message = "Sikeres lkérdezés", result = result });
                    }

                    return NotFound(new { message = "Sikertelen lekérdezés", result = "" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, result = "" });
            }
        }
    }
}