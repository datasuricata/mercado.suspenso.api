using System;
using System.Linq;

namespace mercadosuspenso.domain.Extensions
{
    public static class IRandomExtension
    {
        public static string RandomLetter(this int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
