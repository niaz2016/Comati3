using System.ComponentModel.DataAnnotations.Schema;
namespace Comati3.Models
{
    using Comati3.DTOs;
    public class DashboardDataGetDTO
    {
        public int AmountCollected { get; set; }
        public int MembersRemaining { get; set; }
        public ICollection<ComatiMember>? ComatiMember { get; set; }
        public ComatiMember? ThisMonth { get; set; }
        public string? Remarks { get; set; }
    }
}