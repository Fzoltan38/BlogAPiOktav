using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogApi.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string Title { get; set; }
        public string Content { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string Category { get; set; }
        public DateTime PostTime { get; set; } = DateTime.Now;

        //Kapcsolat kialakítása
        public int BloggerId { get; set; } //Foreign Key

        [JsonIgnore]
        public Blogger Blogger { get; set; }
    }
}
