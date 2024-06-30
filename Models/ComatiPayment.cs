using System.ComponentModel.DataAnnotations.Schema;

namespace Comati3.Models
{
    public class ComatiPayment: BaseEntity
    {
        public int Amount { get; set; }
        [ForeignKey("ComatiId")]
        public int ComatiId { get; set; }
        
        public Comati? Comati { get; set; }
        [ForeignKey("PersonId")]
        public int PersonId { get; set; }
        public Person? Person { get; set; }
        public DateTime PaymentDate { get; set; }

    }
}
