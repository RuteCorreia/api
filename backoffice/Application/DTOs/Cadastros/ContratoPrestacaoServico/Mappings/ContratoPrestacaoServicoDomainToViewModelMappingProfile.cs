using Application.DTOs.Cadastros.ContratoPrestacaoServico.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.ContratoPrestacaoServico.Mappings;

public class ContratoPrestacaoServicoDomainToViewModelMappingProfile : Profile
{
    public ContratoPrestacaoServicoDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.ContratoPrestacaoServico.ContratoPrestacaoServico, ContratoPrestacaoServicoViewModel>();
    }
}
