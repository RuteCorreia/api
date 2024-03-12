using Application.DTOs.Cadastros.Frota.ViewModel;
using Application.DTOs.Cadastros.ManutencaoAeronave.Interface;
using Application.DTOs.Cadastros.ManutencaoAeronave.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Frota;
using Domain.Interfaces.Cadastros.ManutencaoAeronave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Servicos.Cadastros.ManutencaoAeronave
{
    public class ManutencaoAeronaveService : IManutencaoAeronaveService
    {
        private readonly IManutencaoAeronaveRepository _manutencaoAeronaveRepository;
        private readonly IMapper _mapper;

        public ManutencaoAeronaveService(IMapper mapper, IManutencaoAeronaveRepository manutencaoAeronaveRepository)
        {
            _manutencaoAeronaveRepository = manutencaoAeronaveRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ManutencaoAeronaveViewModel>> GetAllAsync()
        {
            var list = await _manutencaoAeronaveRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ManutencaoAeronaveViewModel>>(list);
        }

        public async Task<ManutencaoAeronaveViewModel> GetByIdAsync(int id)
        {
            var obj = await _manutencaoAeronaveRepository.GetByIdAsync(id);
            return _mapper.Map<ManutencaoAeronaveViewModel>(obj);
        }

        public async Task AddAsync(ManutencaoAeronaveViewModel obj)
        {
            var mapManutencaoAeronave = _mapper.Map<Domain.Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave>(obj);
            await _manutencaoAeronaveRepository.AddAsync(mapManutencaoAeronave);
        }

        public async Task UpdateAsync(ManutencaoAeronaveViewModel obj)
        {
            var mapManutencaoAeronave = _mapper.Map<Domain.Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave>(obj);
            await _manutencaoAeronaveRepository.UpdateAsync(mapManutencaoAeronave);
        }

        public async Task DeleteAsync(int id)
        {
            await _manutencaoAeronaveRepository.DeleteAsync(id);
        }
    }
}
