using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Aeronave;

public interface IAeronaveRepository : IBaseRepository<Entidades.Cadastros.Aeronave.Aeronave>
{
    Entidades.Cadastros.Aeronave.Aeronave BuscarPorId(int? Id);
    List<Entidades.Cadastros.Aeronave.Aeronave> ListarTodasAeronaves();

}
