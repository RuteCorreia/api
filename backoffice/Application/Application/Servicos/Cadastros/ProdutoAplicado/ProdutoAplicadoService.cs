using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.Interface;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;
using Application.DTOs.Cadastros.DataFormat.ViewModel;
using Application.DTOs.Cadastros.ProdutoAplicado.Interface;
using Application.DTOs.Cadastros.ProdutoAplicado.ViewModel;
using AutoMapper;
using Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado;
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

    public Task<int> AddAsync(ProdutoAplicadoCaracteristicasViewModel obj)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProdutoAplicadoCaracteristicasViewModel>> GetAllByIdCaracteristicaProdutoAplicadoAsync(int idCaracteristiacaProdutoAplicado)
    {
        throw new NotImplementedException();
    }

    public Task<ProdutoAplicadoCaracteristicasViewModel> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(ProdutoAplicadoCaracteristicasViewModel obj)
    {
        throw new NotImplementedException();
    }
}
