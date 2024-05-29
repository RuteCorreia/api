using Application.DTOs.Cadastros.SubMenu.Interface;
using Application.DTOs.Cadastros.SubMenu.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Menu;
using Domain.Interfaces.Cadastros.SubMenu;

namespace Application.Application.Servicos.Cadastros.SubMenu
{
    public class SubMenuService : ISubMenuService
    {
        private readonly ISubMenuRepository _subMenuRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public SubMenuService(ISubMenuRepository subMenuRepository, IMapper mapper, IMenuRepository menuRepository)
        {
            _subMenuRepository = subMenuRepository;
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(SubMenuViewModel obj)
        {
            var isMenu = await _menuRepository.GetByIdAsync(obj.MenuItemId);
            if(isMenu is null)
            {
                //means that is submenu
                var submenuId = await GetByIdAsync(obj.MenuItemId);
                obj.SubMenuItemId = submenuId.SubMenuId;
                obj.MenuItemId = 0;
            }
            var mapSubMenu = _mapper.Map<Domain.Entidades.Cadastros.SubMenu.SubMenu>(obj);
            if (isMenu is null) mapSubMenu.MenuItemId = null;
            mapSubMenu.SubMenuItemId = isMenu is null ? obj.SubMenuItemId : null;
            await _subMenuRepository.AddAsync(mapSubMenu);
        }

        public async Task DeleteAsync(int id)
        {
            await _subMenuRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<SubMenuViewModel>> GetAllAsync()
        {
            var list = await _subMenuRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SubMenuViewModel>>(list);
        }

        public async Task<SubMenuViewModel> GetByIdAsync(int id)
        {
            var obj = await _subMenuRepository.GetByIdAsync(id);
            var ret = _mapper.Map<SubMenuViewModel>(obj);
            if(obj.MenuItemId is null) ret.MenuItemId = (int)obj.SubMenuItemId;
            return ret;
        }

        public async Task UpdateAsync(SubMenuViewModel obj)
        {
            var isMenu = await _menuRepository.GetByIdAsync(obj.MenuItemId);
            if(isMenu is null)
            {
                //means that is submenu
                var submenuId = await GetByIdAsync(obj.MenuItemId);
                obj.SubMenuItemId = submenuId.SubMenuId;
                obj.MenuItemId = 0;
            }
           
            var mapSubMenu = _mapper.Map<Domain.Entidades.Cadastros.SubMenu.SubMenu>(obj);

            if (isMenu is null)
                mapSubMenu.MenuItemId = null;
            else
                mapSubMenu.SubMenuItemId = null;

            await _subMenuRepository.UpdateAsync(mapSubMenu);
        }
    }
}
