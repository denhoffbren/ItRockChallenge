namespace AuthService.Domain.Exceptions
{
    public class UsuarioRegistradoException : Exception
    {
        public UsuarioRegistradoException()
            : base("Usuario registrado.")
        {
        }
    }
}
