using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Chapter: BaseEntity
    {
        public int CourseId { get; set; }
        public Course? Course { get; set; }

        [Required]
        public string List { get; set; } = string.Empty;
    }
}
