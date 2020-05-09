using System;

namespace mercadosuspenso.domain.Exceptions
{
    public class DomainException : ApplicationException
    {
        public DomainException()
        {
        }

        public DomainException(string message) : base(message)
        {
        }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public static void Validate(bool error, string message)
        {
            if (error)
                throw new DomainException(message);
        }
    }
}