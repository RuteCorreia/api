using Application.Application.Servicos.Cadastros.Empresa;
using Application.DTOs.Cadastros.Empresa.Interface;
using Application.DTOs.Email.Interface;
using Application.DTOs.Email.ViewModel;
using Application.DTOs.Users.Interface;
using Application.DTOs.Users.ViewModel;
using AutoMapper;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Entidades.User;
using Domain.Enums;
using Domain.Interfaces.Cadastros.Cliente;
using Domain.Interfaces.Cadastros.Empresa;
using Domain.Interfaces.User;
using Infra.Repositorio.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Application.Application.Servicos.User;

public class UserAuthService : IUserAuthService
{
    private readonly IUsuarioCredencialRepository _usuarioCredencialRepository;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IEmpresaRepository _empresaRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;

    public UserAuthService(
        IUsuarioCredencialRepository usuarioCredencialRepository,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IUsuarioRepository usuarioRepository,
        IEmpresaRepository empresaRepository,
        IClienteRepository clienteRepository,
        IEmailService emailService,
        IConfiguration config,
        IMapper mapper

        )
    {
        _usuarioCredencialRepository = usuarioCredencialRepository;
        _usuarioRepository = usuarioRepository;
        _empresaRepository = empresaRepository;
        _clienteRepository = clienteRepository;
        _emailService = emailService;
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _config = config;
    }

    public async Task<(bool, string)> LoginAsync(UserLoginViewModel user)
    {
        var identityUser = await _userManager.FindByEmailAsync(user.Email);
        if (identityUser is not null)
        {
            var usuario = await _usuarioRepository.GetByUserIdAsync(identityUser.Id);
            if (usuario is not null) {
                if (usuario.Removido) {
                    return (false, "Usuário removido. Entre em contato com o administrador do sistema.");
                };

                if (usuario.IdEmpresa is not null) {
                    var empresa = await _empresaRepository.GetByIdAsync(usuario.IdEmpresa);

                    if (empresa is null) {
                        return (false, "Empresa vinculada ao usuário não foi encontrada. Entre em contato com o administrador do sistema.");

                    } else {
                        if (empresa.Status is not 0) {
                            return (false, "Empresa desativada. Entre em contato com o administrador do sistema.");

                        } else {
                            var passwordCheck = await _userManager.CheckPasswordAsync(identityUser, user.Password);
                            if (passwordCheck) {

                                var camposFaltantes = ValidarCamposCabecalhoRelatorio(empresa);
                                if (camposFaltantes.Any())
                                    return (false, "Faltam dados obrigatórios para utilização do app. Entrar em contato com FlyTec S.A. (16) 99737-7438.");

                                var token = new StringBuilder();

                                if (usuario.PrimeiroAcesso) {
                                    usuario.PrimeiroAcesso = false;
                                    await _usuarioRepository.UpdateAsync(usuario);
                                }

                                token.Append(await GenerateToken(identityUser, usuario));
                                return (true, token.ToString());

                            } else {
                                return (false, "Senha inválida, por favor tente novamente.");
                            };
                        };
                    };

                } else {
                    return (false, "Usuário não possui empresa vinculada. Entre em contato com o administrador do sistema.");
                };

            };

            return (false, "Usuário não encontrado.");

        };
        return (false, "Usuário não encontrado.");
    }

    public async Task<(bool, string, IList<string>, int?)> LoginBackofficeAsync(UserLoginViewModel user)
    {
        var identityUser = await _userManager.FindByEmailAsync(user.Email);
        if (identityUser is not null) {
            var roles = await _userManager.GetRolesAsync(identityUser);
            var usuario = await _usuarioRepository.GetByUserIdAsync(identityUser.Id);

            if (usuario is not null) {
                if (usuario.Removido) {
                    return (false, "Usuário removido. Entre em contato com o administrador do sistema.", [], null);
                };

                if (usuario.IdEmpresa is not null) {
                    var empresa = await _empresaRepository.GetByIdAsync(usuario.IdEmpresa);

                    if (empresa is null) {
                        return (false, "Empresa vinculada ao usuário não foi encontrada. Entre em contato com o administrador do sistema.", [], null);

                    } else {
                        if (empresa.Status is not 0) {
                            return (false, "Empresa desativada. Entre em contato com o administrador do sistema.", [], null);

                        } else {
                            // permitir acesso apenas para usuários com as roles Administrativo ou AuxiliarAdministrativo
                            if (roles.Contains("Administrativo") || roles.Contains("AuxiliarAdministrativo") || roles.Contains("Administrador"))
                            {
                                var passwordCheck = await _userManager.CheckPasswordAsync(identityUser, user.Password);
                                
                                if (passwordCheck) {
                                    var token = new StringBuilder();
                                    token.Append(await GenerateToken(identityUser, usuario));
                                    return (true, token.ToString(), roles, usuario.IdEmpresa);
                                }

                                return (false, "Senha incorreta. Por favor, tente novamente.", [], null);
                            }

                            // usuário sem permissão para acessar o backoffice
                            return (false, "Acesso não autorizado. Recurso disponível apenas para Administrativo e Auxiliar Administrativo.", roles, null);
                        };
                    };
                } else {
                    return (false, "Usuário não possui empresa vinculada. Entre em contato com o administrador do sistema.", [], null);
                };
            };

            return (false, "Usuário não encontrado.", [], null);
        };

        return (false, "Usuário não encontrado.", [], null);
    }

    public async Task<(bool, string)> VerifyTokenAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return (false, "Token inválido");


        var usuario = await _usuarioRepository.GetByUserIdAsync(userId);
        if (usuario is null)
            return (false, "Usuário não encontrado");

        if (usuario.Removido)
            return (false, "Usuário removido. Entre em contato com o administrador.");

        if (usuario.IdEmpresa is null)
        {
            return (false, "Usuário sem empresa vinculada.");
        }
        else
        {
            var empresa = await _empresaRepository.GetByIdAsync(usuario.IdEmpresa);

            if (empresa is null)
                return (false, "Empresa não encontrada.");

            if (empresa.Status != 0)
                return (false, "Empresa desativada. Entre em contato com o administrador.");


            return (true, "Token válido");
        }
    }

    public async Task<(bool, string)> RegisterUserAsync(UserRegisterViewModel request, string loggedUserId)
    {
        var resultMsg = new StringBuilder();

        if (VerificarSeUsuarioEstaSendoCadastradoOuAtualizadoComoPiloto_E_Executor(request.Funcoes))
            return (false, resultMsg.Append("Não é permitido cadastrar usuário como piloto e executor.").ToString());

        var loggedIdentityUser = await _userManager.FindByIdAsync(loggedUserId);
        if (loggedIdentityUser is not null)
        {
            var identityUser = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.Telefone,
                EmailConfirmed = true
            };

            var identityResult = await _userManager.CreateAsync(identityUser, request.Password);

            if (!identityResult.Succeeded)
                return (false, resultMsg.Append(GetIdentityResultErrors(identityResult)).ToString());

            var loggedUserTblUsuario = await _usuarioRepository.GetByUserIdAsync(loggedUserId);

            if (request.Funcoes.Any(x => x.Funcao == ERole.EngAgronomoCoord))
            {
                var jaExisteEngenheiro = await _usuarioCredencialRepository.VerificarSeEmpresaPossuiEngenheiroAtivo(loggedUserTblUsuario.IdEmpresa);
                if (jaExisteEngenheiro)
                {
                    await _userManager.DeleteAsync(identityUser);
                    return (false, resultMsg.Append("Engenheiro já existente na empresa").ToString());
                }
            }


            var roleListAsString = request.Funcoes.Select(r => r.Funcao.ToString());
            foreach (var role in roleListAsString)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);
                if (!roleExists)
                    await CreateRoleAsync(role);
            }

            var identityRoleResult = await _userManager.AddToRolesAsync(identityUser, roleListAsString);
            if (!identityRoleResult.Succeeded)
            {
                await _userManager.DeleteAsync(identityUser);
                return (false, resultMsg.Append(GetIdentityResultErrors(identityRoleResult)).ToString());
            }


            await CreateUser(request, identityUser, loggedUserTblUsuario.IdEmpresa);
            await CreateUserCredencial(identityUser, request.Funcoes);

            return (true, resultMsg.Append("Usuário criado com sucesso").ToString());
        }

        return (false, resultMsg.Append("Não foi possível criar um usuário").ToString());
    }

    private async Task CreateUser(UserRegisterViewModel request, IdentityUser user, int? idEmpresa)
    {
        var nrUsuarioOrdem = await _usuarioRepository.GetLastAsync();
        var usuario = new Usuario(
            request.Email,
            request.Name,
            user.Id,
            nrUsuarioOrdem is null ? 1 : nrUsuarioOrdem.NrUsuario + 1,
            request.Telefone,
            idEmpresa,
            request.CPF,
            request.Comissao,
            request.GerarRelatorioManutencao,
            request.IdCliente
            );

        usuario.FlagTermoResp = request.FlagTermoResp;

        await _usuarioRepository.AddAsync(usuario);
    }

    private async Task CreateUserCredencial(IdentityUser user, IEnumerable<RoleObject> roles)
    {
        var usuario = await _usuarioRepository.GetByUserIdAsync(user.Id);
        var list = Enumerable.Empty<UsuarioCredencial>();
        foreach (var role in roles)
        {
            var obj = new UsuarioCredencial
            {
                IdUsuario = usuario.Id,
                Credencial = role.Credencial,
                Funcao = role.Funcao,
                NomeCompleto = role.NomeCompleto
            };

            list = list.Concat(new[] { obj });
        }

        await _usuarioCredencialRepository.AddListAsync(list);
    }

    private bool VerificarSeUsuarioEstaSendoCadastradoOuAtualizadoComoPiloto_E_Executor(IEnumerable<RoleObject> funcoes)
    {
        return funcoes.Any(x => x.Funcao == ERole.PilotoAeronave) && funcoes.Any(x => x.Funcao == ERole.TecnicoExecutor);
    }

    private List<string> ValidarCamposCabecalhoRelatorio(Domain.Entidades.Cadastros.Empresa.Empresa empresa)
    {
        var camposFaltantes = new List<string>();

        if (empresa.Imagem == null || !empresa.Imagem.Any())
            camposFaltantes.Add("Logo da Empresa");
        if (string.IsNullOrWhiteSpace(empresa.Nome))
            camposFaltantes.Add("Nome da Empresa");
        if (string.IsNullOrWhiteSpace(empresa.Telefone))
            camposFaltantes.Add("Telefone");
        if (string.IsNullOrWhiteSpace(empresa.CNPJ))
            camposFaltantes.Add("CNPJ");
        if (string.IsNullOrWhiteSpace(empresa.InscricaoEstadual))
            camposFaltantes.Add("Inscrição Estadual");
        if (string.IsNullOrWhiteSpace(empresa.RegistroMapa))
            camposFaltantes.Add("Registro MAPA");
        if (string.IsNullOrWhiteSpace(empresa.CEP))
            camposFaltantes.Add("CEP");
        if (string.IsNullOrWhiteSpace(empresa.Endereco))
            camposFaltantes.Add("Endereço");
        if (string.IsNullOrWhiteSpace(empresa.Numero))
            camposFaltantes.Add("Número");
        if (string.IsNullOrWhiteSpace(empresa.Cidade))
            camposFaltantes.Add("Cidade");
        if (string.IsNullOrWhiteSpace(empresa.Estado))
            camposFaltantes.Add("Estado");
        if (string.IsNullOrWhiteSpace(empresa.NrCDA))
            camposFaltantes.Add("NrCDA");

        return camposFaltantes;
    }

    private async Task<string> GenerateToken(IdentityUser identityUser, Usuario usuario)
    {
        var claims = await GetUserClaims(identityUser, usuario);
        var identityClaims = new ClaimsIdentity(claims);
        return WriteToken(identityClaims);
    }

    private async Task<IEnumerable<Claim>> GetUserClaims(IdentityUser identityUser, Usuario usuario)
    {
        var roles = await _userManager.GetRolesAsync(identityUser);
        var logoEmpresa = usuario.Empresa?.Imagem?.Length > 0 ? Convert.ToBase64String(usuario.Empresa?.Imagem ?? []) : "";

        var empresa = usuario.Empresa;
        string nomeEmpresa = !string.IsNullOrWhiteSpace(empresa?.Nome) ? empresa.Nome : "N/A";
        string telefoneEmpresa = !string.IsNullOrWhiteSpace(empresa?.Telefone) ? empresa.Telefone : "N/A";
        string cnpjEmpresa = !string.IsNullOrWhiteSpace(empresa?.CNPJ) ? empresa.CNPJ : "N/A";
        string inscricaoEstadual = !string.IsNullOrWhiteSpace(empresa?.InscricaoEstadual) ? empresa.InscricaoEstadual : "N/A";
        string registroMapa = !string.IsNullOrWhiteSpace(empresa?.RegistroMapa) ? empresa.RegistroMapa : "N/A";
        string cep = !string.IsNullOrWhiteSpace(empresa?.CEP) ? empresa.CEP : "N/A";
        string endereco = !string.IsNullOrWhiteSpace(empresa?.Endereco) ? empresa.Endereco : "N/A";
        string numero = !string.IsNullOrWhiteSpace(empresa?.Numero) ? empresa.Numero : "N/A";
        string cidade = !string.IsNullOrWhiteSpace(empresa?.Cidade) ? empresa.Cidade : "N/A";
        string estado = !string.IsNullOrWhiteSpace(empresa?.Estado) ? empresa.Estado : "N/A";
        string nrCDA = !string.IsNullOrWhiteSpace(empresa?.NrCDA) ? empresa.NrCDA : "N/A";


        var claims = new List<Claim>
        {
            new("NrUsuario", usuario.NrUsuario.ToString()),
            new("IdUsuario", usuario.Id.ToString()),
            new("cpfUsuario", usuario.CPF),
            new("IdEmpresa", usuario.IdEmpresa.ToString() ?? ""),
            new("NomeEmpresa", nomeEmpresa),
            new("FlagTermoResp", usuario.FlagTermoResp ?? ""),
            new("IdCliente", usuario.IdCliente?.ToString() ?? ""),
            new("EmailEmpresa", empresa?.Email ?? ""),
            new("FrotaRelatoriosAplicacaoIncendio", empresa?.FrotaRelatoriosAplicacaoIncendio.ToString() ?? string.Empty),
            new("Manutencao", empresa?.Manutencao.ToString() ?? string.Empty),
            new("TelefoneEmpresa", telefoneEmpresa),
            new("cnpj", cnpjEmpresa),
            new("inscricaoEstadualEmpresa", inscricaoEstadual),
            new("nrCDAEmpresa", nrCDA),
            new("registroMapaEmpresa", registroMapa),
            new("cepEmpresa", cep),
            new("enderecoEmpresa", endereco),
            new("numeroEmpresa", numero),
            new("cidadeEmpresa", cidade),
            new("estadoEmpresa", estado),
           // new("logoEmpresa", logoEmpresa),
            new("porcentagem", usuario.Comissao?.ToString() ?? "0"),
            new(JwtRegisteredClaimNames.Sub, identityUser.Id),
            new(JwtRegisteredClaimNames.Name, usuario.Nome),
            new(JwtRegisteredClaimNames.Email, identityUser.Email ?? "n/a"),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()),
            new(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)
        };

        // Buscar NomeCliente se houver IdCliente (mantido for other uses)
        string nomeCliente = string.Empty;
        if (usuario.IdCliente.HasValue)
        {
            var cliente = await _clienteRepository.GetByIdAsync(usuario.IdCliente.Value, usuario.IdEmpresa ?? 0);
            nomeCliente = cliente?.NomeCliente ?? string.Empty;
        }
        claims.Add(new Claim("NomeCliente", nomeCliente));

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        return claims;
    }

    private string WriteToken(ClaimsIdentity identityClaims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Secret").Value));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var expiresInHours = Convert.ToDouble(_config.GetSection("Jwt:ExpirationInHours").Value);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = identityClaims,
            Issuer = _config.GetSection("Jwt:Issuer").Value,
            Audience = _config.GetSection("Jwt:Audience").Value,
            Expires = DateTime.UtcNow.AddYears(100),
            SigningCredentials = signingCredentials
        });

        return tokenHandler.WriteToken(token);
    }

    private long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
            .TotalSeconds);

    public async Task<IEnumerable<UserListViewModel>> GetAllUsersAsync(string loggedUserId)
    {
        var loggedUserTblUsuario = await _usuarioRepository.GetByUserIdAsync(loggedUserId);
        var users = await _usuarioRepository.GetAllAsync(loggedUserTblUsuario.IdEmpresa);
        return _mapper.Map<IEnumerable<UserListViewModel>>(users);
    }

    public async Task RemoveUserAsync(string id)
    {
        var userToRemove = await GetUserEntityByIdAsync(id);

        if (userToRemove is not null)
        {
            var userAspNet = await _userManager.FindByIdAsync(userToRemove.UserId);
            var isEmpresa = await _empresaRepository.GetByIdAsync(userToRemove.IdEmpresa);
            if (isEmpresa.Email != userToRemove.Email)
            {
                if (userAspNet != null)
                {
                    await _usuarioRepository.RemoveAsync(userToRemove.Id);
                }
            }
        }
        else
        {
            throw new Exception("Este usuario é uma empresa");
        }
    }

    private async Task<Usuario> GetUserEntityByIdAsync(string id) => await _usuarioRepository.GetUserByIdAsync(id);

    public async Task<UserDetailViewModel> GetUserByIdAsync(string id)
    {
        var obj = await _usuarioRepository.GetUserByIdAsync(id);
        var mapObjUsuario = _mapper.Map<UserDetailViewModel>(obj);
        var usuarioCredencialList = await _usuarioCredencialRepository.GetUsuarioCredencialsAsync(obj.Id);
        mapObjUsuario.Funcoes = _mapper.Map<IEnumerable<RoleObject>>(usuarioCredencialList);
        return mapObjUsuario;
    }

    public async Task<UserDetailViewModel> GetByUserIdAsync(string id)
    {
        var obj = await _usuarioRepository.GetByUserIdAsync(id);
        var mapObjUsuario = _mapper.Map<UserDetailViewModel>(obj);
        var usuarioCredencialList = await _usuarioCredencialRepository.GetUsuarioCredencialsAsync(obj.Id);
        mapObjUsuario.Funcoes = _mapper.Map<IEnumerable<RoleObject>>(usuarioCredencialList);
        return mapObjUsuario;
    }

    public async Task RecoveryUserAsync(int id)
    {
        var usuarios = await _usuarioRepository.GetAllRemovidoAsync(id);
        foreach (var usuario in usuarios)
        {
            usuario.Removido = false;
            await _usuarioRepository.UpdateAsync(usuario);
        }
    }
    public async Task<(bool, string)> UpdateUserAsync(string id, UserUpdateViewModel request)
    {
        var resultMsg = new StringBuilder().Append("Atualização de usuário não foi possível");

        if (VerificarSeUsuarioEstaSendoCadastradoOuAtualizadoComoPiloto_E_Executor(request.Funcoes))
            return (false, resultMsg.Clear().Append("Não é permitido colocar o usuário como piloto e executor.").ToString());

        var userToUpdate = await GetUserEntityByIdAsync(id);
        if (userToUpdate is not null)
        {
            var isEmpresa = await _empresaRepository.GetByEmailAsync(userToUpdate.Email);
            if (isEmpresa != null)
                return (false, resultMsg.Clear().Append("O Email é uma empresa, você não pode editar nessa área").ToString());

            var identityUser = await _userManager.FindByEmailAsync(userToUpdate.Email);
            if (identityUser is not null)
            {
                if (userToUpdate.Email != request.Email)
                {
                    var emailExist = await _userManager.FindByEmailAsync(request.Email);
                    if (emailExist != null)
                        return (false, resultMsg.Clear().Append("O Email ja existe").ToString());
                }

                resultMsg.Clear();
                bool allOk = true;

                if (request.Funcoes.Any(x => x.Funcao == ERole.EngAgronomoCoord))
                {
                    var jaExisteEngenheiro = await _usuarioCredencialRepository.VerificarSeEmpresaPossuiEngenheiroAtivo(userToUpdate.IdEmpresa, userToUpdate.Id.ToString());
                    if (jaExisteEngenheiro)
                    {
                        return (false, resultMsg.Append("Engenheiro já existente na empresa").ToString());
                    }
                }

                var identityUserRoles = await _userManager.GetRolesAsync(identityUser);
                if (identityUser.Email != request.Email)
                {
                    var changeEmailToken = await GenerateChangeEmailOrPhoneTokenAsync(identityUser, request.Email, true);
                    var changeEmailResult = await _userManager.ChangeEmailAsync(identityUser, request.Email, changeEmailToken);
                    if (!changeEmailResult.Succeeded)
                    {
                        resultMsg.Append(GetIdentityResultErrors(changeEmailResult));
                        allOk = false;
                    }
                }

                if (identityUser.PhoneNumber != request.Telefone)
                {
                    var changePhoneToken = await GenerateChangeEmailOrPhoneTokenAsync(identityUser, request.Telefone ?? "");
                    var changePhoneResult = await _userManager.ChangePhoneNumberAsync(identityUser, request.Telefone ?? "", changePhoneToken);
                    if (!changePhoneResult.Succeeded)
                    {
                        resultMsg.Append(GetIdentityResultErrors(changePhoneResult));
                        allOk = false;
                    }
                }

                if (identityUser.UserName != request.Email)
                {
                    identityUser.UserName = request.Email;
                    var updateIdentityUserResult = await _userManager.UpdateAsync(identityUser);
                    if (!updateIdentityUserResult.Succeeded)
                    {
                        resultMsg.Append(GetIdentityResultErrors(updateIdentityUserResult));
                        allOk = false;
                    }
                }

                var requestRole = request.Funcoes.Select(r => r.Funcao.ToString());
                var rolesNotInIdentity = requestRole.Except(identityUserRoles);
                if (rolesNotInIdentity.Any())
                {
                    foreach (var role in rolesNotInIdentity)
                    {
                        var roleExists = await _roleManager.RoleExistsAsync(role);
                        if (!roleExists)
                            await CreateRoleAsync(role);
                    }

                    await _userManager.RemoveFromRolesAsync(identityUser, identityUserRoles);
                    var identityRoleResult = await _userManager.AddToRolesAsync(identityUser, requestRole);
                    if (!identityRoleResult.Succeeded)
                    {
                        resultMsg.Append(GetIdentityResultErrors(identityRoleResult));
                        allOk = false;
                    }
                }

                if (allOk)
                {
                    userToUpdate.Nome = request.Nome;
                    userToUpdate.Email = request.Email;
                    userToUpdate.Telefone = request.Telefone;
                    userToUpdate.CPF = request.CPF;
                    userToUpdate.Comissao = request.Comissao;
                    userToUpdate.GerarRelatorioManutencao = request.GerarRelatorioManutencao;
                    userToUpdate.FlagTermoResp = request.FlagTermoResp;
                    await _usuarioRepository.UpdateAsync(userToUpdate);
                    await _usuarioCredencialRepository.RemoveAllByUserIdAsync(userToUpdate.Id);
                    await CreateUserCredencial(identityUser, request.Funcoes);
                    resultMsg.Append("Sucesso na atualização do usuário");
                    return (true, resultMsg.ToString());
                }
            }
        }

        return (false, resultMsg.ToString());
    }

    private async Task<string> GenerateChangeEmailOrPhoneTokenAsync(IdentityUser user, string item, bool email = false)
    {
        if (email)
            return await _userManager.GenerateChangeEmailTokenAsync(user, item);
        else
            return await _userManager.GenerateChangePhoneNumberTokenAsync(user, item);
    }

    private string GetIdentityResultErrors(IdentityResult result)
    {
        var errors = result.Errors
            .Select(x => x.Description)
            .FirstOrDefault();

        return !string.IsNullOrEmpty(errors) ? errors : "Erro no processamento";
    }


    private async Task CreateRoleAsync(string roleToCreate)
    {
        var role = new IdentityRole(roleToCreate);
        await _roleManager.CreateAsync(role);
    }

    public async Task<(bool, string)> ChangeUserPasswordAsync(UserChangePasswordViewModel user)
    {
        var resultMsg = new StringBuilder().Append("Troca de senha não foi possível");
        var identityUser = await _userManager.FindByEmailAsync(user.Email);
        if (identityUser != null)
        {
            if (user.NewPassword.Length < 6 ||
            !user.NewPassword.Any(char.IsUpper) ||
            !Regex.IsMatch(user.NewPassword, @"[^a-zA-Z0-9]"))
            {
                resultMsg.Clear().Append("A senha deve conter pelo menos 6 digitos, sendo pelo menos um Maiusculo e um Especial");
                return (false, resultMsg.ToString());
            }

            var tokenValid = await _userManager.VerifyUserTokenAsync(identityUser, TokenOptions.DefaultProvider, "ResetPassword", user.Token);
            if (!tokenValid)
            {
                resultMsg.Clear().Append("Recuperação de senha inválida ou expirada. Solicite um novo email de recuperação");
                return (false, resultMsg.ToString());
            }

            var identityResult = await _userManager.ResetPasswordAsync(identityUser, user.Token, user.NewPassword);
            if (identityResult.Succeeded)
            {
                return (true, "Sucesso na troca de senha");
            }
            else
            {
                resultMsg.Clear().Append(GetIdentityResultErrors(identityResult));
            }
        }
        return (false, resultMsg.ToString());
    }

    public async Task<IEnumerable<RoleObject>> GetUserRolesAsync(string id)
    {
        var userRoles = await _usuarioCredencialRepository.GetUsuarioCredencialsAsync(Guid.Parse(id));
        var returnList = Enumerable.Empty<RoleObject>();
        foreach (var item in userRoles)
        {
            var obj = new RoleObject
            {
                Funcao = item.Funcao,
                Credencial = item.Credencial
            };

            returnList = returnList.Concat(new[] { obj });
        }

        return returnList;
    }

    public async Task<(bool, string)> SaveUserSignatureAsync(UserSaveSignatureViewModel obj, string loggedUserId)
    {
        var resultMsg = new StringBuilder().Append("Não foi possível salvar assinatura");
        try
        {
            var loggedIdentityUser = await _userManager.FindByIdAsync(loggedUserId);
            if (loggedIdentityUser is not null)
            {
                var usuario = await _usuarioRepository.GetByUserIdAsync(loggedUserId);
                if (usuario is not null)
                {
                    byte[] bytes = Convert.FromBase64String(obj.Assinatura);
                    usuario.Assinatura = bytes;
                    await _usuarioRepository.UpdateAsync(usuario);
                    return (true, resultMsg.Clear().ToString());
                }
            }

            return (false, resultMsg.ToString());
        }
        catch (Exception ex)
        {
            return (false, resultMsg.Clear().Append(ex.Message).ToString());
        }
    }

    public async Task<string> GetUserSignatureAsync(string userId)
    {
        var result = new StringBuilder();
        var usuario = await _usuarioRepository.GetUserByIdAsync(userId);
        if (usuario is not null && usuario.Assinatura is not null)
        {
            string convertedStr = Convert.ToBase64String(usuario.Assinatura);
            result.Append(convertedStr);
        }

        return result.ToString();
    }

    public async Task<(bool, string)> RegisterUserFromEmpresaAsync(UserRegisterViewModel request, int? idEmpresa)
    {
        var resultMsg = new StringBuilder();

        var identityUser = new IdentityUser
        {
            UserName = request.Email,
            Email = request.Email,
            PhoneNumber = request.Telefone,
            EmailConfirmed = true
        };

        var identityResult = await _userManager.CreateAsync(identityUser, request.Password);

        if (!identityResult.Succeeded)
            return (false, resultMsg.Append(GetIdentityResultErrors(identityResult)).ToString());

        var roleListAsString = request.Funcoes.Select(r => r.Funcao.ToString());
        foreach (var role in roleListAsString)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
                await CreateRoleAsync(role);
        }

        var identityRoleResult = await _userManager.AddToRolesAsync(identityUser, roleListAsString);
        if (!identityRoleResult.Succeeded)
        {
            await _userManager.DeleteAsync(identityUser);
            return (false, resultMsg.Append(GetIdentityResultErrors(identityRoleResult)).ToString());
        }


        await CreateUser(request, identityUser, idEmpresa);
        await CreateUserCredencial(identityUser, request.Funcoes);

        return (true, resultMsg.Append("Usuário criado com sucesso").ToString());
    }

    public async Task<UserProfileViewModel> GetUserProfileAsync(string userId)
    {
        var usuario = await _usuarioRepository.GetUserProfileAsync(userId);

        var viewModel = new UserProfileViewModel
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            IdEmpresa = usuario.IdEmpresa,
            IdCliente = usuario.IdCliente,
            FlagTermoResp = usuario.FlagTermoResp ?? string.Empty
        };

        return viewModel;
    }

    public async Task<IEnumerable<UsuarioClienteInfo>> GetUsuariosClientesAsync(int idEmpresa, string userId)
    {
        return await _usuarioRepository.GetUsuariosClientesAsync(idEmpresa, userId);
    }

    public async Task<(bool, string)> SaveUserFlagTermoRespAsync(string userId, string flag)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return (false, "userId inválido");

        var usuario = await _usuarioRepository.GetByUserIdAsync(userId);
        if (usuario == null)
            return (false, "Usuário não encontrado");

        usuario.FlagTermoResp = flag;
        await _usuarioRepository.UpdateAsync(usuario);
        return (true, "Flag salva com sucesso");
    }
}
