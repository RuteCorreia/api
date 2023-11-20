using Application.DTOs.Cadastros.Equipamento.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Equipamento.Mappings;

public class EquipamentoDomainToViewModelMappingProfile : Profile
{
    public EquipamentoDomainToViewModelMappingProfile()
    {
        CreateMap<Domain.Entidades.Cadastros.Equipamento.Equipamento, EquipamentoViewModel>();
    }
}
