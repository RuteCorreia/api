using Application.DTOs.Cadastros.Cliente.Interface;
using Application.DTOs.Cadastros.Cliente.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Cliente;
using Helpers;

namespace Application.Application.Servicos.Cadastros.Cliente;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;

    public ClienteService(IMapper mapper, IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ClienteViewModel>> GetAllAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var list = await _clienteRepository.GetAllAsync(idEmpresaInt);
        return _mapper.Map<IEnumerable<ClienteViewModel>>(list);
    }

    public async Task<ClienteViewModel> GetByIdAsync(int id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var obj = await _clienteRepository.GetByIdAsync(id, idEmpresaInt);
        return _mapper.Map<ClienteViewModel>(obj);
    }
    
    public async Task<ClienteViewModel> GetByLoginAsync(string email, string password)
    {
        var obj = await _clienteRepository.GetByLoginAsync(email, password);
        return _mapper.Map<ClienteViewModel>(obj);
    }

    public async Task AddAsync(ClienteViewModel obj, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var mapCliente = _mapper.Map<Domain.Entidades.Cadastros.Cliente.Cliente>(obj);
        mapCliente.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
        await _clienteRepository.AddAsync(mapCliente);
    }

    public async Task UpdateAsync(ClienteViewModel obj)
    {
        var mapCliente = _mapper.Map<Domain.Entidades.Cadastros.Cliente.Cliente>(obj);
        await _clienteRepository.UpdateAsync(mapCliente);
    }

    public async Task<IEnumerable<ClienteViewModel>> GetByNameAsync(string name, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var list = await _clienteRepository.GetByNameAsync(name, idEmpresaInt);
        return _mapper.Map<IEnumerable<ClienteViewModel>>(list);
    }

    public async Task DeleteAsync(int id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        await _clienteRepository.DeleteAsync(id, idEmpresaInt);
    }
}
