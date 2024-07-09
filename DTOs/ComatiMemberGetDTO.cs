namespace Comati3.DTOs
{
    public class ComatiMemberGetDTO : BaseDTO
    {
        public int Id { get; set; } // necessary to  post Payment against member
        public string ComatiName { get; set; }=null!;
        public int ComatiMemberNo { get; set; }
        public int Amount { get; set; }
        public DateTime OpeningMonth { get; set; }
        public string? Remarks { get; set; }
        public string Name { get; internal set; } = null!;
    }
}
