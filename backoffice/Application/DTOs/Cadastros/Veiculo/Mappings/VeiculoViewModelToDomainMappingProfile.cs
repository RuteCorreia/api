using Application.DTOs.Cadastros.Veiculo.ViewModel;
using AutoMapper;
using Newtonsoft.Json;

namespace Application.DTOs.Cadastros.Veiculo.Mappings;

public class VeiculoViewModelToDomainMappingProfile : Profile
{
    public VeiculoViewModelToDomainMappingProfile()
    {
        CreateMap<VeiculoViewModel, Domain.Entidades.Cadastros.Veiculo.Veiculo>()
            .ForMember(dest => dest.Checklist, opt => opt.MapFrom(src =>
                src.Checklist != null && src.Checklist.Any()
                    ? JsonConvert.SerializeObject(src.Checklist)
                    : null
            ));
    }
}