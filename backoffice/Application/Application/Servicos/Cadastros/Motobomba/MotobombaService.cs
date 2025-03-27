using Application.DTOs.Cadastros.Motobomba.Interface;
using Application.DTOs.Cadastros.Motobomba.ViewModel;
using Application.DTOs.Cadastros.Pistas.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Motobomba;
using Helpers;

namespace Application.Application.Servicos.Cadastros.Motobomba
{
    public class MotobombaService : IMotobombaService
    {
        private readonly IMotobombaRepository _motobombaRepository;
        private readonly IMapper _mapper;
        public MotobombaService(
            IMotobombaRepository motobombaRepository,
            IMapper mapper)
        {
            _motobombaRepository = motobombaRepository;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(MotobombaViewModel obj, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var mapMotobomba = _mapper.Map<Domain.Entidades.Cadastros.Motobomba.Motobomba>(obj);
            mapMotobomba.IdEmpresa = idEmpresaInt;
            return await _motobombaRepository.AddAsync(mapMotobomba);
        }

        public async Task DeleteAsync(int id)
        {
            await _motobombaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<MotobombaViewModel>> GetAllAsync(string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var list = await _motobombaRepository.GetAllAsync(idEmpresaInt);

            return _mapper.Map<IEnumerable<MotobombaViewModel>>(list);
        }

        public async Task<IEnumerable<MotobombaViewModel>> GetByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("O nome não pode ser nulo ou vazio.", nameof(name));
            }
            var motobombaes = await _motobombaRepository.GetByNameAsync(name);

            return motobombaes.Select(p => _mapper.Map<MotobombaViewModel>(p));
        }

        public async Task<MotobombaViewModel> GetByIdAsync(int? id)
        {
            var obj = await _motobombaRepository.GetByIdAsync(id);

            return _mapper.Map<MotobombaViewModel>(obj);
        }

        public async Task UpdateAsync(MotobombaViewModel obj)
        {
            var mapMotobomba = _mapper.Map<Domain.Entidades.Cadastros.Motobomba.Motobomba>(obj);

            await _motobombaRepository.UpdateAsync(mapMotobomba);
        }
    }
}
