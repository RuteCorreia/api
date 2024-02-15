using Application.DTOs.Cadastros.Menu.ViewModel;
using Application.DTOs.Cadastros.MenuUsuario.Interface;
using Application.DTOs.Cadastros.MenuUsuario.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Menu;
using Domain.Interfaces.Cadastros.MenuUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Servicos.Cadastros.MenuUsuario
{
    public class MenuUsuarioService : IMenuUsuarioService
    {
        private readonly IMenuUsuarioRepository _menuUsuarioRepository;
        private readonly IMapper _mapper;

        public MenuUsuarioService(IMenuUsuarioRepository menuUsuarioRepository, IMapper mapper)
        {
            _menuUsuarioRepository = menuUsuarioRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(MenuUsuarioViewModel obj)
        {
            var mapMenuUsuario = _mapper.Map<Domain.Entidades.Cadastros.MenuUsuario.MenuUsuario>(obj);
            await _menuUsuarioRepository.AddAsync(mapMenuUsuario);
        }

        public async Task DeleteAsync(int id)
        {
            await _menuUsuarioRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<MenuUsuarioViewModel>> GetAllAsync()
        {
            var list = await _menuUsuarioRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MenuUsuarioViewModel>>(list);
        }

        public async Task UpdateAsync(MenuUsuarioViewModel obj)
        {
            var mapMenuUsuario = _mapper.Map<Domain.Entidades.Cadastros.MenuUsuario.MenuUsuario>(obj);
            await _menuUsuarioRepository.UpdateAsync(mapMenuUsuario);
        }

        public async Task<MenuUsuarioViewModel> GetByIdAsync(string id)
        {
            var obj = await _menuUsuarioRepository.GetByIdAsync(id);
            return _mapper.Map<MenuUsuarioViewModel>(obj);
        }
    }
}
