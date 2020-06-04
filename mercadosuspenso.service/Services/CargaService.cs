using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Excel;
using mercadosuspenso.domain.Dtos;
using mercadosuspenso.domain.Exceptions;
using mercadosuspenso.domain.Interfaces.Services;
using mercadosuspenso.domain.Models;
using mercadosuspenso.orm;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace mercadosuspenso.service.Services
{
    public class CargaService : ICargaService
    {
        private delegate void Assert(bool error, string message);
        private Assert Validar = DomainException.Validate;

        private readonly MercadoDbContext context;

        public CargaService(MercadoDbContext context)
        {
            this.context = context;
        }

        public async Task<ProcessamentoDto> ImportarParticipantes(IFormFile file)
        {
            var extensao = new[] { ".xlsx", ".xls" };
            bool sucesso = true;
            string mensagem = default;
            var threshold = 200 * 2048; //500kb

            using var transaction = context.Database.BeginTransaction();

            try
            {
                Validar(extensao.Any(a => a.Contains(Path.GetExtension(file.FileName))), 
                    "Formato de arquivo inválido");

                Validar(file.Length > threshold,
                    "Tamanho máximo do arquivo é de 500kb");

                using var stream = file.OpenReadStream();

                var participantes = CarregarParticipantes(stream);

                await context.Participante.AddRangeAsync(participantes);

                await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                sucesso = false;
                mensagem = "Verifique a estutura do arquivo e tente novamente";
                await transaction.RollbackAsync();
            }
            return new ProcessamentoDto
            {
                Mensagem = mensagem,
                Sucesso = sucesso,
            };
        }

        private IEnumerable<Participante> CarregarParticipantes(Stream stream)
        {
            try
            {
                stream.Seek(0, SeekOrigin.Begin);

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    BufferSize = 1024,
                    Encoding = Encoding.UTF8,
                    TrimOptions = TrimOptions.Trim,
                };

                using var parser = new ExcelParser(stream, config);

                using var reader = new CsvReader(parser);

                reader.Configuration.RegisterClassMap<ParticipanteMap>();

                return reader.GetRecords<Participante>();
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível ler os dados do arquivo, verifique a estrutura e tente novamente");
            }
        }
    }

    public interface ICargaService
    {
        Task<ProcessamentoDto> ImportarParticipantes(IFormFile file);
    }

    internal class ParticipanteMap : ClassMap<Participante>
    {
        public ParticipanteMap()
        {
            AutoMap(CultureInfo.InvariantCulture);

            Map(m => m.Id).Ignore();
            Map(m => m.CriadoEm).Ignore();
            Map(m => m.AtualizadoEm).Ignore();
            Map(m => m.Ativo).Ignore();
            Map(m => m.Status).Ignore();
            Map(m => m.Endereco).Ignore();
            Map(m => m.EnderecoId).Ignore();
        }
    }
}
