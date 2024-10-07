using Application.DTOs.Cadastros.Executor.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Executor.Mappings
{
    public class ExecutorMappingProfile : Profile
    {
        public ExecutorMappingProfile()
        {
            CreateMap<Domain.Entidades.Cadastros.Executor.Executor, ExecutorViewModel>()
                .ForMember(dest => dest.Assinatura, opt => opt.MapFrom(src => Convert.ToBase64String(src.Assinatura)));
            CreateMap<ExecutorViewModel, Domain.Entidades.Cadastros.Executor.Executor>();
        }
    }
}
