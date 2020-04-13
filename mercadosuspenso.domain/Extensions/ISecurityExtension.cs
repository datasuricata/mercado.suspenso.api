using System.Security.Cryptography;
using System.Text;

namespace mercadosuspenso.domain.Extensions
{
    public static class ISecurityExtension
    {
        public static string EncryptToMD5(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            var builder = new StringBuilder();
            foreach (var x in MD5.Create().ComputeHash(Encoding.Default.GetBytes(value)))
                builder.Append(x.ToString("x2"));
            return builder.ToString().ToUpper();
        }
    }
}