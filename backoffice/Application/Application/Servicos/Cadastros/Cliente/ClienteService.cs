using Application.DTOs.Cadastros.Cliente.Interface;
using Application.DTOs.Cadastros.Cliente.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Cliente;

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

    public async Task<IEnumerable<ClienteViewModel>> GetAllAsync()
    {
        var list = await _clienteRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ClienteViewModel>>(list);
    }

    public async Task<ClienteViewModel> GetByIdAsync(int id)
    {
        var obj = await _clienteRepository.GetByIdAsync(id);
        return _mapper.Map<ClienteViewModel>(obj);
    }
    
    public async Task<ClienteViewModel> GetByLoginAsync(string email, string password)
    {
        var obj = await _clienteRepository.GetByLoginAsync(email, password);
        return _mapper.Map<ClienteViewModel>(obj);
    }

    public async Task AddAsync(ClienteViewModel obj)
    {
        var mapCliente = _mapper.Map<Domain.Entidades.Cadastros.Cliente.Cliente>(obj);
        await _clienteRepository.AddAsync(mapCliente);
    }

    public async Task UpdateAsync(ClienteViewModel obj)
    {
        var mapCliente = _mapper.Map<Domain.Entidades.Cadastros.Cliente.Cliente>(obj);
        await _clienteRepository.UpdateAsync(mapCliente);
    }

    public async Task DeleteAsync(int id)
    {
        await _clienteRepository.DeleteAsync(id);
    }
}
