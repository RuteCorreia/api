using Application.DTOs.Cadastros.MenuUsuario.ViewModel;
using Application.DTOs.Cadastros.SubMenu.Interface;
using Application.DTOs.Cadastros.SubMenu.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.MenuUsuario;
using Domain.Interfaces.Cadastros.SubMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Servicos.Cadastros.SubMenu
{
    public class SubMenuService : ISubMenuService
    {
        private readonly ISubMenuRepository _subMenuRepository;
        private readonly IMapper _mapper;

        public SubMenuService(ISubMenuRepository subMenuRepository, IMapper mapper)
        {
            _subMenuRepository = subMenuRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(SubMenuViewModel obj)
        {
            var mapSubMenu = _mapper.Map<Domain.Entidades.Cadastros.SubMenu.SubMenu>(obj);
            await _subMenuRepository.AddAsync(mapSubMenu);
        }

        public async Task DeleteAsync(int id)
        {
            await _subMenuRepository.DeleteAsync(id);
        }

        public IEnumerable<SubMenuViewModel> GetAllAsync()
        {
            var list = _subMenuRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SubMenuViewModel>>(list);
        }

        public async Task<SubMenuViewModel> GetByIdAsync(int id)
        {
            var obj = await _subMenuRepository.GetByIdAsync(id);
            return _mapper.Map<SubMenuViewModel>(obj);
        }

        public async Task UpdateAsync(SubMenuViewModel obj)
        {
            var mapSubMenu = _mapper.Map<Domain.Entidades.Cadastros.SubMenu.SubMenu>(obj);
            await _subMenuRepository.UpdateAsync(mapSubMenu);
        }
    }
}
