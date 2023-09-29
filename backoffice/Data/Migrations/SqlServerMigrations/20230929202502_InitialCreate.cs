using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations.SqlServerMigrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adjuvante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adjuvante", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlturaVoo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlturaVoo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sigla = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTipoCliente = table.Column<int>(type: "int", nullable: true),
                    CPF = table.Column<int>(type: "int", nullable: true),
                    RG = table.Column<int>(type: "int", nullable: true),
                    CNPJ = table.Column<int>(type: "int", nullable: true),
                    InscricaoEstadual = table.Column<int>(type: "int", nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precificacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "Combustivel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combustivel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cultura",
                columns: table => new
                {
                    IdCultura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlvoBiologico = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cultura", x => x.IdCultura);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    IdEmpresa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagem = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PlanoContratado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.IdEmpresa);
                });

            migrationBuilder.CreateTable(
                name: "Equipamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sigla = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pista",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LAT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LONG = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pista", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanoDeContrato",
                columns: table => new
                {
                    IdPlano = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeDoPlano = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoDeContrato", x => x.IdPlano);
                });

            migrationBuilder.CreateTable(
                name: "TipoProduto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoProduto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculante",
                columns: table => new
                {
                    IdVeiculante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculante", x => x.IdVeiculante);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCultura = table.Column<int>(type: "int", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassificacaoToxicologica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoDeFormulacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoServico = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_Cultura_IdCultura",
                        column: x => x.IdCultura,
                        principalTable: "Cultura",
                        principalColumn: "IdCultura");
                });

            migrationBuilder.CreateTable(
                name: "Aeronave",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true),
                    Prefixo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Combustivel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CapacidadeDeCarga = table.Column<int>(type: "int", nullable: true),
                    Horimetro = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aeronave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aeronave_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                });

            migrationBuilder.CreateTable(
                name: "Engenheiro",
                columns: table => new
                {
                    IdEngenheiro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CREA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Assinatura = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engenheiro", x => x.IdEngenheiro);
                    table.ForeignKey(
                        name: "FK_Engenheiro_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                });

            migrationBuilder.CreateTable(
                name: "Executor",
                columns: table => new
                {
                    IdExecutor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CFTA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Assinatura = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Executor", x => x.IdExecutor);
                    table.ForeignKey(
                        name: "FK_Executor_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                });

            migrationBuilder.CreateTable(
                name: "Frota",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true),
                    NomeVeiculo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Placa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Combustivel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hodometro = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frota", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Frota_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                });

            migrationBuilder.CreateTable(
                name: "Piloto",
                columns: table => new
                {
                    IdPiloto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true),
                    NomePiloto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CDAC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Assinatura = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PorcentagemComissao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piloto", x => x.IdPiloto);
                    table.ForeignKey(
                        name: "FK_Piloto_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                });

            migrationBuilder.CreateTable(
                name: "Precificacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true),
                    DistanciaPista = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecoHA = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Precificacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Precificacao_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                });

            migrationBuilder.CreateTable(
                name: "AlvoBiologico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProduto = table.Column<int>(type: "int", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoseProdutoPorHectare = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlvoBiologico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlvoBiologico_Produto_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "Produto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CombateIncendio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true),
                    IdExecutor = table.Column<int>(type: "int", nullable: true),
                    OrgaoPublico_Privado = table.Column<bool>(type: "bit", nullable: false),
                    Aviso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdAeronave = table.Column<int>(type: "int", nullable: true),
                    IdPista = table.Column<int>(type: "int", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HoraInicial = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HorimetroAviao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalIncendioLat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalIncendioLon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Referencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HorarioFinalOperacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HorimetroFinalOperacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAguaUtilizadaOperacao = table.Column<int>(type: "int", nullable: true),
                    CoordenadorBaseOperacionalNome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordenadorBaseOperacionalPosto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordenadorBaseOperacionalRE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordenadorBaseOperacionalAssinatura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComandanteOcorrenciaNome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComandanteOcorrenciaPosto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComandanteOcorrenciaRE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComandanteOcorrenciaAssinatura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsavelOcorrenciaNome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsavelOcorrenciaPosto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsavelOcorrenciaRE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsavelOcorrenciaAssinatura = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombateIncendio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CombateIncendio_Aeronave_IdAeronave",
                        column: x => x.IdAeronave,
                        principalTable: "Aeronave",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CombateIncendio_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                    table.ForeignKey(
                        name: "FK_CombateIncendio_Executor_IdExecutor",
                        column: x => x.IdExecutor,
                        principalTable: "Executor",
                        principalColumn: "IdExecutor");
                    table.ForeignKey(
                        name: "FK_CombateIncendio_Pista_IdPista",
                        column: x => x.IdPista,
                        principalTable: "Pista",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Aplicacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true),
                    StatusEnvio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPiloto = table.Column<int>(type: "int", nullable: true),
                    IdExecutor = table.Column<int>(type: "int", nullable: true),
                    IdCliente = table.Column<int>(type: "int", nullable: true),
                    IdCultura = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aplicacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aplicacao_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK_Aplicacao_Cultura_IdCultura",
                        column: x => x.IdCultura,
                        principalTable: "Cultura",
                        principalColumn: "IdCultura");
                    table.ForeignKey(
                        name: "FK_Aplicacao_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                    table.ForeignKey(
                        name: "FK_Aplicacao_Executor_IdExecutor",
                        column: x => x.IdExecutor,
                        principalTable: "Executor",
                        principalColumn: "IdExecutor");
                    table.ForeignKey(
                        name: "FK_Aplicacao_Piloto_IdPiloto",
                        column: x => x.IdPiloto,
                        principalTable: "Piloto",
                        principalColumn: "IdPiloto");
                });

            migrationBuilder.CreateTable(
                name: "ControleDeFrota",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdFrota = table.Column<int>(type: "int", nullable: true),
                    IdAeronave = table.Column<int>(type: "int", nullable: true),
                    KmInicial = table.Column<int>(type: "int", nullable: true),
                    LocalInicial = table.Column<int>(type: "int", nullable: true),
                    LocalizacaoPistaLat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalizacaoPistaLon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KmFinal = table.Column<int>(type: "int", nullable: true),
                    HorimetroInicial = table.Column<int>(type: "int", nullable: true),
                    HorimetroFinal = table.Column<int>(type: "int", nullable: true),
                    Combustivel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QtdeCombustivel = table.Column<int>(type: "int", nullable: true),
                    QtdeHectare = table.Column<int>(type: "int", nullable: true),
                    IdPiloto = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControleDeFrota", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControleDeFrota_Aeronave_IdAeronave",
                        column: x => x.IdAeronave,
                        principalTable: "Aeronave",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ControleDeFrota_Frota_IdFrota",
                        column: x => x.IdFrota,
                        principalTable: "Frota",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ControleDeFrota_Piloto_IdPiloto",
                        column: x => x.IdPiloto,
                        principalTable: "Piloto",
                        principalColumn: "IdPiloto");
                });

            migrationBuilder.CreateTable(
                name: "Bula",
                columns: table => new
                {
                    IdBula = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProduto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCultura = table.Column<int>(type: "int", nullable: true),
                    IdClassificacaoToxicologica = table.Column<int>(type: "int", nullable: true),
                    Classe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoDeFormulacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAlvoBiologico = table.Column<int>(type: "int", nullable: true),
                    DoseProdutoComercial = table.Column<int>(type: "int", nullable: true),
                    Adjuvante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTipoDeServico = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bula", x => x.IdBula);
                    table.ForeignKey(
                        name: "FK_Bula_AlvoBiologico_IdAlvoBiologico",
                        column: x => x.IdAlvoBiologico,
                        principalTable: "AlvoBiologico",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bula_Cultura_IdCultura",
                        column: x => x.IdCultura,
                        principalTable: "Cultura",
                        principalColumn: "IdCultura");
                });

            migrationBuilder.CreateTable(
                name: "CombateIncendioDecolagemPouso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCombateIncendio = table.Column<int>(type: "int", nullable: true),
                    DecolagemHorario = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DecolagemHorimetro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PousoHorario = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PousoHorimetro = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombateIncendioDecolagemPouso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CombateIncendioDecolagemPouso_CombateIncendio_IdCombateIncendio",
                        column: x => x.IdCombateIncendio,
                        principalTable: "CombateIncendio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AplicacaoAreaTratada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAplicacao = table.Column<int>(type: "int", nullable: true),
                    IdEstado = table.Column<int>(type: "int", nullable: true),
                    IdCidade = table.Column<int>(type: "int", nullable: true),
                    Localizacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extensao = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AplicacaoAreaTratada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AplicacaoAreaTratada_Aplicacao_IdAplicacao",
                        column: x => x.IdAplicacao,
                        principalTable: "Aplicacao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AplicacaoAreaTratada_Cidades_IdCidade",
                        column: x => x.IdCidade,
                        principalTable: "Cidades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AplicacaoAreaTratada_Estados_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estados",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AplicacaoCaracteristicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAplicacao = table.Column<int>(type: "int", nullable: true),
                    IdProduto = table.Column<int>(type: "int", nullable: true),
                    IdAdjuvante = table.Column<int>(type: "int", nullable: true),
                    TipoDeServico = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AplicacaoCaracteristicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AplicacaoCaracteristicas_Adjuvante_IdAdjuvante",
                        column: x => x.IdAdjuvante,
                        principalTable: "Adjuvante",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AplicacaoCaracteristicas_Aplicacao_IdAplicacao",
                        column: x => x.IdAplicacao,
                        principalTable: "Aplicacao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AplicacaoCaracteristicas_Produto_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "Produto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AplicacaoContrato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAplicacao = table.Column<int>(type: "int", nullable: true),
                    IdUF = table.Column<int>(type: "int", nullable: true),
                    IdCidade = table.Column<int>(type: "int", nullable: true),
                    NomeCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPFCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assinatura = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AplicacaoContrato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AplicacaoContrato_Aplicacao_IdAplicacao",
                        column: x => x.IdAplicacao,
                        principalTable: "Aplicacao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AplicacaoContrato_Cidades_IdCidade",
                        column: x => x.IdCidade,
                        principalTable: "Cidades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AplicacaoContrato_Estados_IdUF",
                        column: x => x.IdUF,
                        principalTable: "Estados",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AplicacaoCroqui",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAplicacao = table.Column<int>(type: "int", nullable: true),
                    Desenho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdMapa = table.Column<int>(type: "int", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AplicacaoCroqui", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AplicacaoCroqui_Aplicacao_IdAplicacao",
                        column: x => x.IdAplicacao,
                        principalTable: "Aplicacao",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AplicacaoLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAplicacao = table.Column<int>(type: "int", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AplicacaoLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AplicacaoLog_Aplicacao_IdAplicacao",
                        column: x => x.IdAplicacao,
                        principalTable: "Aplicacao",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AplicacaoRecomendacoesTecnicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAplicacao = table.Column<int>(type: "int", nullable: true),
                    IdVeiculante = table.Column<int>(type: "int", nullable: true),
                    QtdeVeiculante = table.Column<int>(type: "int", nullable: true),
                    LarguraFaixa = table.Column<int>(type: "int", nullable: true),
                    VolumeAplicacao = table.Column<int>(type: "int", nullable: true),
                    IdAeronave = table.Column<int>(type: "int", nullable: true),
                    IdAlturaVoo = table.Column<int>(type: "int", nullable: true),
                    AlturaVooCustom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Temperatura = table.Column<int>(type: "int", nullable: true),
                    UrDoAR = table.Column<int>(type: "int", nullable: true),
                    VelocidadeVento = table.Column<int>(type: "int", nullable: true),
                    IdTipoDeProduto = table.Column<int>(type: "int", nullable: true),
                    IdEquipamento = table.Column<int>(type: "int", nullable: true),
                    Angulo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AplicacaoRecomendacoesTecnicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AplicacaoRecomendacoesTecnicas_Aeronave_IdAeronave",
                        column: x => x.IdAeronave,
                        principalTable: "Aeronave",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AplicacaoRecomendacoesTecnicas_AlturaVoo_IdAlturaVoo",
                        column: x => x.IdAlturaVoo,
                        principalTable: "AlturaVoo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AplicacaoRecomendacoesTecnicas_Aplicacao_IdAplicacao",
                        column: x => x.IdAplicacao,
                        principalTable: "Aplicacao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AplicacaoRecomendacoesTecnicas_Equipamento_IdEquipamento",
                        column: x => x.IdEquipamento,
                        principalTable: "Equipamento",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AplicacaoRecomendacoesTecnicas_TipoProduto_IdTipoDeProduto",
                        column: x => x.IdTipoDeProduto,
                        principalTable: "TipoProduto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AplicacaoRecomendacoesTecnicas_Veiculante_IdVeiculante",
                        column: x => x.IdVeiculante,
                        principalTable: "Veiculante",
                        principalColumn: "IdVeiculante");
                });

            migrationBuilder.CreateTable(
                name: "AplicacaoRelatorio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAplicacao = table.Column<int>(type: "int", nullable: true),
                    IdPista = table.Column<int>(type: "int", nullable: true),
                    Dosagem = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    KG_LT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VolumeAplicacao = table.Column<int>(type: "int", nullable: true),
                    TotalAreaAplicada = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Alteracoes_Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AplicacaoRelatorio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AplicacaoRelatorio_Aplicacao_IdAplicacao",
                        column: x => x.IdAplicacao,
                        principalTable: "Aplicacao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AplicacaoRelatorio_Pista_IdPista",
                        column: x => x.IdPista,
                        principalTable: "Pista",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AplicacaoCroquiImportacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAplicacaoCroqui = table.Column<int>(type: "int", nullable: true),
                    Arquivo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AplicacaoCroquiImportacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AplicacaoCroquiImportacao_AplicacaoCroqui_IdAplicacaoCroqui",
                        column: x => x.IdAplicacaoCroqui,
                        principalTable: "AplicacaoCroqui",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AplicacaoRelatorioItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAplicacaoRelatorio = table.Column<int>(type: "int", nullable: true),
                    HoraInicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HorimetroInicial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoraTermino = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HorimetroTermino = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemperaturaInicial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemperaturaFinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrInicial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrFinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VentoInicial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VentoFinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagemDadosClimaticos = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AplicacaoRelatorioItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AplicacaoRelatorioItem_AplicacaoRelatorio_IdAplicacaoRelatorio",
                        column: x => x.IdAplicacaoRelatorio,
                        principalTable: "AplicacaoRelatorio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aeronave_IdEmpresa",
                table: "Aeronave",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_AlvoBiologico_IdProduto",
                table: "AlvoBiologico",
                column: "IdProduto");

            migrationBuilder.CreateIndex(
                name: "IX_Aplicacao_IdCliente",
                table: "Aplicacao",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Aplicacao_IdCultura",
                table: "Aplicacao",
                column: "IdCultura");

            migrationBuilder.CreateIndex(
                name: "IX_Aplicacao_IdEmpresa",
                table: "Aplicacao",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Aplicacao_IdExecutor",
                table: "Aplicacao",
                column: "IdExecutor");

            migrationBuilder.CreateIndex(
                name: "IX_Aplicacao_IdPiloto",
                table: "Aplicacao",
                column: "IdPiloto");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoAreaTratada_IdAplicacao",
                table: "AplicacaoAreaTratada",
                column: "IdAplicacao");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoAreaTratada_IdCidade",
                table: "AplicacaoAreaTratada",
                column: "IdCidade");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoAreaTratada_IdEstado",
                table: "AplicacaoAreaTratada",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoCaracteristicas_IdAdjuvante",
                table: "AplicacaoCaracteristicas",
                column: "IdAdjuvante");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoCaracteristicas_IdAplicacao",
                table: "AplicacaoCaracteristicas",
                column: "IdAplicacao");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoCaracteristicas_IdProduto",
                table: "AplicacaoCaracteristicas",
                column: "IdProduto");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoContrato_IdAplicacao",
                table: "AplicacaoContrato",
                column: "IdAplicacao");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoContrato_IdCidade",
                table: "AplicacaoContrato",
                column: "IdCidade");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoContrato_IdUF",
                table: "AplicacaoContrato",
                column: "IdUF");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoCroqui_IdAplicacao",
                table: "AplicacaoCroqui",
                column: "IdAplicacao");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoCroquiImportacao_IdAplicacaoCroqui",
                table: "AplicacaoCroquiImportacao",
                column: "IdAplicacaoCroqui");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoLog_IdAplicacao",
                table: "AplicacaoLog",
                column: "IdAplicacao");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoRecomendacoesTecnicas_IdAeronave",
                table: "AplicacaoRecomendacoesTecnicas",
                column: "IdAeronave");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoRecomendacoesTecnicas_IdAlturaVoo",
                table: "AplicacaoRecomendacoesTecnicas",
                column: "IdAlturaVoo");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoRecomendacoesTecnicas_IdAplicacao",
                table: "AplicacaoRecomendacoesTecnicas",
                column: "IdAplicacao");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoRecomendacoesTecnicas_IdEquipamento",
                table: "AplicacaoRecomendacoesTecnicas",
                column: "IdEquipamento");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoRecomendacoesTecnicas_IdTipoDeProduto",
                table: "AplicacaoRecomendacoesTecnicas",
                column: "IdTipoDeProduto");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoRecomendacoesTecnicas_IdVeiculante",
                table: "AplicacaoRecomendacoesTecnicas",
                column: "IdVeiculante");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoRelatorio_IdAplicacao",
                table: "AplicacaoRelatorio",
                column: "IdAplicacao");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoRelatorio_IdPista",
                table: "AplicacaoRelatorio",
                column: "IdPista");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacaoRelatorioItem_IdAplicacaoRelatorio",
                table: "AplicacaoRelatorioItem",
                column: "IdAplicacaoRelatorio");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bula_IdAlvoBiologico",
                table: "Bula",
                column: "IdAlvoBiologico");

            migrationBuilder.CreateIndex(
                name: "IX_Bula_IdCultura",
                table: "Bula",
                column: "IdCultura");

            migrationBuilder.CreateIndex(
                name: "IX_CombateIncendio_IdAeronave",
                table: "CombateIncendio",
                column: "IdAeronave");

            migrationBuilder.CreateIndex(
                name: "IX_CombateIncendio_IdEmpresa",
                table: "CombateIncendio",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_CombateIncendio_IdExecutor",
                table: "CombateIncendio",
                column: "IdExecutor");

            migrationBuilder.CreateIndex(
                name: "IX_CombateIncendio_IdPista",
                table: "CombateIncendio",
                column: "IdPista");

            migrationBuilder.CreateIndex(
                name: "IX_CombateIncendioDecolagemPouso_IdCombateIncendio",
                table: "CombateIncendioDecolagemPouso",
                column: "IdCombateIncendio");

            migrationBuilder.CreateIndex(
                name: "IX_ControleDeFrota_IdAeronave",
                table: "ControleDeFrota",
                column: "IdAeronave");

            migrationBuilder.CreateIndex(
                name: "IX_ControleDeFrota_IdFrota",
                table: "ControleDeFrota",
                column: "IdFrota");

            migrationBuilder.CreateIndex(
                name: "IX_ControleDeFrota_IdPiloto",
                table: "ControleDeFrota",
                column: "IdPiloto");

            migrationBuilder.CreateIndex(
                name: "IX_Engenheiro_IdEmpresa",
                table: "Engenheiro",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Executor_IdEmpresa",
                table: "Executor",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Frota_IdEmpresa",
                table: "Frota",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Piloto_IdEmpresa",
                table: "Piloto",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Precificacao_IdEmpresa",
                table: "Precificacao",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_IdCultura",
                table: "Produto",
                column: "IdCultura");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AplicacaoAreaTratada");

            migrationBuilder.DropTable(
                name: "AplicacaoCaracteristicas");

            migrationBuilder.DropTable(
                name: "AplicacaoContrato");

            migrationBuilder.DropTable(
                name: "AplicacaoCroquiImportacao");

            migrationBuilder.DropTable(
                name: "AplicacaoLog");

            migrationBuilder.DropTable(
                name: "AplicacaoRecomendacoesTecnicas");

            migrationBuilder.DropTable(
                name: "AplicacaoRelatorioItem");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Bula");

            migrationBuilder.DropTable(
                name: "CombateIncendioDecolagemPouso");

            migrationBuilder.DropTable(
                name: "Combustivel");

            migrationBuilder.DropTable(
                name: "ControleDeFrota");

            migrationBuilder.DropTable(
                name: "Engenheiro");

            migrationBuilder.DropTable(
                name: "PlanoDeContrato");

            migrationBuilder.DropTable(
                name: "Precificacao");

            migrationBuilder.DropTable(
                name: "Adjuvante");

            migrationBuilder.DropTable(
                name: "Cidades");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "AplicacaoCroqui");

            migrationBuilder.DropTable(
                name: "AlturaVoo");

            migrationBuilder.DropTable(
                name: "Equipamento");

            migrationBuilder.DropTable(
                name: "TipoProduto");

            migrationBuilder.DropTable(
                name: "Veiculante");

            migrationBuilder.DropTable(
                name: "AplicacaoRelatorio");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AlvoBiologico");

            migrationBuilder.DropTable(
                name: "CombateIncendio");

            migrationBuilder.DropTable(
                name: "Frota");

            migrationBuilder.DropTable(
                name: "Aplicacao");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Aeronave");

            migrationBuilder.DropTable(
                name: "Pista");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Executor");

            migrationBuilder.DropTable(
                name: "Piloto");

            migrationBuilder.DropTable(
                name: "Cultura");

            migrationBuilder.DropTable(
                name: "Empresa");
        }
    }
}
