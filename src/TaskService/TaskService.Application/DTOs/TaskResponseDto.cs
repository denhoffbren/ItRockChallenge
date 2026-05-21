namespace TaskService.Application.DTOs
{
    public class TaskResponseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
