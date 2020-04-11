using System;

namespace mercadosuspenso.domain.Exceptions
{
    public class Domain : ApplicationException
    {
        public Domain()
        {
        }

        public Domain(string message) : base(message)
        {
        }

        public Domain(string message, Exception innerException) : base(message, innerException)
        {
        }

        public static void Validate(bool error, string message)
        {
            if (error)
                throw new Domain(message);
        }
    }
}