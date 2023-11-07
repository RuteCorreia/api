using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Aeronave;

public interface IAeronaveService : IBaseService<Entidades.Cadastros.Aeronave.Aeronave>
{
    Entidades.Cadastros.Aeronave.Aeronave BuscarPorId(int? Id);
    List<Entidades.Cadastros.Aeronave.Aeronave> ListarTodasAeronaves();
}
