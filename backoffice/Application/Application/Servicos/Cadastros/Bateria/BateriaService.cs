using Application.DTOs.Cadastros.Bateria.Interface;
using Application.DTOs.Cadastros.Bateria.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Bateria;
using Helpers;

namespace Application.Application.Servicos.Cadastros.Bateria
{
    public class BateriaService : IBateriaService
    {
        private readonly IBateriaRepository _bateriaRepository;
        private readonly IMapper _mapper;
        public BateriaService(
            IBateriaRepository bateriaRepository,
            IMapper mapper)
        {
            _bateriaRepository = bateriaRepository;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(BateriaViewModel obj, string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var mapBateria = _mapper.Map<Domain.Entidades.Cadastros.Bateria.Bateria>(obj);
            mapBateria.IdEmpresa = idEmpresaInt;
            return await _bateriaRepository.AddAsync(mapBateria);
        }

        public async Task DeleteAsync(int id, string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            await _bateriaRepository.DeleteAsync(id, idEmpresaInt);
        }

        public async Task<IEnumerable<BateriaViewModel>> GetAllAsync(string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var list = await _bateriaRepository.GetAllAsync(idEmpresaInt);
            return _mapper.Map<IEnumerable<BateriaViewModel>>(list);
        }

        public async Task<BateriaViewModel> GetByIdAsync(int? id, string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var obj = await _bateriaRepository.GetByIdAsync(id, idEmpresaInt);
            return _mapper.Map<BateriaViewModel>(obj);
        }

        public async Task<IEnumerable<BateriaViewModel>> GetByNameAsync(string name, string? idEmpresa)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("O nome não pode ser nulo ou vazio.", nameof(name));
            }
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var baterias = await _bateriaRepository.GetByNameAsync(name, idEmpresaInt);

            return baterias.Select(p => _mapper.Map<BateriaViewModel>(p));
        }

        public async Task UpdateAsync(BateriaViewModel obj)
        {
            var mapBula = _mapper.Map<Domain.Entidades.Cadastros.Bateria.Bateria>(obj);
            await _bateriaRepository.UpdateAsync(mapBula);
        }
    }
}
