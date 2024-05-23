using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Importação_Planilha
{
    /// <summary>
    /// Interface que define os serviços relacionados ao processamento de planilhas para um tipo específico de dados.
    /// </summary>
    /// <typeparam name="T">Tipo de dados associado à planilha.</typeparam>
    public interface IServicosPlanilha<T>
    {
        /// <summary>
        /// Inicia o processamento de dados para o serviço da planilha.
        /// </summary>
        void IniciarProcessamento();
    }
}
