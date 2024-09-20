using Application.DTOs.Cadastros.Gerador.Interface;
using Application.DTOs.Cadastros.Gerador.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Gerador;
using Helpers;

namespace Application.Application.Servicos.Cadastros.Gerador
{
    public class GeradorService : IGeradorService
    {
        private readonly IGeradorRepository _geradorRepository;
        private readonly IMapper _mapper;
        public GeradorService(
            IGeradorRepository geradorRepository,
            IMapper mapper)
        {
            _geradorRepository = geradorRepository;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(GeradorViewModel obj, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var mapGerador = _mapper.Map<Domain.Entidades.Cadastros.Gerador.Gerador>(obj);
            mapGerador.IdEmpresa = idEmpresaInt;
            return await _geradorRepository.AddAsync(mapGerador);
        }

        public async Task DeleteAsync(int id)
        {
            await _geradorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<GeradorViewModel>> GetAllAsync(string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var list = await _geradorRepository.GetAllAsync(idEmpresaInt);
            return _mapper.Map<IEnumerable<GeradorViewModel>>(list);
        }

        public async Task<GeradorViewModel> GetByIdAsync(int? id)
        {
            var obj = await _geradorRepository.GetByIdAsync(id);
            return _mapper.Map<GeradorViewModel>(obj);
        }

        public async Task UpdateAsync(GeradorViewModel obj)
        {
            var mapBula = _mapper.Map<Domain.Entidades.Cadastros.Gerador.Gerador>(obj);
            await _geradorRepository.UpdateAsync(mapBula);
        }
    }
}
