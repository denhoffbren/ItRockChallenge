namespace TaskService.Domain.Exceptions
{
    public class NoTienePermisosExcepcion : Exception
    {
        public NoTienePermisosExcepcion()
            : base("No tiene permisos")
        { 
        }
    }
}
