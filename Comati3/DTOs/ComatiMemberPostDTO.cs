using Comati3.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comati3.DTOs
{
    public class ComatiMemberPostDTO : BaseDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int ComatiId { get; set; }
        public int PersonId { get; set; }
        public int Amount { get; set; }
        public DateTime? OpeningMonth { get; set; }
        public string? Remarks  { get; set; }
    }
}
