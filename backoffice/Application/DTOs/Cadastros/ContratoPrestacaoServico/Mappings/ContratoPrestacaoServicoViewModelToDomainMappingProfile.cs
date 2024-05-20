using Application.DTOs.Cadastros.ContratoPrestacaoServico.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.ContratoPrestacaoServico.Mappings;

public class ContratoPrestacaoServicoViewModelToDomainMappingProfile : Profile
{
	public ContratoPrestacaoServicoViewModelToDomainMappingProfile()
	{
        CreateMap<ContratoPrestacaoServicoViewModel, Domain.Entidades.Cadastros.ContratoPrestacaoServico.ContratoPrestacaoServico>();
    }
}
