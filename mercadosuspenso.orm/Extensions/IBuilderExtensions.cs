using mercadosuspenso.domain.Enums;
using mercadosuspenso.domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public static void UseDataSeeder(this ModelBuilder builder)
        {
            builder.Usuarios().Enderecos().Distribuidores().Varejistas().Participantes().Vinculos();
        }

        private static ModelBuilder Usuarios(this ModelBuilder builder)
        {
            builder.Entity<Usuario>().HasData
            (
                new Usuario(UsuarioTipo.Administrador)
                {
                    Id = "d87e4de503984cfd8b282616ba50b42f",
                    Email = "lucas.moraes@datasuricata.com.br",
                    Senha = "C3F21A70DFB5072C152691AF019CC68A",
                },
                new Usuario(UsuarioTipo.Administrador)
                {
                    Id = "e0176f9f9b0445de83411b404e68a311",
                    Email = "equipegkio@gmail.com",
                    Senha = "2D27FE16A2DE63872DDEC0BAFFB473BC",
                },
                new Usuario(UsuarioTipo.Administrador)
                {
                    Id = "c9f9cd17020c4bfaa83f7c53e5f9b278",
                    Email = "web.admin@mailinator.com",
                    Senha = "6C5F89C867E08F99BE95D3CBA4B88594",
                },
                new Usuario("jeniffer.restaurante@restaurante.com", "123456")
                {
                    Id = "7d5ea14a85ba4af7a4ca4c95545f3545",
                    Tipo = UsuarioTipo.Distribuidor
                },
                new Usuario("carlos@pizzaria.com", "123456")
                {
                    Id = "2580868e52444603b4d75fdf92f7f916",
                    Tipo = UsuarioTipo.Distribuidor
                },
                new Usuario("olivia@noturna.com.br", "123456")
                {
                    Id = "4bea229e5b5a4d66a75d1d248f7dc2d5",
                    Tipo = UsuarioTipo.Varejo
                }
            );

            return builder;
        }

        private static ModelBuilder Distribuidores(this ModelBuilder builder)
        {
            builder.Entity<Distribuidor>().HasData
                (
                    new Distribuidor("Luiza e Jennifer Restaurante Ltda", "Jennifer Rocha", "46992525000151", "1129551395")
                    {
                        Id = "cf936849e0b244a2a6a812c15d69390d",
                        EnderecoId = "eff3b1b9b3b742a6b5ba4fffdf99585e",
                        UsuarioId = "7d5ea14a85ba4af7a4ca4c95545f3545",
                    },
                    new Distribuidor("LAndré e Mateus Pizzaria Delivery Ltda", "Carlos Matues", "99270944000146", "1128810961")
                    {
                        Id = "189be3c445504dcf9d8559959579035e",
                        EnderecoId = "5d2387e4b93f476c81dd1bb757b27f43",
                        UsuarioId = "2580868e52444603b4d75fdf92f7f916",
                    }
                );

            return builder;
        }

        private static ModelBuilder Enderecos(this ModelBuilder builder)
        {
            builder.Entity<Endereco>().HasData
                (
                    new Endereco("04747110", "Rua Santo Aristides", "23", "SALA 02", "Centro", "São Paulo", "SP") { Id = "eff3b1b9b3b742a6b5ba4fffdf99585e" },
                    new Endereco("04028003", "Avenida Ibirapuera", "334", "Conj. 01", "Indianópolis", "São Paulo", "SP") { Id = "5d2387e4b93f476c81dd1bb757b27f43" },
                    new Endereco("09171686", "Rua Santa Adélia", "35", "02", "Sitio dos Vianas", "São Paulo", "SP") { Id = "010bafef4b7342bda843d22b1ec9d7c2" },
                    new Endereco("80020040", "Muricy 73", "32", "32", "Centro", "Curitiba", "PR") { Id = "39f3a4d569ea400f848dd71deed5eb82" }
                );

            return builder;
        }

        private static ModelBuilder Varejistas(this ModelBuilder builder)
        {
            builder.Entity<Varejista>().HasData
                (
                    new Varejista("Olivia e Raimundo Casa Noturna ME", "Oliveira Moraes", "18904128000145", "1125262748")
                    {
                        Id = "eeb45a8bc09e422dbfd45b456a78f878",
                        EnderecoId = "010bafef4b7342bda843d22b1ec9d7c2",
                        UsuarioId = "4bea229e5b5a4d66a75d1d248f7dc2d5",
                    }
                );

            return builder;
        }

        private static ModelBuilder Participantes(this ModelBuilder builder)
        {
            builder.Entity<Participante>().HasData
                (
                    new Participante("Luiz Inacio", "09316578980", "5243518", "41994643417", "email@email.com")
                    {
                        Id = "e942c6f6c1364447a584a8f2fee583c2",
                        EnderecoId = "39f3a4d569ea400f848dd71deed5eb82"
                    },
                    new Participante("Carlos Gabriel", "09316558980", "3243518", "41595653717", "email@email1.com") { Id = "816e1eb79d7d4863a953f77e9d8a2347" },
                    new Participante("Amanda Mendez", "09316573980", "1243518", "41696663717", "email@email2.com") { Id = "02ee3c7582ea47a0a8105e9a8e9764bc" }
                );

            return builder;
        }

        private static ModelBuilder Vinculos(this ModelBuilder builder)
        {
            builder.Entity<Vinculo>().HasData
                (
                    new Vinculo("cf936849e0b244a2a6a812c15d69390d", "eeb45a8bc09e422dbfd45b456a78f878"),
                    new Vinculo("189be3c445504dcf9d8559959579035e", "eeb45a8bc09e422dbfd45b456a78f878")
                );
            return builder;
        }
    }
}
