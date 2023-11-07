using Application.Application.Servicos.Genericos;
using Application.DTOs.Cadastros.Cidades.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Cidades;
using Domain.Interfaces.Genericos;

namespace Application.Application.Servicos.Cadastros.Cidades
{
    public class CidadesService : ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepository;
        private readonly IMapper _mapper;
        public CidadesService(ICidadeRepository cidadeRepository, IMapper mapper) 
        {
            _cidadeRepository = cidadeRepository;
            _mapper = mapper;
        }

        public Task<IEnumerable<CidadeViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
