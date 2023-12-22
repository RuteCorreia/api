using Application.DTOs.Cadastros.Equipamento.ViewModel;
using AutoMapper;

namespace Application.DTOs.Cadastros.Equipamento.Mappings
{
    public class EquipamentoViewModelToDomainMappingProfile : Profile
    {
        public EquipamentoViewModelToDomainMappingProfile()
        {
            CreateMap<EquipamentoViewModel, Domain.Entidades.Cadastros.Equipamento.Equipamento>();
        }
    }
}
