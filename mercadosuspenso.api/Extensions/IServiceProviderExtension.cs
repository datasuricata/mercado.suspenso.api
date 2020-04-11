using Microsoft.Extensions.DependencyInjection;
using System;

namespace mercadosuspenso.api.Extensions
{
    public static class IServiceProviderExtension
    {
        public static T GetService<T>(this IServiceProvider provider) => (T)provider.GetService(typeof(T));

        public static T GetRequiredService<T>(this IServiceProvider provider) => (T)provider.GetRequiredService(typeof(T));
    }
}