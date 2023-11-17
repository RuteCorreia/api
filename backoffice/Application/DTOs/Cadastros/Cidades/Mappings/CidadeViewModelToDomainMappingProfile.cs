using Application.DTOs.Cadastros.Cidades.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Cidades.Mappings;

public class CidadeViewModelToDomainMappingProfile : Profile
{
    public CidadeViewModelToDomainMappingProfile()
    {
        CreateMap<CidadeViewModel, Domain.Entidades.Cadastros.Cidades.Cidades>();
    }
}
