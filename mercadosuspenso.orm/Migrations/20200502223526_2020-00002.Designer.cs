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
    [Migration("20200502223526_2020-00002")]
    partial class _202000002
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
                            CriadoEm = new DateTimeOffset(new DateTime(2020, 5, 2, 22, 35, 26, 75, DateTimeKind.Unspecified).AddTicks(5064), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "lucas.moraes@datasuricata.com.br",
                            Senha = "C3F21A70DFB5072C152691AF019CC68A",
                            Tipo = 99
                        },
                        new
                        {
                            Id = "e0176f9f9b0445de83411b404e68a311",
                            Ativo = true,
                            CriadoEm = new DateTimeOffset(new DateTime(2020, 5, 2, 22, 35, 26, 75, DateTimeKind.Unspecified).AddTicks(8303), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "equipegkio@gmail.com",
                            Senha = "2D27FE16A2DE63872DDEC0BAFFB473BC",
                            Tipo = 99
                        },
                        new
                        {
                            Id = "c9f9cd17020c4bfaa83f7c53e5f9b278",
                            Ativo = true,
                            CriadoEm = new DateTimeOffset(new DateTime(2020, 5, 2, 22, 35, 26, 75, DateTimeKind.Unspecified).AddTicks(8365), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "web.admin@mailinator.com",
                            Senha = "6C5F89C867E08F99BE95D3CBA4B88594",
                            Tipo = 99
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
