using Domain.Entidades.Cadastros.AuxiliarPista;

namespace Domain.Interfaces.Cadastros.AuxiliarPista
{
    public interface IAuxiliarPistaRepository
    {
        Task<int> AddAsync(Entidades.Cadastros.AuxiliarPista.AuxiliarPista obj);
    }
}
