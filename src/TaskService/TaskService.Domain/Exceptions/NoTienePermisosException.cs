namespace TaskService.Domain.Exceptions
{
    public class NoTienePermisosException : Exception
    {
        public NoTienePermisosException()
            : base("No tiene permisos")
        { 
        }
    }
}
