using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
