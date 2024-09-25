using Application.DTOs.Cadastros.Cultura.ViewModel;
using Application.DTOs.Cadastros.Dashboard.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Dashboard.Mapping
{
    public class DashboardMappingProfile : Profile
    {
        public DashboardMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.Dashboard.Dashboard, DashboardViewModel>();
            CreateMap<DashboardViewModel, Domain.Entidades.Cadastros.Dashboard.Dashboard>();
        }
    }
}
