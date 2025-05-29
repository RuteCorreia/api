using Application.DTOs.Cadastros.ReceituarioAgronomico.Interface;
using Application.DTOs.Cadastros.ReceituarioAgronomico.ViewModel;
using AutoMapper;
using Domain.Entidades.Cadastros.ReceituarioAgronomico;
using Domain.Interfaces.BlobStorage;
using Domain.Interfaces.Cadastros.ReceituarioAgronomico;

namespace Application.Application.Servicos.Cadastros.CaracteristicasReceituarioAgronomico;

public class ReceituarioAgronomicoService : IReceituarioAgronomicoService
{
    private readonly IReceituarioAgronomicoRepository _receituarioAgronomicoRepository;
    private readonly IBlobStorageRepository _blobStorageRepository;
    private readonly IMapper _mapper;

    public ReceituarioAgronomicoService(
        IMapper mapper,
        IBlobStorageRepository blobStorageRepository,
        IReceituarioAgronomicoRepository receituarioAgronomicoRepository
    )
    {
        _mapper = mapper;
        _blobStorageRepository = blobStorageRepository;
        _receituarioAgronomicoRepository = receituarioAgronomicoRepository;
    }

    public async Task<int> AddAsync(ReceituarioAgronomicoViewModel obj)
    {
        return await _receituarioAgronomicoRepository.AddAsync(_mapper.Map<ReceituarioAgronomico>(obj));
    }

    public async Task DeleteAsync(int id)
    {
        await _receituarioAgronomicoRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ReceituarioAgronomicoViewModel>> GetAllByIdRelatorioAplicacaoAsync(int relatorioAplicacaoId)
    {
        var produtos = await _receituarioAgronomicoRepository.GetAllByIdRelatorioAplicacaoAsync(relatorioAplicacaoId);

        return produtos.Select(p => _mapper.Map<ReceituarioAgronomicoViewModel>(p));
    }

    public async Task<ReceituarioAgronomicoViewModel> GetByIdAsync(int id)
    {
        var produto = await _receituarioAgronomicoRepository.GetByIdAsync(id);

        return _mapper.Map<ReceituarioAgronomicoViewModel>(produto);
    }

    public async Task UpdateAsync(ReceituarioAgronomicoViewModel obj)
    {
        await _receituarioAgronomicoRepository.UpdateAsync(_mapper.Map<ReceituarioAgronomico>(obj));
    }
}
