using Application.DTOs.Cadastros.Classe.Interface;
using Application.DTOs.Cadastros.Classe.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Classe;

namespace Application.Application.Servicos.Cadastros.Classe
{
    public class ClasseService : IClasseService
    {
        private readonly IClasseRepository _classeRepository;
        private readonly IMapper _mapper;

        public ClasseService(IMapper mapper, IClasseRepository classeRepository)
        {
            _classeRepository = classeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClasseViewModel>> GetAllAsync()
        {
            var list = await _classeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClasseViewModel>>(list);
        }

        public async Task<IEnumerable<ClasseViewModel>> GetByTipoServicoAsync(int idTipoDeServico)
        {
            var list = await _classeRepository.GetByTipoServicoAsync(idTipoDeServico);
            return _mapper.Map<IEnumerable<ClasseViewModel>>(list);
        }

        public async Task<ClasseViewModel?> GetByIdAsync(int id)
        {
            var obj = await _classeRepository.GetByIdAsync(id);
            return obj == null ? null : _mapper.Map<ClasseViewModel>(obj);
        }

        public async Task<ClasseViewModel> AddAsync(ClasseCreateViewModel obj)
        {
            var entity = _mapper.Map<Domain.Entidades.Cadastros.Classe.Classe>(obj);
            entity.ClassePublica = "S";
            entity.IdEmpresa = obj.IdEmpresa;
            var created = await _classeRepository.AddAsync(entity);
            return _mapper.Map<ClasseViewModel>(created);
        }

        public async Task<string?> UpdateAsync(int id, ClasseUpdateViewModel obj)
        {
            var existing = await _classeRepository.GetByIdAsync(id);
            if (existing == null)
                return "Classe não encontrada";

            if (await _classeRepository.ExistsDuplicateAsync(obj.Descricao, existing.IdTipoDeServico, id))
                return "Já existe uma classe com esta descrição para o mesmo tipo de serviço";

            existing.Descricao = obj.Descricao;
            await _classeRepository.UpdateAsync(existing);
            return null;
        }

        public async Task<string?> DeleteAsync(int id)
        {
            var existing = await _classeRepository.GetByIdAsync(id);
            if (existing == null)
                return "Classe não encontrada";

            if (existing.ClassePublica != "S")
                return "Classes padrão do sistema não podem ser excluídas";

            if (await _classeRepository.HasProdutoVinculadoAsync(id))
                return "Classe não pode ser excluída pois está vinculada a produtos";

            await _classeRepository.DeleteAsync(existing);
            return null;
        }
    }
}
