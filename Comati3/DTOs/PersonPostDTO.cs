using Comati3.Models;

namespace Comati3.DTOs
{
    public class PersonPostDTO : BaseDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; }=null!;
        public string Address { get; set; } = null!;
        public string? Remarks { get; set; }
        public int Mgr { get; set; }
        public ICollection<ComatiMemberGetDTO>? MemberShips { get; internal set; }
    }
}
