using Application.DTOs.Cadastros.ManutencaoAeronave.Interface;
using Application.DTOs.Cadastros.ManutencaoAeronave.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.ManutencaoAeronave;
using Domain.Interfaces.Cadastros.ManutencaoAeronaveItemsRevisao;

namespace Application.Application.Servicos.Cadastros.ManutencaoAeronave
{
    public class ManutencaoAeronaveService : IManutencaoAeronaveService
    {
        private readonly IManutencaoAeronaveRepository _manutencaoAeronaveRepository;
        private readonly IManutencaoAeronaveItemsRevisaoRepository _manutencaoItemsRevisaoRepository;
        private readonly IMapper _mapper;

        public ManutencaoAeronaveService(
            IMapper mapper, 
            IManutencaoAeronaveRepository manutencaoAeronaveRepository,
            IManutencaoAeronaveItemsRevisaoRepository manutencaoItemsRevisaoRepository
        )
        {
            _manutencaoAeronaveRepository = manutencaoAeronaveRepository;
            _manutencaoItemsRevisaoRepository = manutencaoItemsRevisaoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ManutencaoAeronaveViewModel>> GetAllAsync()
        {
            var list = await _manutencaoAeronaveRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ManutencaoAeronaveViewModel>>(list);
        }

        public async Task<ManutencaoAeronaveViewModel> GetByIdAsync(int id)
        {
            var obj = await _manutencaoAeronaveRepository.GetByIdAsync(id);
            return _mapper.Map<ManutencaoAeronaveViewModel>(obj);
        }

        public async Task AddAsync(ManutencaoAeronaveViewModel obj)
        {
            var mapManutencaoAeronave = _mapper.Map<Domain.Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave>(obj);
            var objManutencao = await _manutencaoAeronaveRepository.AddAsync(mapManutencaoAeronave);

            if (obj.ItensRevisao.Any())
            {
                var itens = obj.ItensRevisao.Select(x => new Domain.Entidades.Cadastros.ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao
                {
                    Descricao = x.Item,
                    IdManutencaoAeronave = objManutencao.Id
                });

                await _manutencaoItemsRevisaoRepository.AddAsync(itens);
            }
        }

        public async Task UpdateAsync(ManutencaoAeronaveViewModel obj)
        {
            var mapManutencaoAeronave = _mapper.Map<Domain.Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave>(obj);
            await _manutencaoAeronaveRepository.UpdateAsync(mapManutencaoAeronave);
        }

        public async Task DeleteAsync(int id)
        {
            await _manutencaoAeronaveRepository.DeleteAsync(id);
        }
    }
}
