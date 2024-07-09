namespace Comati3.DTOs
{
    public class ComatiPostDTO : BaseDTO
    {
        public string Name { get; set; } = null!;
        public DateTime Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public int Per_Head { get; set; }
        public string? Remarks { get; set; }
        public int ManagerId { get; set; }

    }
}
