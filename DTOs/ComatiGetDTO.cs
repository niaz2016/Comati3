namespace Comati3.DTOs
{
    public class ComatiGetDTO : BaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public string? Remarks { get; set; }
        public int ManagerId { get; set; }
        public int TotalMembers { get; set; }
        public int TotalComati { get; set; }
        public int Per_Head { get; set; }
    }
}
