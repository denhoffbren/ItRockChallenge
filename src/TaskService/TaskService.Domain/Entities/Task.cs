namespace TaskService.Domain.Entities
{
    public class Tarea
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool Completed { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; } = true;
    }
}
