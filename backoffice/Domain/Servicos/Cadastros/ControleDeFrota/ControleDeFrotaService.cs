using Domain.Interfaces.Cadastros.CombateIncendioDecolagemPouso;
using Domain.Interfaces.Cadastros.Combustivel;
using Domain.Interfaces.Cadastros.ControleDeFrota;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using Entities.Entidades.Cadastros.CombateIncendio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.ControleDeFrota
{
    public class ControleDeFrotaService : BaseService<Entities.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>, IControleDeFrotaService
    {
        private readonly IControleDeFrotaRepository _controleDeFrotaRepository;

        public ControleDeFrotaService(IControleDeFrotaRepository controleDeFrotaRepository) : base(controleDeFrotaRepository)
        {
            _controleDeFrotaRepository = controleDeFrotaRepository;
        }

        public Entities.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota BuscarPorId(int? Id)
        {
            var obj = _controleDeFrotaRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota> ListarTodosControlesDeFrota()
        {
            var obj = _controleDeFrotaRepository.ListarTodosControlesDeFrota();
            return obj;
        }
    }
}
