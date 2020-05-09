using mercadosuspenso.domain.Dtos;
using mercadosuspenso.domain.Enums;
using mercadosuspenso.domain.Exceptions;
using mercadosuspenso.domain.Extensions;
using mercadosuspenso.domain.Interfaces.Providers;
using mercadosuspenso.domain.Interfaces.Services;
using mercadosuspenso.domain.Models;
using mercadosuspenso.orm.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mercadosuspenso.service.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private delegate void Assert(bool error, string message);
        private Assert Validar = DomainException.Validate;

        private readonly IAuthenticationProvider authProvider;
        private readonly IRepository<Varejista> varejistaRepository;
        private readonly IRepository<Distribuidor> distribuidorRepository;
        private readonly IRepository<Usuario> usuarioRepository;

        public AutenticacaoService(IAuthenticationProvider authProvider, IRepository<Usuario> usuarioRepository, IRepository<Varejista> varejistaRepository, IRepository<Distribuidor> distribuidorRepository)
        {
            this.authProvider = authProvider;
            this.usuarioRepository = usuarioRepository;
            this.varejistaRepository = varejistaRepository;
            this.distribuidorRepository = distribuidorRepository;
        }

        public async Task<SignInDto> SignInAdminAsync(string email, string password)
        {
            var md5 = password.EncryptToMD5();

            var usuario = await usuarioRepository.ByAsync
            (
                usuario =>
                usuario.Ativo &&
                usuario.Email == email
            );

            Validar(usuario is null,
                "Usuário não vinculado, contate o suporte");

            Validar(usuario.Tipo != UsuarioTipo.Administrador,
                "Perfil sem acesso a esta funcionalidade");

            Validar(usuario.Senha != md5,
                "Senha inválida tente novamente");

            // todo retrive from db
            var roles = new List<string>
            {
                "REPORTS",
                "MANAGEMENT",
            };

            var token = authProvider.CreateAccessToken(usuario, roles);

            return new SignInDto
            {
                AccessToken = token,
                Type = "Bearer",
                Roles = roles,
            };
        }

        public async Task<SignInDto> SignInVarejistaAsync(string cnpj, string password)
        {
            var md5 = password.EncryptToMD5();

            var varejista = await varejistaRepository.ByAsync
            (
                varejista =>
                varejista.Ativo &&
                varejista.Cnpj == cnpj.CleanFormat(),
                includes: include => include.Usuario
            );

            Validar(varejista is null,
                "Usuário não encontrado, tente novamente");

            Validar(varejista.Usuario is null,
                "Usuário não vinculado, contate o suporte");

            Validar(varejista.Usuario.Senha != md5,
                "Senha inválida tente novamente");

            Validar(varejista.Status == RegistroStatus.Recusado,
                "Cadastro inativo contate a administração");

            // todo retrive from db
            var roles = new List<string>
            {
                "RETAILER",
                "STOCKIST",
            };

            var token = authProvider.CreateAccessToken(varejista.Usuario, roles);

            return new SignInDto
            {
                AccessToken = token,
                Type = "Bearer",
                Roles = roles,
            };
        }

        public async Task<SignInDto> SignInDistribuidorAsync(string cnpj, string password)
        {
            var md5 = password.EncryptToMD5();

            var distribuidor = await distribuidorRepository.ByAsync
            (
                varejista =>
                varejista.Ativo &&
                varejista.Cnpj == cnpj.CleanFormat(),
                includes: include => include.Usuario
            );

            Validar(distribuidor is null,
                "Usuário não encontrado, tente novamente");

            Validar(distribuidor.Usuario is null,
                "Usuário não vinculado, contate o suporte");

            Validar(distribuidor.Usuario.Senha != md5,
                "Senha inválida tente novamente");

            Validar(distribuidor.Status == RegistroStatus.Recusado,
                "Cadastro inativo contate a administração");

            // todo retrive from db
            var roles = new List<string>
            {
                "DISPENSER",
                "STOCKIST",
            };

            var token = authProvider.CreateAccessToken(distribuidor.Usuario, roles);

            return new SignInDto
            {
                AccessToken = token,
                Type = "Bearer",
                Roles = roles,
            };
        }
    }
}