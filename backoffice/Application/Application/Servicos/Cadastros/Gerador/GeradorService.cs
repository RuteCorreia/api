using Application.DTOs.Cadastros.Gerador.Interface;
using Application.DTOs.Cadastros.Gerador.ViewModel;
using Application.DTOs.Cadastros.Pistas.ViewModel;
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
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var mapGerador = _mapper.Map<Domain.Entidades.Cadastros.Gerador.Gerador>(obj);
            mapGerador.QuantidadeHoras = mapGerador.QuantidadeHoras > 0 ? mapGerador.QuantidadeHoras * 3600000 : mapGerador.QuantidadeHoras;
            mapGerador.QuantidadeHorasTroca = mapGerador.QuantidadeHorasTroca > 0 ? mapGerador.QuantidadeHorasTroca * 3600000 : mapGerador.QuantidadeHorasTroca;
            mapGerador.IdEmpresa = idEmpresaInt;
            return await _geradorRepository.AddAsync(mapGerador);
        }

        public async Task DeleteAsync(int id, string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            await _geradorRepository.DeleteAsync(id, idEmpresaInt);
        }

        public async Task<IEnumerable<GeradorViewModel>> GetAllAsync(string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var list = await _geradorRepository.GetAllAsync(idEmpresaInt);
            foreach (var item in list) 
            {
                item.QuantidadeHoras = item.QuantidadeHoras / 3600000;
                item.QuantidadeHorasTroca = item.QuantidadeHorasTroca / 3600000;
            }
            return _mapper.Map<IEnumerable<GeradorViewModel>>(list);
        }

        public async Task<IEnumerable<GeradorViewModel>> GetByNameAsync(string name, string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("O nome não pode ser nulo ou vazio.", nameof(name));
            }
            var geradores = await _geradorRepository.GetByNameAsync(name, idEmpresaInt);

            return geradores.Select(p => _mapper.Map<GeradorViewModel>(p));
        }

        public async Task<GeradorViewModel> GetByIdAsync(int? id, string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var obj = await _geradorRepository.GetByIdAsync(id, idEmpresaInt);
            obj.QuantidadeHoras = obj.QuantidadeHoras / 3600000;
            obj.QuantidadeHorasTroca = obj.QuantidadeHorasTroca / 3600000;
            return _mapper.Map<GeradorViewModel>(obj);
        }

        public async Task UpdateAsync(GeradorViewModel obj)
        {
            var mapGerador = _mapper.Map<Domain.Entidades.Cadastros.Gerador.Gerador>(obj);
            mapGerador.QuantidadeHoras = mapGerador.QuantidadeHoras > 0 ? mapGerador.QuantidadeHoras * 3600000 : mapGerador.QuantidadeHoras;
            mapGerador.QuantidadeHorasTroca = mapGerador.QuantidadeHorasTroca > 0 ? mapGerador.QuantidadeHorasTroca * 3600000 : mapGerador.QuantidadeHorasTroca;
            await _geradorRepository.UpdateAsync(mapGerador);
        }
    }
}
