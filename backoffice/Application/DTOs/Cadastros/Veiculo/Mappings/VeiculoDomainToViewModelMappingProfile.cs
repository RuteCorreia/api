using Application.DTOs.Cadastros.Veiculo.ViewModel;
using AutoMapper;
using Newtonsoft.Json;

namespace Application.DTOs.Cadastros.Veiculo.Mappings;

public class VeiculoDomainToViewModelMappingProfile : Profile
{
    public VeiculoDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Veiculo.Veiculo, VeiculoViewModel>()
            .ForMember(dest => dest.Checklist, opt => opt.MapFrom(src =>
                string.IsNullOrEmpty(src.Checklist)
                    ? Enumerable.Empty<string>()
                    : JsonConvert.DeserializeObject<IEnumerable<string>>(src.Checklist)
            ));
    }
}
