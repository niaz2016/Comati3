using System.ComponentModel.DataAnnotations.Schema;

namespace Comati3.Models
{
    public class ComatiMember : BaseEntity
    {
        [ForeignKey("ComatiId")]
        public int ComatiId { get; set; }
        
        public Comati? Comati { get; set; }
        [ForeignKey("PersonId")]
        public int PersonId { get; set; }
        
        public Person? Person { get; set; }
        public int? Amount {  get; set; }
    }
}
