using BlogApi.Models;
using BlogApi.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        [HttpPost]
        public ActionResult AddNewPost(AddPostDto addPostDto)
        {
            try
            {
                var post = new Post 
                { 
                    Title = addPostDto.Title,
                    Content = addPostDto.Content,
                    Category = addPostDto.Category,
                    BloggerId = addPostDto.BloggerId
                };

                using (var context = new BlogDbContext())
                {
                    if (post != null)
                    {
                        context.posts.Add(post);
                        context.SaveChanges();

                        return StatusCode(201, new { message = "Sikeres hozzáadás", result = post });
                    }

                    return StatusCode(404, new { message = "Sikertelen hozzáadás", result = post });
                }
                   
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = ex.Message, result = "" });
            }
        }

        [HttpGet]
        public ActionResult GetAllPost()
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    return StatusCode(200, new { message = "Sikeres lekérdezés", result = context.posts.ToList() });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = ex.Message, result = "" });

            }
        }

        [HttpGet("byid")]
        public ActionResult GetPostById(int id)
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var post = context.posts.FirstOrDefault(x => x.Id == id);

                    if (post != null)
                    {
                        return StatusCode(200, new { message = "Sikeres lekérdezés", result = post });
                    }
                    return StatusCode(404, new { message = "Nincs ilyen Id.", result = post });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(404, new { message = ex.Message, result = "" });
            }
        }

        [HttpDelete]
        public ActionResult DeletePost(int id) 
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var post = context.posts.FirstOrDefault(y => y.Id == id);

                    if (post != null)
                    {
                        context.posts.Remove(post);
                        context.SaveChanges();

                        return StatusCode(200, new { message = "Sikeres törlés", result = post });
                    }

                    return StatusCode(404, new { message = "Nincs ilyen Id.", result = post });

                }
            }
            catch (Exception ex)
            {
                return StatusCode(404, new { message = ex.Message, result = "" });
            }
        }

        [HttpPut]
        public ActionResult PutPost(int id, UpdatePostDto updatePost) 
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var post = context.posts.FirstOrDefault(x => x.Id == id);

                    if(post != null)
                    {
                        post.Title = updatePost.Title;
                        post.Content = updatePost.Content;
                        post.Category = updatePost.Category;

                        context.posts.Update(post);
                        context.SaveChanges();

                        return StatusCode(200, new { message = "Sikeres frissítés", result = post });
                    }

                    return StatusCode(404, new { message = "Nincs ilyen Id.", result = post });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(404, new { message = ex.Message, result = "" });
            }
        }

    }
}
