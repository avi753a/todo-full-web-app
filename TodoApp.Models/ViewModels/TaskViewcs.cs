namespace TodoApp.Models
{
    public class TaskView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime EditedTime { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string PriorityValue { get; set; }


    }
}

