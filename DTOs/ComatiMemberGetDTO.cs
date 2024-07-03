namespace Comati3.DTOs
{
    public class ComatiMemberGetDTO : BaseDTO
    {
        public int ComatiId { get; set; }
        public string Name { get; set; } = null!;
        public int Amount { get; set; }
        public DateTime OpeningMonth { get; set; }
        public string? Remarks { get; set; }

    }
}
