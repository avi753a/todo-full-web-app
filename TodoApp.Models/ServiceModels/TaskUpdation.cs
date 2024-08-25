

namespace TodoApp.Models
{
    public class TaskUpdation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedTime { get; set; }
        public int Status {  get; set; }
        
    }
}
