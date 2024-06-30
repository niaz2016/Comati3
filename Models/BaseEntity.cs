using System.ComponentModel.DataAnnotations;

namespace Comati3.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;  
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }=false;
    }
}
