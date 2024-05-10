using Application.DTOs.Cadastros.Combustivel.ViewModel;
using Application.DTOs.Cadastros.Componentes.Interface;
using Application.DTOs.Cadastros.Componentes.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Combustivel;
using Domain.Interfaces.Cadastros.Componentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Servicos.Cadastros.Componentes
{
    public class ComponentesService : IComponentesService
    {
        private readonly IComponenteRepository _componenteRepository;
        private readonly IMapper _mapper;

        public ComponentesService(IMapper mapper, IComponenteRepository componenteRepository)
        {
            _componenteRepository = componenteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ComponentesViewModel>> GetAllAsync(string? idEmpresa)
        {
            var idEmpresaInt = !string.IsNullOrEmpty(idEmpresa) ? Convert.ToInt32(idEmpresa) : 0;
            var list = await _componenteRepository.GetAllAsync(idEmpresaInt);
            return _mapper.Map<IEnumerable<ComponentesViewModel>>(list);
        }

        public async Task<ComponentesViewModel> GetByIdAsync(int id)
        {
            var obj = await _componenteRepository.GetByIdAsync(id);
            return _mapper.Map<ComponentesViewModel>(obj);
        }

        public async Task AddAsync(ComponentesViewModel obj, string? idEmpresa)
        {
            var mapComponente = _mapper.Map<Domain.Entidades.Cadastros.Componentes.Componentes>(obj);
            var idEmpresaAsNumber = !string.IsNullOrEmpty(idEmpresa) ? Convert.ToInt32(idEmpresa) : 0;
            mapComponente.IdEmpresa = idEmpresaAsNumber == 0 ? null : idEmpresaAsNumber;
            await _componenteRepository.AddAsync(mapComponente);
        }

        public async Task UpdateAsync(ComponentesViewModel obj)
        {
            var mapCombustivel = _mapper.Map<Domain.Entidades.Cadastros.Componentes.Componentes>(obj);
            await _componenteRepository.UpdateAsync(mapCombustivel);
        }

        public async Task DeleteAsync(int id)
        {
            await _componenteRepository.DeleteAsync(id);
        }
    }
}
