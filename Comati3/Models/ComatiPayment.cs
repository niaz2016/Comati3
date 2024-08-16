using System.ComponentModel.DataAnnotations.Schema;

namespace Comati3.Models
{
    public class ComatiPayment: BaseEntity
    {
        [ForeignKey("ComatiId")]
        public int ComatiId { get; set; }
        public Comati? Comati { get; set; }
        
        [ForeignKey("MemberId")]
        public int MemberId { get; set; }
        public ComatiMember ComatiMember { get; set; } = null!;
        public int Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? Remarks { get; set; }

    }
}
