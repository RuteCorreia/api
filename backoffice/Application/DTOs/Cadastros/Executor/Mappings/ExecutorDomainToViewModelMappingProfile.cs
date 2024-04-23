using Application.DTOs.Cadastros.Executor.ViewModel;
using AutoMapper;
using Domain.Entidades.User;

namespace Application.DTOs.Cadastros.Executor.Mappings;

public class ExecutorDomainToViewModelMappingProfile : Profile
{
    public ExecutorDomainToViewModelMappingProfile()
    {
        CreateMap<Usuario, ExecutorViewModel>();
    }
}
