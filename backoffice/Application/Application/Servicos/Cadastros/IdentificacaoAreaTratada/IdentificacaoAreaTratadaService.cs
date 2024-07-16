using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;
using Application.DTOs.Cadastros.Frota.ViewModel;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.Interface;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Frota;
using Domain.Interfaces.Cadastros.IdentificacaoAreaTratada;
using Helpers;
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

        public async Task<AreaTratadaViewModel> GetByIdAsync(int id)
        {
            var obj = await _identificacaoAreaTratadaRepository.GetByIdAsync(id);
            return _mapper.Map<AreaTratadaViewModel>(obj);
        }

        public async Task<int> AddAsync(AreaTratadaViewModel obj, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var mapIdentificacaoAreaTratada = _mapper.Map<Domain.Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada>(obj);
            mapIdentificacaoAreaTratada.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;

            if (obj.Id > 0) 
            {
                await _identificacaoAreaTratadaRepository.UpdateAsync(mapIdentificacaoAreaTratada);
                return mapIdentificacaoAreaTratada.Id;
            }  else {
                return await _identificacaoAreaTratadaRepository.AddAsync(mapIdentificacaoAreaTratada);
            }
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

        public async Task<AreaTratadaViewModel> GetForExportExcelAsync(int id)
        {
            var obj = await _identificacaoAreaTratadaRepository.GetForExportExcelAsync(id);
            return _mapper.Map<AreaTratadaViewModel>(obj);
        }
    }
}
