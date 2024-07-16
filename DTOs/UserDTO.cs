namespace Comati3.DTOs
{
    public class UserDTO : BaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Address { get; set; }
        public string? Remarks { get; set; }

    }
}
