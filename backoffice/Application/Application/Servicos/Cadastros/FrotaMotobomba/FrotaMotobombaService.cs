using Application.DTOs.Cadastros.FrotaGerador.ViewModel;
using Application.DTOs.Cadastros.FrotaMotobomba.Interface;
using Application.DTOs.Cadastros.FrotaMotobomba.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.FrotaGerador;
using Domain.Interfaces.Cadastros.FrotaMotobomba;
using Helpers;

namespace Application.Application.Servicos.Cadastros.FrotaMotobomba
{
    public class FrotaMotobombaService : IFrotaMotobombaService
    { 
        private readonly IFrotaMotobombaRepository _frotaMotobombaRepository;
        private readonly IMapper _mapper;
        public FrotaMotobombaService(
            IFrotaMotobombaRepository frotaMotobombaRepository,
            IMapper mapper)
        {
            _frotaMotobombaRepository = frotaMotobombaRepository;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(FrotaMotobombaViewModel obj, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var mapFrotaMotobomba = _mapper.Map<Domain.Entidades.Cadastros.FrotaMotobomba.FrotaMotobomba>(obj);
            mapFrotaMotobomba.IdEmpresa = idEmpresaInt;
            return await _frotaMotobombaRepository.AddAsync(mapFrotaMotobomba);
        }

        public async Task DeleteAsync(int id)
        {
            await _frotaMotobombaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<FrotaMotobombaViewModel>> GetAllAsync(string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var list = await _frotaMotobombaRepository.GetAllAsync(idEmpresaInt);
            return _mapper.Map<IEnumerable<FrotaMotobombaViewModel>>(list);
        }

        public async Task<FrotaMotobombaViewModel> GetByIdAsync(int? id)
        {
            var obj = await _frotaMotobombaRepository.GetByIdAsync(id);
            return _mapper.Map<FrotaMotobombaViewModel>(obj);
        }

        public async Task<int> UpdateAsync(FrotaMotobombaViewModel obj)
        {
            var mapFrotaMotobomba = _mapper.Map<Domain.Entidades.Cadastros.FrotaMotobomba.FrotaMotobomba>(obj);
            return await _frotaMotobombaRepository.UpdateAsync(mapFrotaMotobomba);
        }
    }
}
