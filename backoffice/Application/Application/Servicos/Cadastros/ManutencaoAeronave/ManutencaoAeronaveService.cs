using Application.DTOs.Cadastros.ManutencaoAeronave.Interface;
using Application.DTOs.Cadastros.ManutencaoAeronave.ViewModel;
using Application.DTOs.Cadastros.ManutencaoAeronaveItemsRevisao.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.ManutencaoAeronave;
using Domain.Interfaces.Cadastros.ManutencaoAeronaveItemsRevisao;
using Helpers;

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

        public async Task<IEnumerable<ManutencaoAeronaveViewModel>> GetAllAsync(string? idEmpresa)
        {
            var idEmpresaAsNumber = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var list = await _manutencaoAeronaveRepository.GetAllAsync(idEmpresaAsNumber);
            return _mapper.Map<IEnumerable<ManutencaoAeronaveViewModel>>(list);
        }

        public async Task<ManutencaoAeronaveViewModel> GetByIdAsync(int id)
        {
            var obj = await _manutencaoAeronaveRepository.GetByIdAsync(id);
            var mappedObj = _mapper.Map<ManutencaoAeronaveViewModel>(obj);
            var itensRevisao = await _manutencaoItemsRevisaoRepository.GetAllByManutencaoAeronaveIdAsync(mappedObj.Id);
            
            mappedObj.ItensRevisao = itensRevisao.Select(x => new ManutencaoAeronaveItemsRevisaoViewModel
            {
                Id = x.Id,
                Item = x.Descricao
            });

            return mappedObj;
        }

        public async Task AddAsync(ManutencaoAeronaveViewModel obj, string? idEmpresa)
        {
            var idEmpresaAsNumber = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var mapManutencaoAeronave = _mapper.Map<Domain.Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave>(obj);
            mapManutencaoAeronave.IdEmpresa = idEmpresaAsNumber == 0 ? null : idEmpresaAsNumber;
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
            await _manutencaoItemsRevisaoRepository.DeleteByIdManutencaoAeronaveAsync(obj.Id);
            if (obj.ItensRevisao.Any())
            {
                var itens = obj.ItensRevisao.Select(x => new Domain.Entidades.Cadastros.ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao
                {
                    Descricao = x.Item,
                    IdManutencaoAeronave = obj.Id
                });

                await _manutencaoItemsRevisaoRepository.AddAsync(itens);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _manutencaoAeronaveRepository.DeleteAsync(id);
        }
    }
}
