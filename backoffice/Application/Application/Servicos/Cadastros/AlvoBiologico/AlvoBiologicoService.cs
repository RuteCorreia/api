using Application.DTOs.Cadastros.Alvo_Biologico.ViewModel;
using Application.DTOs.Cadastros.AlvoBiologico.Interface;
using Application.DTOs.Cadastros.AlvoBiologico.ViewModel;
using AutoMapper;
using Domain.Entidades.Cadastros.Alvo_Biologico;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.AlvoBiologico;
using Domain.Interfaces.Cadastros.BulaAplicacao;
using Domain.Interfaces.Cadastros.Cultura;
using Domain.Interfaces.Cadastros.Produto;
using Domain.Interfaces.Cadastros.TipoDeUnidade;
using Helpers;
using System.Collections.Generic;

namespace Application.Application.Servicos.Cadastros.AlvoBiologico;

public class AlvoBiologicoService : IAlvoBiologicoService
{
    private readonly IAlvoBiologicoRepository _alvoBiologicoRepository;
    private readonly IBulaAplicacaoRepository _bulaAplicacaoRepository;
    private readonly ITipoDeUnidadeRepository _tipoDeUnidadeRepository;
    private readonly ICulturaRepository _culturaRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;

    public AlvoBiologicoService(
        IAlvoBiologicoRepository alvoBiologicoRepository,
        IBulaAplicacaoRepository bulaAplicacaoRepository,
        ITipoDeUnidadeRepository tipoDeUnidadeRepository,
        ICulturaRepository culturaRepository,
        IProdutoRepository produtoRepository,
        IMapper mapper 
        )
    {
        _alvoBiologicoRepository = alvoBiologicoRepository;
        _tipoDeUnidadeRepository = tipoDeUnidadeRepository;
        _bulaAplicacaoRepository = bulaAplicacaoRepository;
        _culturaRepository = culturaRepository;
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AlvoBiologicoViewModel>> GetAllAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var list = await _alvoBiologicoRepository.GetAllAsync(idEmpresaInt);
        return _mapper.Map<IEnumerable<AlvoBiologicoViewModel>>(list);
    }

    public async Task<AlvoBiologicoViewModel> GetByIdAsync(int id)
    {
        var obj = await _alvoBiologicoRepository.GetByIdAsync(id);
        return _mapper.Map<AlvoBiologicoViewModel>(obj);
    }

    public async Task<IEnumerable<AlvoBiologicoViewModel>> GetByIdCulturaAsync(int id)
    {
        var list = await _alvoBiologicoRepository.GetByIdCulturaAsync(id);
        return _mapper.Map<IEnumerable<AlvoBiologicoViewModel>>(list);
    }

    public async Task<IEnumerable<AlvoBiologicoViewModel>> GetAlvosBiologicosAsync(string nomeCultura, string nomeProduto, string? idEmpresa)
    {
        try
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var cultura = await _culturaRepository.GetByNameAsync(nomeCultura, idEmpresaInt);
            var produto = await _produtoRepository.GetByNameAsync(nomeProduto, idEmpresaInt);
            var result = await _alvoBiologicoRepository.GetAlvosBiologicosAsync(cultura.IdCultura, produto.Id, idEmpresaInt);
            return _mapper.Map<IEnumerable<AlvoBiologicoViewModel>>(result);
        }
        catch (Exception ex)
        {
            // Aqui você pode adicionar tratamento de exceção, logging, etc.
            throw new Exception("Erro ao obter Alvos Biológicos.", ex);
        }
    }

    public async Task AddAsync(AlvoBiologicoViewModel obj, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var mapAlvoBiologico = _mapper.Map<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>(obj);
        mapAlvoBiologico.IdEmpresa = idEmpresaInt;
        await _alvoBiologicoRepository.AddAsync(mapAlvoBiologico);
    }

    public async Task UpdateAsync(AlvoBiologicoViewModel obj, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var mapAlvoBiologico = _mapper.Map<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>(obj);
        await _alvoBiologicoRepository.UpdateAsync(mapAlvoBiologico, idEmpresaInt);
    }

    public async Task DeleteAsync(int id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        await _alvoBiologicoRepository.DeleteAsync(id, idEmpresaInt);
    }

    public async Task<AlvoBiologicoViewModel> GetByName(string name, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var obj = await _alvoBiologicoRepository.GetByNameAsync(name, idEmpresaInt);
        return _mapper.Map<AlvoBiologicoViewModel>(obj);
    }

    public async Task<IEnumerable<FormulacaoViewModel>> GetFormulacaoAsync(int idBula)
    {
        var listBulaAplicacao = await _bulaAplicacaoRepository.GetByIdBulaAsync(idBula);
        var result = new List<FormulacaoViewModel>();
        foreach (var bulaAplicacao in listBulaAplicacao)
        {
            var alvoBiologico = await _alvoBiologicoRepository.GetByIdAsync(bulaAplicacao.IdAlvoBiologico);
            //var cultura = await _culturaRepository.GetByIdAsync(alvoBiologico.IdCultura);
            var tipoDeUnidade = await _tipoDeUnidadeRepository.GetByIdAsync(alvoBiologico.IdTipoDeUnidade);

            var formulacao = new FormulacaoViewModel
            {
                Id = alvoBiologico.Id,
                //Cultura = cultura.Nome,
                AlvoBiologico = alvoBiologico.Nome,
                DoseProduto = alvoBiologico.DoseProdutoPorHectare,
                UnidadeDeMedida = tipoDeUnidade.NomeUnidade
            };

            result.Add(formulacao);
        }
        return result;
    }

    public async Task UpdateFormulacaoAsync(FormulacaoViewModel objeto, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var alvoBiologico = await _alvoBiologicoRepository.GetByIdAsync(objeto.Id);

        if (alvoBiologico == null)
        {
            throw new Exception("Alvo Biológico não encontrado.");
        }

        var unidadesDeMedida = await _tipoDeUnidadeRepository.GetAllAsync();
        var unidadeDeMedida = unidadesDeMedida.FirstOrDefault(u => u.NomeUnidade == objeto.UnidadeDeMedida);

        if (unidadeDeMedida == null)
        {
            throw new Exception("Unidade de medida não encontrada.");
        }

        alvoBiologico.DoseProdutoPorHectare = objeto.DoseProduto;
        alvoBiologico.IdTipoDeUnidade = unidadeDeMedida.Id;

        var mapAlvoBiologico = _mapper.Map<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>(alvoBiologico);
        await _alvoBiologicoRepository.UpdateAsync(mapAlvoBiologico, idEmpresaInt);
        
    }
}
