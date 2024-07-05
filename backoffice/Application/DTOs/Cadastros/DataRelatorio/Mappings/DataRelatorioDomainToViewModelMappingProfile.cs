using Application.DTOs.Cadastros.DadosResponsavel.ViewModel;
using Application.DTOs.Cadastros.DataRelatorio.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.DataRelatorio.Mappings
{
    public class DataRelatorioDomainToViewModelMappingProfile : Profile
    {
        public DataRelatorioDomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.DataRelatorio.DataRelatorio, DataRelatorioViewModel>();
        }
    }
}
