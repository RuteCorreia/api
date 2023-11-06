using Domain.Interfaces.Cadastros.AplicacaoRelatorioItem;
using Domain.Interfaces.Cadastros.Bula;
using Domain.Interfaces.Cadastros.PlanoContrato;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using Domain.Entidades.Cadastros.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Bula
{
    public class BulaService : BaseService<Domain.Entidades.Cadastros.Empresa.Bula>, IBulaService
    {
        private readonly IBulaRepository _bulaRepository;

        public BulaService(IBulaRepository bulaRepository) : base(bulaRepository)
        {
            _bulaRepository = bulaRepository;
        }

        public Domain.Entidades.Cadastros.Empresa.Bula BuscarPorId(int? Id)
        {
            var obj = _bulaRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.Empresa.Bula> ListarTodasBulas()
        {
            var obj = _bulaRepository.ListarTodasBulas();
            return obj;
        }
    }
}
