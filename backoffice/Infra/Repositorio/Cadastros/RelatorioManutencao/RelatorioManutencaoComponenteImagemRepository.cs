using Domain.Entidades.Cadastros.RelatorioManutencao;
using Domain.Interfaces.Cadastros.RelatorioManutencao;
using Helpers;
using Infra.Configuracao;

namespace Infra.Repositorio.Cadastros.RelatorioManutencao
{
    public class RelatorioManutencaoComponenteImagemRepository : IRelatorioManutencaoComponenteImagemRepository
    {
        private readonly ContextBase _contextBase;

        public RelatorioManutencaoComponenteImagemRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }
        public async Task AddAsync(RelatorioManutencaoComponenteImagem obj)
        {
            await _contextBase.AddAsync(obj);
            await _contextBase.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entityToRemove = await GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(entityToRemove))
            {
                _contextBase.Remove(entityToRemove);
                await _contextBase.SaveChangesAsync();
            }
        }

        public async Task<RelatorioManutencaoComponenteImagem> GetByIdAsync(int id)
        {
            var obj = await _contextBase.RelatorioManutencaoComponenteImagem.FindAsync(id);
            return obj;
        }

        public async Task UpdateAsync(RelatorioManutencaoComponenteImagem obj)
        {
            var objeto = await _contextBase.RelatorioManutencaoComponenteImagem.FindAsync(obj.Id);

            if (objeto != null)
            {
                // Atualiza as propriedades do objeto encontrado
                objeto.IdEmpresa = obj.IdEmpresa;
                objeto.IdRelatorioManutencaoComponente = obj.IdRelatorioManutencaoComponente;
                objeto.Legenda = obj.Legenda;
                objeto.Imagem = obj.Imagem;

                // Atualiza o objeto no contexto
                _contextBase.RelatorioManutencaoComponenteImagem.Update(objeto);
                await _contextBase.SaveChangesAsync();
            }
            else
            {
                // Lidar com o caso em que o objeto não foi encontrado
                throw new KeyNotFoundException("Objeto não encontrado.");
            }
        }
    }
}
