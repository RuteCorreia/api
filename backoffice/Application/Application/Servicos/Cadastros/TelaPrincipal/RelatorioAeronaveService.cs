using Application.DTOs.Cadastros.Aeronave.Interface;
using Application.DTOs.Cadastros.AlvoBiologico.ViewModel;
using Application.DTOs.Cadastros.TelaPrincipal.Interface;
using Application.DTOs.Cadastros.TelaPrincipal.ViewModel;
using AutoMapper;
using Domain.Entidades.Cadastros.TelaPrincipal;
using Domain.Interfaces.Cadastros.TelaPrincipal;
using System.Globalization;

namespace Application.Application.Servicos.Cadastros.TelaPrincipal
{
    public class RelatorioAeronaveService : IRelatorioAeronaveService
    {
        private readonly IRelatorioAeronaveRepository _relatorioAeronaveRepository;
        private readonly IMapper _mapper;
        public RelatorioAeronaveService(
            IRelatorioAeronaveRepository relatorioAeronaveRepository,
            IMapper mapper)
        {
            _relatorioAeronaveRepository = relatorioAeronaveRepository;  
            _mapper = mapper;
        }
        public async Task<IEnumerable<RelatorioAeronaveViewModel>> GetAllAsync()
        {
            try
            {
                var relatoriosAeronave = await _relatorioAeronaveRepository.GetAllAsync();
                var viewModelList = _mapper.Map<IEnumerable<RelatorioAeronaveViewModel>>(relatoriosAeronave);

                var cultureInfo = new CultureInfo("pt-BR");

                foreach (var viewModel in viewModelList) 
                {
                    if (viewModel.TotalHoras > 0)
                    {
                        viewModel.Rendimento = viewModel.ExtensaoTotal / viewModel.TotalHoras;
                        viewModel.ValorHorasVoadas = viewModel.ValorTotal / viewModel.TotalHoras;
                    }
                    else
                    {
                        viewModel.Rendimento = null;
                        viewModel.ValorHorasVoadas = 0;
                    }

                    viewModel.ExtensaoTotalFormatado = $"{viewModel.ExtensaoTotal:N0} ha";
                    viewModel.ValorTotalFormatado = $"R$ {viewModel.ValorTotal.ToString("N2", cultureInfo)}";
                    viewModel.TotalHorasFormatado = $"{viewModel.TotalHoras.ToString("N2", cultureInfo)} horas";
                    viewModel.RendimentoFormatado = viewModel.Rendimento.HasValue
                        ? $"{viewModel.Rendimento.Value.ToString("N2", cultureInfo)} ha/hora"
                        : "N/A";
                    viewModel.ValorHorasVoadasFormatado = $"R$ {viewModel.ValorHorasVoadas.ToString("N2", cultureInfo)}";
                }

                return viewModelList;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter relatorios por aeronave.", ex);
            }
        }
    }
}
