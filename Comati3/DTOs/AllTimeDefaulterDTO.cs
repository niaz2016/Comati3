namespace Comati3.DTOs
{
    public class AllTimeDefaulterDTO : BaseDTO
    {
        public string Name { get; set; } = null!;
        public int MemberId { get; set; }
        public int TotalPaid { get; set; }
        public int AmountOverdue { get; set; }

    }
}
