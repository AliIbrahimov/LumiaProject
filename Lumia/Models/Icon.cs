namespace Lumia.Models
{
    public class Icon
    {
        public int Id { get; set; }
        public string? IconName { get; set; }
        public int TeamMemberId { get; set; }
        public TeamMember? TeamMember { get; set; }
    }
}
