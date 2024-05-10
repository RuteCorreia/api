using Application.DTOs.Cadastros.Menu.Interface;
using Application.DTOs.Cadastros.Menu.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Menu;

namespace Application.Application.Servicos.Cadastros.Menu;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;
    private readonly IMapper _mapper;

    public MenuService(IMenuRepository menuRepository, IMapper mapper)
    {
        _menuRepository = menuRepository;
        _mapper = mapper;
    }

    public async Task AddAsync(MenuViewModel obj)
    {
        var mapMenu = _mapper.Map<Domain.Entidades.Cadastros.Menu.Menu>(obj);
        await _menuRepository.AddAsync(mapMenu);
    }

    public async Task DeleteAsync(int id)
    {
        await _menuRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<MenuViewModel>> GetAllAsync(string? idEmpresa)
    {
        var idEmpresaInt = !string.IsNullOrEmpty(idEmpresa) ? Convert.ToInt32(idEmpresa) : 0;
        var list = await _menuRepository.GetAllAsync(idEmpresaInt);
        return _mapper.Map<IEnumerable<MenuViewModel>>(list);
    }

    public async Task<MenuViewModel> GetByIdAsync(int id)
    {
        var obj = await _menuRepository.GetByIdAsync(id);
        return _mapper.Map<MenuViewModel>(obj);
    }

    public async Task UpdateAsync(MenuViewModel obj)
    {
        var mapMenu = _mapper.Map<Domain.Entidades.Cadastros.Menu.Menu>(obj);
        await _menuRepository.UpdateAsync(mapMenu);
    }
}