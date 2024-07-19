namespace Comati3.DTOs
{
    public class ComatiMemberGetDTO : BaseDTO
    {
        public int Id { get; set; } // necessary to  post Payment against member
        public int ComatiId { get; set; }
        public string ComatiName { get; set; } = null!;
        public int PersonId { get; set; }
        public PersonsGetDTO Person { get; set; }=null!;
        public int Amount { get; set; }
        public string Name { get; set; } = null!;
        public DateTime OpeningMonth { get; set; }
        public string? Remarks { get; set; }
        public ICollection<DefaulterDTO> Payments { get; set; } = null!;
    }
}
