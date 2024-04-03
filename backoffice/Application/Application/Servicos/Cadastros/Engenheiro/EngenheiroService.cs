using Application.DTOs.Cadastros.Engenheiro.Interface;
using Application.DTOs.Cadastros.Engenheiro.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Engenheiro;

namespace Application.Application.Servicos.Cadastros.Engenheiro;

public class EngenheiroService : IEngenheiroService
{
    private readonly IEngenheiroRepository _engenheiroRepository;
    private readonly IMapper _mapper;

    public EngenheiroService(IMapper mapper, IEngenheiroRepository engenheiroRepository)
    {
        _engenheiroRepository = engenheiroRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EngenheiroViewModel>> GetAllAsync()
    {
        var list = await _engenheiroRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EngenheiroViewModel>>(list);
    }

    public async Task<EngenheiroViewModel> GetByIdAsync(string id)
    {
        var obj = await _engenheiroRepository.GetByIdAsync(id);
        return _mapper.Map<EngenheiroViewModel>(obj);
    }

    public async Task<EngenheiroViewModel> GetByLoginAsync(string email, string password)
    {
        //var obj = await _engenheiroRepository.GetByLoginAsync(email, password);
        return _mapper.Map<EngenheiroViewModel>(null);
    }

    public async Task AddAsync(EngenheiroViewModel obj)
    {
        //VERIFICAR SE VAI SER NECESSARIO ADD ESSES USUARIOS ENGENHEIROS
        //var empresaJaTemEngenheiro = await GetByIdEmpresaAsync(obj.Id, obj.IdEmpresa);
        //if(empresaJaTemEngenheiro is null)
        //{
        //    //var mapEngenheiro = _mapper.Map<Domain.Entidades.Cadastros.Engenheiro.Engenheiro>(obj);
        //    //await _engenheiroRepository.AddAsync(mapEngenheiro);
        //}
        //else
        //{
        //    throw new Exception("A empresa já possui um engenheiro");
        //}
    }

    public async Task UpdateAsync(EngenheiroViewModel obj)
    {
        //VERIFICAR SE VAI SER NECESSARIO ADD ESSES USUARIOS ENGENHEIROS
        //var empresaJaTemEngenheiro = await GetByIdEmpresaAsync(obj.Id, obj.IdEmpresa);
        //if (empresaJaTemEngenheiro is null)
        //{
        //    //var mapEngenheiro = _mapper.Map<Domain.Entidades.Cadastros.Engenheiro.Engenheiro>(obj);
        //    //await _engenheiroRepository.UpdateAsync(mapEngenheiro);
        //}
        //else
        //{
        //    throw new Exception("A empresa já possui um engenheiro");
        //}
    }

    public async Task DeleteAsync(int id)
    {
        //VERIFICAR SE VAI SER NECESSARIO ADD ESSES USUARIOS ENGENHEIROS
        //await _engenheiroRepository.DeleteAsync(id);
    }

    public async Task<EngenheiroViewModel> GetByIdEmpresaAsync(int id, int idEmpresa)
    {
        //VERIFICAR SE VAI SER NECESSARIO ADD ESSES USUARIOS ENGENHEIROS
        //var obj = await _engenheiroRepository.GetByIdEmpresaAsync(id, idEmpresa);
        return _mapper.Map<EngenheiroViewModel>(null);
    }
}
