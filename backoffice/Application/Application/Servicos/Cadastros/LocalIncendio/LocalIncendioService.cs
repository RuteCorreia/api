using Application.DTOs.Cadastros.LocalIncendio.Interface;
using Application.DTOs.Cadastros.LocalIncendio.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.LocalIncendio;

namespace Application.Application.Servicos.Cadastros.LocalIncendio
{
    public class LocalIncendioService : ILocalIncendioService
    {
        private readonly ILocalIncendioRepository _localIncendioRepository;
        private readonly IMapper _mapper;

        public LocalIncendioService(IMapper mapper, ILocalIncendioRepository localIncendioRepository)
        {
            _localIncendioRepository = localIncendioRepository;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(LocalIncendioViewModel obj)
        {
            var mapProduto = _mapper.Map<Domain.Entidades.Cadastros.LocalIncendio.LocalIncendio>(obj);
            return await _localIncendioRepository.AddAsync(mapProduto);
        }
    }
}
