using Application.DTOs.Cadastros.BulaAplicacao.Interface;
using Application.DTOs.Cadastros.BulaAplicacao.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.BulaAplicacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Servicos.Cadastros.BulaAplicacao
{
    public class BulaAplicacaoService : IBulaAplicacaoService
    {
        private readonly IBulaAplicacaoRepository _bulaRepository;
        private readonly IMapper _mapper;

        public BulaAplicacaoService(IMapper mapper, IBulaAplicacaoRepository bulaRepository)
        {
            _bulaRepository = bulaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BulaAplicacaoViewModel>> GetAllAsync()
        {
            var list = await _bulaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BulaAplicacaoViewModel>>(list);
        }

        public async Task<BulaAplicacaoViewModel> GetByIdAsync(int id)
        {
            var obj = await _bulaRepository.GetByIdAsync(id);
            return _mapper.Map<BulaAplicacaoViewModel>(obj);
        }

        public async Task AddAsync(BulaAplicacaoViewModel obj)
        {
            var mapBula = _mapper.Map<Domain.Entidades.Cadastros.Empresa.BulaAplicacao>(obj);
            await _bulaRepository.AddAsync(mapBula);
        }

        public async Task UpdateAsync(BulaAplicacaoViewModel obj)
        {
            var mapBula = _mapper.Map<Domain.Entidades.Cadastros.Empresa.BulaAplicacao>(obj);
            await _bulaRepository.UpdateAsync(mapBula);
        }

        public async Task DeleteAsync(int idRecomendacao, int idBula)
        {
            var bulaAplicacao = await _bulaRepository.getByIdBulaAndIdAlvoAsync(idRecomendacao, idBula);
            await _bulaRepository.DeleteAsync(bulaAplicacao.IdBulaAplicacao);
        }

        public async Task<IEnumerable<BulaAplicacaoViewModel>> GetByIdBulaAsync(int id)
        {
            var obj = await _bulaRepository.GetByIdBulaAsync(id);
            return _mapper.Map<List<BulaAplicacaoViewModel>>(obj);
        }
    }
}
