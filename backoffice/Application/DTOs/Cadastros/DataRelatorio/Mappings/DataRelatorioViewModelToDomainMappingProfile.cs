using Application.DTOs.Cadastros.DadosResponsavel.ViewModel;
using Application.DTOs.Cadastros.DataRelatorio.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.DataRelatorio.Mappings
{
    public class DataRelatorioViewModelToDomainMappingProfile : Profile
    {
        public DataRelatorioViewModelToDomainMappingProfile()
        {
            CreateMap<DataRelatorioViewModel, Domain.Entidades.Cadastros.DataRelatorio.DataRelatorio>();
        }
    }
}
