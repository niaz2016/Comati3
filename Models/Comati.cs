using System.ComponentModel.DataAnnotations.Schema;
namespace Comati3.Models
{
    public class Comati : BaseEntity
    {
        public string Name { get; set; } = null!;
        public DateTime Start_Date { get; set; }
        public int Per_Head { get; set; }
        [ForeignKey("PersonId")]
        public int ManagerId { get; set; }
        public Person? Manager { get; set; } // must be null to post a comati
        public ICollection<ComatiMember>? Members { get; set; }
        public ICollection<ComatiPayment>? Payments { get; set; }
        public string? Remarks { get; set; }
    }

}