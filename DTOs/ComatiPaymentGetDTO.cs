using System.ComponentModel.DataAnnotations.Schema;

namespace Comati3.DTOs
{
    public class ComatiPaymentGetDTO
    {
        public int Amount { get; set; }
        [ForeignKey("ComatiId")]
        public int ComatiId { get; set; }
        public int MemberId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? Remarks { get; set; }
    }
}
