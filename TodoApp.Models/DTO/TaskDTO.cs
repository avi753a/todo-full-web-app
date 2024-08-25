using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class TaskDTO
    {
        [Required,MinLength(3)]
        public string Name { get; set; }
        [Required,MinLength(5)]
        public string Description { get; set; }
        [Required]
        public int PriorityId { get; set; }
        [Required]
        public int StatusId { get; set; }

    }
}
