using Application.DTOs.Cadastros.Aeronave.ViewModel;
using Application.DTOs.Cadastros.AlturaVoo.ViewModel;
using Application.DTOs.Cadastros.AlvoBiologico.ViewModel;
using Application.DTOs.Cadastros.Bateria.ViewModel;
using Application.DTOs.Cadastros.Bula.ViewModel;
using Application.DTOs.Cadastros.Cliente.ViewModel;
using Application.DTOs.Cadastros.Cultura.ViewModel;
using Application.DTOs.Cadastros.Engenheiro.ViewModel;
using Application.DTOs.Cadastros.Equipamento.ViewModel;
using Application.DTOs.Cadastros.Executor.ViewModel;
using Application.DTOs.Cadastros.Gerador.ViewModel;
using Application.DTOs.Cadastros.Motobomba.ViewModel;
using Application.DTOs.Cadastros.Piloto.ViewModel;
using Application.DTOs.Cadastros.Pistas.ViewModel;
using Application.DTOs.Cadastros.Produto.ViewModel;
using Application.DTOs.Cadastros.Sincronizacao.Interface;
using Application.DTOs.Cadastros.Sincronizacao.ViewModel;
using Application.DTOs.Cadastros.Tipo_Produto.ViewModel;
using Application.DTOs.Cadastros.TipoDeFormulacao.ViewModel;
using Application.DTOs.Cadastros.TipoDeServico.ViewModel;
using Application.DTOs.Cadastros.TipoDeUnidade.ViewModel;
using Application.DTOs.Cadastros.Veiculante.ViewModel;
using Application.DTOs.Cadastros.Veiculo.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Aeronave;
using Domain.Interfaces.Cadastros.AlturaVoo;
using Domain.Interfaces.Cadastros.AlvoBiologico;
using Domain.Interfaces.Cadastros.Bateria;
using Domain.Interfaces.Cadastros.Bula;
using Domain.Interfaces.Cadastros.Cliente;
using Domain.Interfaces.Cadastros.Cultura;
using Domain.Interfaces.Cadastros.Engenheiro;
using Domain.Interfaces.Cadastros.Equipamento;
using Domain.Interfaces.Cadastros.Executor;
using Domain.Interfaces.Cadastros.Gerador;
using Domain.Interfaces.Cadastros.Motobomba;
using Domain.Interfaces.Cadastros.Piloto;
using Domain.Interfaces.Cadastros.Pista;
using Domain.Interfaces.Cadastros.Produto;
using Domain.Interfaces.Cadastros.TipoDeFormulacao;
using Domain.Interfaces.Cadastros.TipoDeServico;
using Domain.Interfaces.Cadastros.TipoDeUnidade;
using Domain.Interfaces.Cadastros.TipoProduto;
using Domain.Interfaces.Cadastros.Veiculante;
using Domain.Interfaces.Cadastros.Veiculo;
using Helpers;
using Newtonsoft.Json;

namespace Application.Application.Servicos.Cadastros.Sincronizacao
{
    public class SincronizacaoService : ISincronizacaoService
    {
        private readonly IAeronaveRepository _aeronaveRepository;
        private readonly IAlvoBiologicoRepository _alvoBiologicoRepository;
        private readonly IAlturaVooRepository _alturaVooRepository;
        private readonly IBateriaRepository _bateriaRepository;
        private readonly IBulaRepository _bulaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly ICulturaRepository _culturaRepository;
        private readonly IEquipamentoRepository _equipamentoRepository;
        private readonly IGeradorRepository _geradoreRepository;
        private readonly IPistaRepository _pistaRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly ITipoDeFormulacaoRepository _tipoDeFormulacaoRepository;
        private readonly ITipoProdutoRepository _tipoProdutoRepository;
        private readonly ITipoDeServicoRepository _tipoDeServicoRepository;
        private readonly ITipoDeUnidadeRepository _tipoDeUnidadeRepository;
        private readonly IVeiculanteRepository _veiculanteRepository;
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IEngenheiroRepository _engenheiroRepository;
        private readonly IExecutorRepository _executoreRepository;
        private readonly IPilotoRepository _pilotoRepository;
        private readonly IMotobombaRepository _motobombaRepository;
        private readonly IMapper _mapper;

        public SincronizacaoService(
            IAeronaveRepository aeronaveRepository,
            IAlvoBiologicoRepository alvoBiologicoRepository,
            IAlturaVooRepository alturaVooRepository,
            IBateriaRepository bateriaRepository,
            IBulaRepository bulaRepository,
            IClienteRepository clienteRepository,
            ICulturaRepository culturaRepository,
            IEquipamentoRepository equipamentoRepository,
            IGeradorRepository geradoreRepository,
            IPistaRepository pistaRepository,
            IProdutoRepository produtoRepository,
            ITipoDeFormulacaoRepository tipoDeFormulacaoRepository,
            ITipoProdutoRepository tipoProdutoRepository,
            ITipoDeServicoRepository tipoDeServicoRepository,
            ITipoDeUnidadeRepository tipoDeUnidadeRepository,
            IVeiculanteRepository veiculanteRepository,
            IVeiculoRepository veiculoRepository,
            IEngenheiroRepository engenheiroRepository,
            IExecutorRepository executoreRepository,
            IPilotoRepository pilotoRepository,
            IMotobombaRepository motobombaRepository,
            IMapper mapper)
        {
            _aeronaveRepository = aeronaveRepository;
            _alvoBiologicoRepository = alvoBiologicoRepository;
            _alturaVooRepository = alturaVooRepository;
            _bateriaRepository = bateriaRepository;
            _bulaRepository = bulaRepository;
            _clienteRepository = clienteRepository;
            _culturaRepository = culturaRepository;
            _equipamentoRepository = equipamentoRepository;
            _geradoreRepository = geradoreRepository;
            _pistaRepository = pistaRepository;
            _produtoRepository = produtoRepository;
            _tipoDeFormulacaoRepository = tipoDeFormulacaoRepository;
            _tipoProdutoRepository = tipoProdutoRepository;
            _tipoDeServicoRepository = tipoDeServicoRepository;
            _tipoDeUnidadeRepository = tipoDeUnidadeRepository;
            _veiculanteRepository = veiculanteRepository;
            _veiculoRepository = veiculoRepository;
            _engenheiroRepository = engenheiroRepository;
            _executoreRepository = executoreRepository;
            _pilotoRepository = pilotoRepository;
            _motobombaRepository = motobombaRepository;
            _mapper = mapper;
        }

        public async Task<SincronizacaoViewModel> GetAsync(string? idEmpresa, DateTime dataUltimaSincronizacao)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var sincronizacao = new SincronizacaoViewModel();

            sincronizacao.Aeronaves = await GetAeronavesAsync(idEmpresaInt, dataUltimaSincronizacao);
            sincronizacao.AlvosBiologicos = _mapper.Map<IEnumerable<AlvoBiologicoViewModel>>(await _alvoBiologicoRepository.GetByDateAsync(idEmpresaInt, dataUltimaSincronizacao));
            sincronizacao.Alturas = _mapper.Map<IEnumerable<AlturaVooViewModel>>(await _alturaVooRepository.GetByDateAsync(dataUltimaSincronizacao));
            sincronizacao.Baterias = _mapper.Map<IEnumerable<BateriaViewModel>>(await _bateriaRepository.GetByDateAsync(idEmpresaInt, dataUltimaSincronizacao));
            sincronizacao.Bulas = _mapper.Map<IEnumerable<BulaAppViewModel>>(await _bulaRepository.GetByDateAsync(idEmpresaInt, dataUltimaSincronizacao));
            sincronizacao.Clientes = _mapper.Map<IEnumerable<ClienteViewModel>>(await _clienteRepository.GetByDateAsync(idEmpresaInt, dataUltimaSincronizacao));
            sincronizacao.Culturas = _mapper.Map<IEnumerable<CulturaViewModel>>(await _culturaRepository.GetByDateAsync(idEmpresaInt, dataUltimaSincronizacao));
            sincronizacao.Equipamentos = _mapper.Map<IEnumerable<EquipamentoViewModel>>(await _equipamentoRepository.GetByDateAsync(dataUltimaSincronizacao));
            sincronizacao.Pistas = _mapper.Map<IEnumerable<PistaViewModel>>(await _pistaRepository.GetByDateAsync(idEmpresaInt, dataUltimaSincronizacao));
            sincronizacao.Produtos = _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.GetByDateAsync(idEmpresaInt, dataUltimaSincronizacao));
            sincronizacao.TiposDeFormulacao = _mapper.Map<IEnumerable<TipoDeFormulacaoViewModel>>(await _tipoDeFormulacaoRepository.GetByDateAsync(idEmpresaInt, dataUltimaSincronizacao));
            sincronizacao.TiposProduto = _mapper.Map<IEnumerable<TipoProdutoViewModel>>(await _tipoProdutoRepository.GetByDateAsync(dataUltimaSincronizacao));
            sincronizacao.TiposDeServico = _mapper.Map<IEnumerable<TipoDeServicoViewModel>>(await _tipoDeServicoRepository.GetByDateAsync(dataUltimaSincronizacao));
            sincronizacao.TiposDeUnidade = _mapper.Map<IEnumerable<TipoDeUnidadeViewModel>>(await _tipoDeUnidadeRepository.GetByDateAsync(dataUltimaSincronizacao));
            sincronizacao.Veiculantes = _mapper.Map<IEnumerable<VeiculanteViewModel>>(await _veiculanteRepository.GetByDateAsync(dataUltimaSincronizacao));
            sincronizacao.Veiculos = _mapper.Map<IEnumerable<VeiculoViewModel>>(await _veiculoRepository.GetByDateAsync(idEmpresaInt, dataUltimaSincronizacao));
            sincronizacao.Engenheiros = await GetEngenheirosAsync(idEmpresaInt, dataUltimaSincronizacao);
            sincronizacao.Executores = _mapper.Map<IEnumerable<ExecutorViewModel>>(await _executoreRepository.GetByDateAsync(idEmpresaInt, dataUltimaSincronizacao));
            sincronizacao.Pilotos = _mapper.Map<IEnumerable<PilotoViewModel>>(await _pilotoRepository.GetByDateAsync(idEmpresaInt, dataUltimaSincronizacao));
            sincronizacao.Motobombas = _mapper.Map<IEnumerable<MotobombaViewModel>>(await _motobombaRepository.GetByDateAsync(idEmpresaInt, dataUltimaSincronizacao));

            var geradores = await _geradoreRepository.GetByDateAsync(idEmpresaInt, dataUltimaSincronizacao);

            sincronizacao.Geradores = _mapper.Map<IEnumerable<GeradorViewModel>>(geradores);
            foreach (var gerador in sincronizacao.Geradores)
            {
                gerador.QuantidadeHoras = gerador.QuantidadeHoras / 3600000;
                gerador.QuantidadeHorasTroca = gerador.QuantidadeHorasTroca / 3600000;
            }

            return sincronizacao;
        }

        private async Task<IEnumerable<AeronaveViewModel>> GetAeronavesAsync(int idEmpresa, DateTime dataUltimaSincronizacao)
        {
            var aeronaves = await _aeronaveRepository.GetByDateAsync(idEmpresa, dataUltimaSincronizacao);
            var aeronaveViewModels = new List<AeronaveViewModel>();

            foreach (var a in aeronaves)
            {
                var aeronaveViewModel = new AeronaveViewModel
                {
                    Id = a.Id,
                    Tipo = a.Tipo,
                    Fabricante = a.Fabricante,
                    Prefixo = a.Prefixo,
                    Modelo = a.Modelo,
                    SerialNumber = a.SerialNumber
                };

                if (!string.IsNullOrEmpty(a.Checklist))
                {
                    aeronaveViewModel.Checklist = JsonConvert.DeserializeObject<IEnumerable<string>>(a.Checklist);
                }
                else
                {
                    aeronaveViewModel.Checklist = Enumerable.Empty<string>();
                }

                aeronaveViewModels.Add(aeronaveViewModel);
            }

            return aeronaveViewModels;
        }

        public async Task<IEnumerable<EngenheiroViewModel>> GetEngenheirosAsync(int idEmpresa, DateTime dataUltimaSincronizacao)
        {
            var usuarioCredencialList = await _engenheiroRepository.GetByDateAsync(idEmpresa, dataUltimaSincronizacao);
            var usuarioList = usuarioCredencialList
                .Select(u => u.Usuario)
                .ToList();
            var mappedList = _mapper.Map<IEnumerable<EngenheiroViewModel>>(usuarioList);
            var usuarioDict = usuarioList.ToDictionary(u => u.Id, u => u);
            var credencialDict = usuarioCredencialList.ToDictionary(uc => uc.IdUsuario, uc => uc.Credencial);

            var returnList = mappedList.Select(x => new EngenheiroViewModel
            {
                Id = x.Id,
                Nome = x.Nome,
                Email = x.Email,
                Telefone = x.Telefone,
                Assinatura = usuarioDict.TryGetValue(Guid.Parse(x.Id), out var usuario) ?
                    Convert.ToBase64String(usuario?.Assinatura ?? [])
                    : null,
                CREA = credencialDict.TryGetValue(Guid.Parse(x.Id), out var credencial) ?
                    credencial
                    : null
            });
            return returnList;
        }
    }

}
