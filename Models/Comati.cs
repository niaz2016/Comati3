using System.ComponentModel.DataAnnotations;
namespace Comati3.Models
{
    public class Comati : BaseEntity
    {
        public string Name { get; set; } = null!;
        public DateTime Start_Date { get; set; }
        public int Per_Head { get; set; }

        public int? ManagerId { get; set; }
        public Person? Manager { get; set; }
        public ICollection<ComatiMember>? Members { get; set; }
        public ICollection<ComatiPayment>? Payments { get; set; }
        public DateTime? End_Date
        {
            get
            {
                double? months = Members.Sum(member => member.Amount) / Per_Head;
                if (Convert.ToInt32(months) == months.Value)
                {
                    return Start_Date.AddMonths(Convert.ToInt32(months)).Date;
                }
                else
                {
                    return null;
                }
            }
        }

    }

}
