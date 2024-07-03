using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Comati3.Models
{
    public class Person : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Remarks { get; set; }

        public ICollection<Comati>? ComatisManaged { get; set; }
        public ICollection<ComatiMember> ComatiMemberships { get; set; }=null!;


    }
}
