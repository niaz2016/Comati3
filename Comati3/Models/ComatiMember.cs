using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comati3.Models
{
    public class ComatiMember : BaseEntity
    {
        [ForeignKey("ComatiId")]
        public int ComatiId { get; set; }
        public Comati Comati { get; set; } = null!;
        [ForeignKey("PersonId")]
        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;
        public int Amount {  get; set; }
        public DateTime OpeningMonth { get; set; }
        public string? Remarks { get; set; }
        public ICollection<ComatiPayment> ComatiPayments { get; set; }

    }
}
