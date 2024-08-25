namespace TodoApp.Data.Entites
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime EditedTime { get; set; }
        public Guid UserId { get; set; }
        public int StatusId { get; set; }
        public int PriorityId { get; set; }
        public User User { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
    }
}
