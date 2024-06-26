using Application.DTOs.Cadastros.Frota.ViewModel;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.Interface;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.ViewModel;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Frota;
using Domain.Interfaces.Cadastros.IdentificacaoAreaTratada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Servicos.Cadastros.IdentificacaoAreaTratada
{
    public class IdentificacaoAreaTratadaService : IIdentificacaoAreaTratadaService
    {
        private readonly IIdentificacaoAreaTratadaRepository _identificacaoAreaTratadaRepository;
        private readonly IMapper _mapper;

        public IdentificacaoAreaTratadaService(IMapper mapper, IIdentificacaoAreaTratadaRepository identificacaoAreaTratadaRepository)
        {
            _identificacaoAreaTratadaRepository = identificacaoAreaTratadaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IdentificacaoAreaTratadaViewModel>> GetAllAsync()
        {
            var list = await _identificacaoAreaTratadaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<IdentificacaoAreaTratadaViewModel>>(list);
        }

        public async Task<IdentificacaoAreaTratadaViewModel> GetByIdAsync(int id)
        {
            var obj = await _identificacaoAreaTratadaRepository.GetByIdAsync(id);
            return _mapper.Map<IdentificacaoAreaTratadaViewModel>(obj);
        }

        public async Task<int> AddAsync(AreaTratadaViewModel obj)
        {
            var mapIdentificacaoAreaTratada = _mapper.Map<Domain.Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada>(obj);
            var identificacaoAreaTratada = _identificacaoAreaTratadaRepository.AddAsync(mapIdentificacaoAreaTratada);
            return identificacaoAreaTratada.Result;
        }

        public async Task UpdateAsync(IdentificacaoAreaTratadaViewModel obj)
        {
            var mapIdentificacaoAreaTratada = _mapper.Map<Domain.Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada>(obj);
            await _identificacaoAreaTratadaRepository.UpdateAsync(mapIdentificacaoAreaTratada);
        }

        public async Task DeleteAsync(int id)
        {
            await _identificacaoAreaTratadaRepository.DeleteAsync(id);
        }
    }
}
