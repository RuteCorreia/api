using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.Interface;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;
using Application.DTOs.Cadastros.DataFormat.ViewModel;
using Application.DTOs.Cadastros.Gerador.ViewModel;
using Application.DTOs.Cadastros.ProdutoAplicado.Interface;
using Application.DTOs.Cadastros.ProdutoAplicado.ViewModel;
using AutoMapper;
using Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado;
using Domain.Entidades.Cadastros.Gerador;
using Domain.Entidades.Cadastros.ProdutoAplicado;
using Domain.Entidades.Cadastros.RelatorioAplicacao;
using Domain.Interfaces.BlobStorage;
using Domain.Interfaces.Cadastros.CaracteristicasProdutoAplicado;
using Domain.Interfaces.Cadastros.ProdutoAplicado;
using Helpers;
using Newtonsoft.Json;

namespace Application.Application.Servicos.Cadastros.CaracteristicasProdutoAplicado;

public class ProdutoAplicadoService : IProdutoAplicadoService
{
    private readonly IProdutoAplicadoRepository _produtoAplicadoRepository;
    private readonly IBlobStorageRepository _blobStorageRepository;
    private readonly IMapper _mapper;

    public ProdutoAplicadoService(
        IMapper mapper,
        IBlobStorageRepository blobStorageRepository,
        IProdutoAplicadoRepository produtoAplicadoRepository
    )
    {
        _mapper = mapper;
        _blobStorageRepository = blobStorageRepository;
        _produtoAplicadoRepository = produtoAplicadoRepository;
    }

    public async Task<int> AddAsync(ProdutoAplicadoCaracteristicasViewModel obj)
    {
        return await _produtoAplicadoRepository.AddAsync(_mapper.Map<ProdutoAplicado>(obj));
    }

    public async Task DeleteAsync(int id)
    {
        await _produtoAplicadoRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ProdutoAplicadoCaracteristicasViewModel>> GetAllByIdRelatorioAplicacaoAsync(int relatorioAplicacaoId)
    {
        var produtos = await _produtoAplicadoRepository.GetAllByIdRelatorioAplicacaoAsync(relatorioAplicacaoId);

        return produtos.Select(p => _mapper.Map<ProdutoAplicadoCaracteristicasViewModel>(p));
    }

    public async Task<ProdutoAplicadoCaracteristicasViewModel> GetByIdAsync(int id)
    {
        var produto = await _produtoAplicadoRepository.GetByIdAsync(id);

        return _mapper.Map<ProdutoAplicadoCaracteristicasViewModel>(produto);
    }

    public async Task UpdateAsync(ProdutoAplicadoCaracteristicasViewModel obj)
    {
        await _produtoAplicadoRepository.UpdateAsync(_mapper.Map<ProdutoAplicado>(obj));
    }
}
