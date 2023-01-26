using System.ComponentModel.DataAnnotations.Schema;

namespace Lumia.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? FormFIle { get; set; }
        public List<Icon>? Icons { get; set; }

    }
}
