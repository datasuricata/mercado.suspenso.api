using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mercadosuspenso.orm.Extensions
{
    public static class IBuilderExtensions
    {
        public static void UseAssemblyMapping(this ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(MercadoDbContext).Assembly);
        }

        public static void UseDeleteCascadeOff(this ModelBuilder builder)
        {

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                entityType.GetForeignKeys().Where(mutable => !mutable.IsOwnership && mutable.DeleteBehavior == DeleteBehavior.Cascade).ToList()
                    .ForEach(mutable => mutable.DeleteBehavior = DeleteBehavior.Restrict);
            }
        }
    }
}
