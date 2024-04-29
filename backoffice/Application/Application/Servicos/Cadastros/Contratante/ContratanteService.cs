using Application.DTOs.Cadastros.Componentes.ViewModel;
using Application.DTOs.Cadastros.Contratante.Interface;
using Application.DTOs.Cadastros.Contratante.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Componentes;
using Domain.Interfaces.Cadastros.Contratante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Servicos.Cadastros.Contratante
{
    public class ContratanteService : IContratanteService
    {
        private readonly IContratanteRepository _contratanteRepository;
        private readonly IMapper _mapper;

        public ContratanteService(IMapper mapper, IContratanteRepository contratanteRepository)
        {
            _contratanteRepository = contratanteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContratanteViewModel>> GetAllAsync()
        {
            var list = await _contratanteRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ContratanteViewModel>>(list);
        }

        public async Task<ContratanteViewModel> GetByIdAsync(int id)
        {
            var obj = await _contratanteRepository.GetByIdAsync(id);
            return _mapper.Map<ContratanteViewModel>(obj);
        }

        public async Task AddAsync(ContratanteViewModel obj)
        {
            var mapContratante = _mapper.Map<Domain.Entidades.Cadastros.Contratante.Contratante>(obj);
            await _contratanteRepository.AddAsync(mapContratante);
        }

        public async Task UpdateAsync(ContratanteViewModel obj)
        {
            var mapContratante = _mapper.Map<Domain.Entidades.Cadastros.Contratante.Contratante>(obj);
            await _contratanteRepository.UpdateAsync(mapContratante);
        }

        public async Task DeleteAsync(int id)
        {
            await _contratanteRepository.DeleteAsync(id);
        }
    }
}
