namespace Comati3.DTOs
{
    public class PersonPostDTO : BaseDTO
    {
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Remarks { get; set; }
    }
}
