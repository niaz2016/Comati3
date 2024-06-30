using System.ComponentModel.DataAnnotations;
namespace Comati3.Models
{
    public class Comati : BaseEntity
    {
        public string Name { get; set; } = null!;
        public DateTime Start_Date { get; set; }
        public DateTime? End_Date { get; set; }
        public int Per_Head { get; set; }

        public int? ManagerId { get; set; }
        public Person? Manager { get; set; }
        public ICollection<ComatiMember>? Members { get; set; }
        public ICollection<ComatiPayment>? Payments { get; set; }

    }

}
