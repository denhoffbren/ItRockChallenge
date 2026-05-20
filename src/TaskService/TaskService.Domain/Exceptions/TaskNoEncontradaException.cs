namespace TaskService.Domain.Exceptions
{
    public class TaskNoEncontradaException : Exception
    {
        public TaskNoEncontradaException()
            : base("Tarea no encontrada.")
        {
        }
    }
}
