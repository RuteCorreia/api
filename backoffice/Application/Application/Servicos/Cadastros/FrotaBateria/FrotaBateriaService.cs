using Application.DTOs.Cadastros.FrotaBateria.Interface;
using Application.DTOs.Cadastros.FrotaBateria.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.FrotaBateria;
using Helpers;
using System.Threading.Tasks;

namespace Application.Application.Servicos.Cadastros.FrotaBateria
{
    public class FrotaBateriaService : IFrotaBateriaService
    {
        private readonly IFrotaBateriaRepository _frotaBateriaRepository;
        private readonly IMapper _mapper;
        public FrotaBateriaService(
            IFrotaBateriaRepository frotaBateriaRepository,
            IMapper mapper)
        {
            _frotaBateriaRepository = frotaBateriaRepository;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(FrotaBateriaViewModel obj, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var mapFrotaBateria = _mapper.Map<Domain.Entidades.Cadastros.FrotaBateria.FrotaBateria>(obj);
            mapFrotaBateria.IdEmpresa = idEmpresaInt;
            return await _frotaBateriaRepository.AddAsync(mapFrotaBateria);
        }

        public async Task DeleteAsync(int id)
        {
            await _frotaBateriaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<FrotaBateriaViewModel>> GetAllAsync(string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var list = await _frotaBateriaRepository.GetAllAsync(idEmpresaInt);
            return _mapper.Map<IEnumerable<FrotaBateriaViewModel>>(list);
        }

        public async Task<IEnumerable<FrotaBateriaViewModel>> GetByIdAsync(int? id)
        {
            var obj = await _frotaBateriaRepository.GetByIdAsync(id);
            return _mapper.Map<IEnumerable<FrotaBateriaViewModel>>(obj);
        }

        public async Task<int> UpdateAsync(FrotaBateriaViewModel obj)
        {
            var mapFrotaBateria = _mapper.Map<Domain.Entidades.Cadastros.FrotaBateria.FrotaBateria>(obj);
            return await _frotaBateriaRepository.UpdateAsync(mapFrotaBateria);
        }
    }
}
