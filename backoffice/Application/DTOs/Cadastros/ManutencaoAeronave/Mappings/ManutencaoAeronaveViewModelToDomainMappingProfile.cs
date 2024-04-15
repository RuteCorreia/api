using Application.DTOs.Cadastros.ManutencaoAeronave.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.ManutencaoAeronave.Mappings;

public class ManutencaoAeronaveViewModelToDomainMappingProfile : Profile
{
    public ManutencaoAeronaveViewModelToDomainMappingProfile()
    {
        CreateMap<ManutencaoAeronaveViewModel, Domain.Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave>();
    }
}
