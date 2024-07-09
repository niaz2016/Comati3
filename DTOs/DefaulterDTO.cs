namespace Comati3.DTOs
{
    public class DefaulterDTO
    {
        public int MemberId { get; set; }
        public int ComatiId { get; internal set; }
        public string Name { get; set; } = null!;
        public int ComatiMemberNo { get; set; }
        public string Phone {  get; set; }=null!;
        public int Amount { get; set; }
        public bool IsNotPaid { get; set; }
        public string? Address { get; internal set; }
        public string? Remarks { get; internal set; }
    }
}
