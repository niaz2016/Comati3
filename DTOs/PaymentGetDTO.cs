using Comati3.Models;

namespace Comati3.DTOs
{
    public class PaymentGetDTO
    {
        public DateTime PaymentDate { get; set; }
        public int Amount { get; set; }
        public string? Remarks { get; set; }
        public ICollection<ComatiPayment>? ComatiPayments { get; set; }
    }
}
