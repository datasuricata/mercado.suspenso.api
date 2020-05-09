using System;
using System.Text.RegularExpressions;

namespace mercadosuspenso.domain.Extensions
{
    public static class IMaskExtension
    {
        public static string CleanFormat(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return value.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
        }

        public static string CleanStripHTML(this string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
    }
}