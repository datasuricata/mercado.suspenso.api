﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using mercadosuspenso.orm;

namespace mercadosuspenso.orm.Migrations
{
    [DbContext(typeof(MercadoDbContext))]
    [Migration("20200503214747_2020-00003")]
    partial class _202000003
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("mercadosuspenso.domain.Models.Distribuidor", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("AtualizadoEm")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Cnpj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CriadoEm")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("EnderecoId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("RazaoSocial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Representante")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Distribuidor");

                    b.HasData(
                        new
                        {
                            Id = "cf936849e0b244a2a6a812c15d69390d",
                            Ativo = true,
                            Cnpj = "46992525000151",
                            CriadoEm = new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 618, DateTimeKind.Unspecified).AddTicks(7986), new TimeSpan(0, 0, 0, 0, 0)),
                            EnderecoId = "eff3b1b9b3b742a6b5ba4fffdf99585e",
                            RazaoSocial = "Luiza e Jennifer Restaurante Ltda",
                            Representante = "Jennifer Rocha",
                            Status = 0,
                            Telefone = "1129551395",
                            UsuarioId = "7d5ea14a85ba4af7a4ca4c95545f3545"
                        },
                        new
                        {
                            Id = "189be3c445504dcf9d8559959579035e",
                            Ativo = true,
                            Cnpj = "99270944000146",
                            CriadoEm = new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 619, DateTimeKind.Unspecified).AddTicks(8546), new TimeSpan(0, 0, 0, 0, 0)),
                            EnderecoId = "5d2387e4b93f476c81dd1bb757b27f43",
                            RazaoSocial = "LAndré e Mateus Pizzaria Delivery Ltda",
                            Representante = "Carlos Matues",
                            Status = 0,
                            Telefone = "1128810961",
                            UsuarioId = "2580868e52444603b4d75fdf92f7f916"
                        });
                });

            modelBuilder.Entity("mercadosuspenso.domain.Models.Doacao", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("AtualizadoEm")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Cpf")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CriadoEm")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Detalhes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VarejistaId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Doacao");
                });

            modelBuilder.Entity("mercadosuspenso.domain.Models.DoacaoProduto", b =>
                {
                    b.Property<string>("DoacaoId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("ProdutoId")
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("DoacaoId", "ProdutoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("DoacaoProduto");
                });

            modelBuilder.Entity("mercadosuspenso.domain.Models.Endereco", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("AtualizadoEm")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Bairro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cep")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Complemento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CriadoEm")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logradouro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Endereco");

                    b.HasData(
                        new
                        {
                            Id = "eff3b1b9b3b742a6b5ba4fffdf99585e",
                            Ativo = true,
                            Bairro = "Centro",
                            Cep = "04747110",
                            Cidade = "São Paulo",
                            Complemento = "SALA 02",
                            CriadoEm = new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 617, DateTimeKind.Unspecified).AddTicks(1268), new TimeSpan(0, 0, 0, 0, 0)),
                            Estado = "SP",
                            Logradouro = "Rua Santo Aristides",
                            Numero = "23"
                        },
                        new
                        {
                            Id = "5d2387e4b93f476c81dd1bb757b27f43",
                            Ativo = true,
                            Bairro = "Indianópolis",
                            Cep = "04028003",
                            Cidade = "São Paulo",
                            Complemento = "Conj. 01",
                            CriadoEm = new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 618, DateTimeKind.Unspecified).AddTicks(2476), new TimeSpan(0, 0, 0, 0, 0)),
                            Estado = "SP",
                            Logradouro = "Avenida Ibirapuera",
                            Numero = "334"
                        },
                        new
                        {
                            Id = "010bafef4b7342bda843d22b1ec9d7c2",
                            Ativo = true,
                            Bairro = "Sitio dos Vianas",
                            Cep = "09171686",
                            Cidade = "São Paulo",
                            Complemento = "02",
                            CriadoEm = new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 618, DateTimeKind.Unspecified).AddTicks(2672), new TimeSpan(0, 0, 0, 0, 0)),
                            Estado = "SP",
                            Logradouro = "Rua Santa Adélia",
                            Numero = "35"
                        },
                        new
                        {
                            Id = "39f3a4d569ea400f848dd71deed5eb82",
                            Ativo = true,
                            Bairro = "Centro",
                            Cep = "80020040",
                            Cidade = "Curitiba",
                            Complemento = "32",
                            CriadoEm = new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 618, DateTimeKind.Unspecified).AddTicks(2685), new TimeSpan(0, 0, 0, 0, 0)),
                            Estado = "PR",
                            Logradouro = "Muricy 73",
                            Numero = "32"
                        });
                });

            modelBuilder.Entity("mercadosuspenso.domain.Models.Participante", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("AtualizadoEm")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Cpf")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CriadoEm")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("EnderecoId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Participante");

                    b.HasData(
                        new
                        {
                            Id = "e942c6f6c1364447a584a8f2fee583c2",
                            Ativo = true,
                            Cpf = "09316578980",
                            CriadoEm = new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 621, DateTimeKind.Unspecified).AddTicks(1321), new TimeSpan(0, 0, 0, 0, 0)),
                            EnderecoId = "39f3a4d569ea400f848dd71deed5eb82",
                            Nome = "Luiz Inacio",
                            Rg = "5243518",
                            Status = 0,
                            Telefone = "41994643417"
                        },
                        new
                        {
                            Id = "816e1eb79d7d4863a953f77e9d8a2347",
                            Ativo = true,
                            Cpf = "09316558980",
                            CriadoEm = new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 621, DateTimeKind.Unspecified).AddTicks(8395), new TimeSpan(0, 0, 0, 0, 0)),
                            Nome = "Carlos Gabriel",
                            Rg = "3243518",
                            Status = 0,
                            Telefone = "41595653717"
                        },
                        new
                        {
                            Id = "02ee3c7582ea47a0a8105e9a8e9764bc",
                            Ativo = true,
                            Cpf = "09316573980",
                            CriadoEm = new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 621, DateTimeKind.Unspecified).AddTicks(8508), new TimeSpan(0, 0, 0, 0, 0)),
                            Nome = "Amanda Mendez",
                            Rg = "1243518",
                            Status = 0,
                            Telefone = "41696663717"
                        });
                });

            modelBuilder.Entity("mercadosuspenso.domain.Models.Produto", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("AtualizadoEm")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Codigo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CriadoEm")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("mercadosuspenso.domain.Models.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("AtualizadoEm")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("CriadoEm")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Usuario");

                    b.HasData(
                        new
                        {
                            Id = "d87e4de503984cfd8b282616ba50b42f",
                            Ativo = true,
                            CriadoEm = new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 602, DateTimeKind.Unspecified).AddTicks(601), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "lucas.moraes@datasuricata.com.br",
                            Senha = "C3F21A70DFB5072C152691AF019CC68A",
                            Tipo = 99
                        },
                        new
                        {
                            Id = "e0176f9f9b0445de83411b404e68a311",
                            Ativo = true,
                            CriadoEm = new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 602, DateTimeKind.Unspecified).AddTicks(3700), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "equipegkio@gmail.com",
                            Senha = "2D27FE16A2DE63872DDEC0BAFFB473BC",
                            Tipo = 99
                        },
                        new
                        {
                            Id = "c9f9cd17020c4bfaa83f7c53e5f9b278",
                            Ativo = true,
                            CriadoEm = new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 602, DateTimeKind.Unspecified).AddTicks(3777), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "web.admin@mailinator.com",
                            Senha = "6C5F89C867E08F99BE95D3CBA4B88594",
                            Tipo = 99
                        },
                        new
                        {
                            Id = "7d5ea14a85ba4af7a4ca4c95545f3545",
                            Ativo = true,
                            CriadoEm = new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 602, DateTimeKind.Unspecified).AddTicks(4936), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "jeniffer.restaurante@restaurante.com",
                            Senha = "E10ADC3949BA59ABBE56E057F20F883E",
                            Tipo = 2
                        },
                        new
                        {
                            Id = "2580868e52444603b4d75fdf92f7f916",
                            Ativo = true,
                            CriadoEm = new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 614, DateTimeKind.Unspecified).AddTicks(6502), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "carlos@pizzaria.com",
                            Senha = "E10ADC3949BA59ABBE56E057F20F883E",
                            Tipo = 2
                        },
                        new
                        {
                            Id = "4bea229e5b5a4d66a75d1d248f7dc2d5",
                            Ativo = true,
                            CriadoEm = new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 614, DateTimeKind.Unspecified).AddTicks(7053), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "olivia@noturna.com.br",
                            Senha = "E10ADC3949BA59ABBE56E057F20F883E",
                            Tipo = 1
                        });
                });

            modelBuilder.Entity("mercadosuspenso.domain.Models.Varejista", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("AtualizadoEm")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Cnpj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CriadoEm")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("EnderecoId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<bool>("Parceiro")
                        .HasColumnType("bit");

                    b.Property<string>("RazaoSocial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Representante")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Varejista");

                    b.HasData(
                        new
                        {
                            Id = "eeb45a8bc09e422dbfd45b456a78f878",
                            Ativo = true,
                            Cnpj = "18904128000145",
                            CriadoEm = new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 620, DateTimeKind.Unspecified).AddTicks(2272), new TimeSpan(0, 0, 0, 0, 0)),
                            EnderecoId = "010bafef4b7342bda843d22b1ec9d7c2",
                            Parceiro = false,
                            RazaoSocial = "Olivia e Raimundo Casa Noturna ME",
                            Representante = "Oliveira Moraes",
                            Status = 0,
                            Telefone = "1125262748",
                            UsuarioId = "4bea229e5b5a4d66a75d1d248f7dc2d5"
                        });
                });

            modelBuilder.Entity("mercadosuspenso.domain.Models.Vinculo", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("AtualizadoEm")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("CriadoEm")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("DistribuidorId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VarejistaId")
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("DistribuidorId");

                    b.HasIndex("VarejistaId");

                    b.ToTable("Vinculo");

                    b.HasData(
                        new
                        {
                            Id = "c9dc874f89314d86be9ac7930254be10",
                            Ativo = true,
                            CriadoEm = new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 622, DateTimeKind.Unspecified).AddTicks(1629), new TimeSpan(0, 0, 0, 0, 0)),
                            DistribuidorId = "cf936849e0b244a2a6a812c15d69390d",
                            VarejistaId = "eeb45a8bc09e422dbfd45b456a78f878"
                        },
                        new
                        {
                            Id = "a577f4026f6d4e3cb4a711ff1fd9409a",
                            Ativo = true,
                            CriadoEm = new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 622, DateTimeKind.Unspecified).AddTicks(2927), new TimeSpan(0, 0, 0, 0, 0)),
                            DistribuidorId = "189be3c445504dcf9d8559959579035e",
                            VarejistaId = "eeb45a8bc09e422dbfd45b456a78f878"
                        });
                });

            modelBuilder.Entity("mercadosuspenso.domain.Models.Vistoria", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("AtualizadoEm")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("CriadoEm")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("DistribudidorId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DistribuidorId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("DoacaoId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Hash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParticipanteId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("Processamento")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DistribuidorId");

                    b.HasIndex("DoacaoId");

                    b.HasIndex("ParticipanteId");

                    b.ToTable("Vistoria");
                });

            modelBuilder.Entity("mercadosuspenso.domain.Models.Distribuidor", b =>
                {
                    b.HasOne("mercadosuspenso.domain.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId");

                    b.HasOne("mercadosuspenso.domain.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("mercadosuspenso.domain.Models.DoacaoProduto", b =>
                {
                    b.HasOne("mercadosuspenso.domain.Models.Doacao", "Doacao")
                        .WithMany("DoacaoProdutos")
                        .HasForeignKey("DoacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("mercadosuspenso.domain.Models.Produto", "Produto")
                        .WithMany("DoacaoProdutos")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("mercadosuspenso.domain.Models.Participante", b =>
                {
                    b.HasOne("mercadosuspenso.domain.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId");
                });

            modelBuilder.Entity("mercadosuspenso.domain.Models.Varejista", b =>
                {
                    b.HasOne("mercadosuspenso.domain.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId");

                    b.HasOne("mercadosuspenso.domain.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("mercadosuspenso.domain.Models.Vinculo", b =>
                {
                    b.HasOne("mercadosuspenso.domain.Models.Distribuidor", "Distribuidor")
                        .WithMany()
                        .HasForeignKey("DistribuidorId");

                    b.HasOne("mercadosuspenso.domain.Models.Varejista", "Varejista")
                        .WithMany()
                        .HasForeignKey("VarejistaId");
                });

            modelBuilder.Entity("mercadosuspenso.domain.Models.Vistoria", b =>
                {
                    b.HasOne("mercadosuspenso.domain.Models.Distribuidor", "Distribuidor")
                        .WithMany()
                        .HasForeignKey("DistribuidorId");

                    b.HasOne("mercadosuspenso.domain.Models.Doacao", "Doacao")
                        .WithMany()
                        .HasForeignKey("DoacaoId");

                    b.HasOne("mercadosuspenso.domain.Models.Participante", "Participante")
                        .WithMany()
                        .HasForeignKey("ParticipanteId");
                });
#pragma warning restore 612, 618
        }
    }
}
