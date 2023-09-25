using Domain.Entidades.Base;
using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _baseRepository;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public TEntity Inserir<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {
            Validate(obj, Activator.CreateInstance<TValidator>());
            _baseRepository.Inserir(obj);
            return obj;
        }

        public void Remover(int id) => _baseRepository.Remover(id);

        public IList<TEntity> Listar() => _baseRepository.ListarTodos();

        public TEntity BuscarPorId(int id) => _baseRepository.BuscarPorId(id);

        public TEntity Atualizar<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {
            Validate(obj, Activator.CreateInstance<TValidator>());
            _baseRepository.Atualizar(obj);
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
