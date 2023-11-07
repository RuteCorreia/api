using Domain.Interfaces.Genericos;
using FluentValidation;

namespace Application.Application.Servicos.Genericos
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> _baseRepository;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<TEntity> Inserir<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {
            Validate(obj, Activator.CreateInstance<TValidator>());
            await _baseRepository.AddAsync(obj);
            return obj;
        }

        public async Task Remover(int id) => await _baseRepository.DeleteAsync(id);

        public async Task<IEnumerable<TEntity>> Listar() => await _baseRepository.GetAllAsync();

        public async Task<TEntity> BuscarPorId(int id) => await _baseRepository.GetByIdAsync(id);

        public async Task<TEntity> Atualizar<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {
            Validate(obj, Activator.CreateInstance<TValidator>());
            await _baseRepository.UpdateAsync(obj);
            return obj;
        }

        private void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }
    }
}
