using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApi.Models
{
    public class Blogger
    {
        [Key]
        public int Id { get; set; }
        
        [Column(TypeName = "varchar(30)")]
        public string Name { get; set; }
        
        [Column(TypeName = "text")]
        public string Password { get; set; }
        
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        public DateTime RegTime { get; set; } = DateTime.Now;
        public DateTime ModDate { get; set; } = DateTime.Now;

        //Kapcsolat kialakítása
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
