namespace AuthService.Domain.Exceptions
{
    public class CredencialesInvalidasException : Exception
    {
        public CredencialesInvalidasException()
            : base("Credenciales invalidas.")
        {
        }
    }
}
