using Application.DTOs.Cadastros.FrotaGerador.Interface;
using Application.DTOs.Cadastros.FrotaGerador.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.FrotaGerador;
using Domain.Interfaces.Cadastros.Gerador;
using Helpers;

namespace Application.Application.Servicos.Cadastros.FrotaGerador
{
    public class FrotaGeradorService : IFrotaGeradorService
    {
        private readonly IFrotaGeradorRepository _frotaGeradorRepository;
        private readonly IGeradorRepository _geradorRepository;
        private readonly IMapper _mapper;
        public FrotaGeradorService(
            IFrotaGeradorRepository frotaGeradorRepository,
            IGeradorRepository geradorRepository,
            IMapper mapper)
        {
            _frotaGeradorRepository = frotaGeradorRepository;
            _geradorRepository = geradorRepository;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(FrotaGeradorViewModel obj, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var mapFrotaGerador = _mapper.Map<Domain.Entidades.Cadastros.FrotaGerador.FrotaGerador>(obj);
            mapFrotaGerador.IdEmpresa = idEmpresaInt;
            mapFrotaGerador.HorasUso = mapFrotaGerador.HoraFim - mapFrotaGerador.HoraInicio;
            
            if (mapFrotaGerador.IdGerador != null && mapFrotaGerador.IdGerador > 0 
                && mapFrotaGerador.HoraFim != null && mapFrotaGerador.HoraFim > 0 )
            {
                long horasfim = (long)mapFrotaGerador.HoraFim;
                long horaFimEmMilissegundos = horasfim * 3600000;

                await _geradorRepository.UpdateHorasAtualAsync(mapFrotaGerador.IdGerador, horaFimEmMilissegundos, mapFrotaGerador.DataTrocaOleo);
            }

            return await _frotaGeradorRepository.AddAsync(mapFrotaGerador);
        }

        public async Task DeleteAsync(int id)
        {
            await _frotaGeradorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<FrotaGeradorViewModel>> GetAllAsync(string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var list = await _frotaGeradorRepository.GetAllAsync(idEmpresaInt);
            return _mapper.Map<IEnumerable<FrotaGeradorViewModel>>(list);
        }

        public async Task<IEnumerable<FrotaGeradorViewModel>> GetByIdAsync(int? id)
        {
            var obj = await _frotaGeradorRepository.GetByIdAsync(id);
            return _mapper.Map<IEnumerable<FrotaGeradorViewModel>>(obj);
        }

        public async Task<int> UpdateAsync(FrotaGeradorViewModel obj)
        {
            var mapFrotaGerador = _mapper.Map<Domain.Entidades.Cadastros.FrotaGerador.FrotaGerador>(obj);
            return await _frotaGeradorRepository.UpdateAsync(mapFrotaGerador);
        }
    }
}
