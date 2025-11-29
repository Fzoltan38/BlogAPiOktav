using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApi.Models.Dtos
{
    public class AddPostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public int BloggerId { get; set; }

    }
}
