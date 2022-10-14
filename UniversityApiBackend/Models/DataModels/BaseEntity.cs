using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityApiBackend.Models.DataModels
{
    public abstract class BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }

        //[Column("CreatedBy")]
        //public int CreatedById { get; set; }
        //public virtual User CreatedBy { get; set; } = new User();
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //[Column("UpdatedBy")]
        //public int? UpdatedById { get; set; }
        //public virtual User UpdatedBy { get; set; } = new User();
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        //[Column("DeletedBy")]
        //public int? DeletedById { get; set; }
        public string DeletedBy { get; set; } = string.Empty;
        public DateTime? DeletedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;
    }
}
