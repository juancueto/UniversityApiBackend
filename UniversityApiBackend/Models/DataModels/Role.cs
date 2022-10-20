using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Role: BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; }


        public ICollection<User> Users { get; set; }
    }
}
