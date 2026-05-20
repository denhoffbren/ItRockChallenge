namespace AuthService.Domain.Exceptions
{
    public class UsuarioInexistenteException : Exception
    {
        public UsuarioInexistenteException()
            : base("Usuario no encontrado.")
        {
        }
    }
}
