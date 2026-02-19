using Dapper;
using Domain.Interfaces.Cadastros.Contratante;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio.Cadastros.Contratante
{
    public class ContratanteRepository : IContratanteRepository
    {
        private readonly ContextBase _contextBase;
        private readonly IDbConnection _dbConnection;

        public ContratanteRepository(ContextBase contextBase, IDbConnection dbConnection)
        {
            _contextBase = contextBase;
            _dbConnection = dbConnection;
        }

        public async Task<int> AddAsync(Domain.Entidades.Cadastros.Contratante.Contratante obj)
        {
            _contextBase.Add(obj);
            _contextBase.SaveChanges();
            return obj.Id;
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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.Contratante.Contratante>> GetAllAsync()
        {
            var entities = await _contextBase.Contratante
                .AsNoTracking()
                .ToListAsync();
            return entities;
        }

        public async Task<Domain.Entidades.Cadastros.Contratante.Contratante> GetByIdAsync(int? id)
        {
            var obj = await _contextBase.Contratante.FindAsync(id);
            return obj;
        }

        public async Task UpdateAsync(Domain.Entidades.Cadastros.Contratante.Contratante obj)
        {
            var objeto = await _contextBase.Contratante.FindAsync(obj.Id);
            objeto.CNPJ = obj.CNPJ;
            objeto.Nome = obj.Nome;
            objeto.RG = obj.RG;
            objeto.Endereco = obj.Endereco;
            objeto.UF = obj.UF;
            objeto.TipoContratante = obj.TipoContratante;
            objeto.InscricaoEstadual = obj.InscricaoEstadual;
            objeto.Cidade = obj.Cidade;
            objeto.CPF = obj.CPF;
            objeto.ContratanteRef = obj.ContratanteRef;
            objeto.IdEmpresa = obj.IdEmpresa;
            _contextBase.Contratante.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }
    }
}
