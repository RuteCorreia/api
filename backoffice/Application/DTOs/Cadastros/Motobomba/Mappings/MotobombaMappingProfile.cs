using Application.DTOs.Cadastros.Motobomba.ViewModel;
using AutoMapper;
using Newtonsoft.Json;

namespace Application.DTOs.Cadastros.Motobomba.Mappings
{
    public class MotobombaMappingProfile : Profile
    {
        public MotobombaMappingProfile()
        {
            CreateMap<MotobombaViewModel, Domain.Entidades.Cadastros.Motobomba.Motobomba>()
                .ForMember(dest => dest.Checklist, opt => opt.MapFrom(src =>
                    src.Checklist != null && src.Checklist.Any()
                        ? JsonConvert.SerializeObject(src.Checklist)
                        : null
                ));
            
            CreateMap<Domain.Entidades.Cadastros.Motobomba.Motobomba, MotobombaViewModel>()
                .ForMember(dest => dest.Checklist, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.Checklist)
                        ? Enumerable.Empty<string>()
                        : JsonConvert.DeserializeObject<IEnumerable<string>>(src.Checklist)
                )); ;
        }
    }
}
