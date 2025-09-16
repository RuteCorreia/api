using Application.DTOs.Cadastros.Aeronave.Interface;
using Application.DTOs.Cadastros.Aeronave.ViewModel;
using AutoMapper;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Enums;
using Domain.Interfaces.Cadastros.Aeronave;
using Domain.Interfaces.Cadastros.Empresa;
using Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Application.Application.Servicos.Cadastros.Aeronave;

public class AeronaveService : IAeronaveService
{
    private readonly IAeronaveRepository _aeronaveRepository;
    private readonly IEmpresaRepository _empresaRepository;
    private readonly IMapper _mapper;

    public AeronaveService(IMapper mapper, IAeronaveRepository aeronaveRepository, IEmpresaRepository empresaRepository)
    {
        _aeronaveRepository = aeronaveRepository;
        _empresaRepository = empresaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AeronaveViewModel>> GetAllAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var list = await _aeronaveRepository.GetAllAsync(idEmpresaInt);

        // Cria uma lista para armazenar os AeronaveViewModels
        var aeronaveViewModels = new List<AeronaveViewModel>();

        foreach (var item in list)
        {
            // Criação do modelo de visualização sem usar AutoMapper
            var aeronaveViewModel = new AeronaveViewModel
            {
                Id = item.Id,
                Tipo = item.Tipo,
                Fabricante = item.Fabricante,
                Prefixo = item.Prefixo,
                Modelo = item.Modelo,
                SerialNumber = item.SerialNumber,
                // Outros campos que você tem em AeronaveViewModel
            };

            // Deserializa o checklist de JSON para IEnumerable<string>
            if (!string.IsNullOrEmpty(item.Checklist))
            {
                aeronaveViewModel.Checklist = JsonConvert.DeserializeObject<IEnumerable<string>>(item.Checklist);
            }
            else
            {
                aeronaveViewModel.Checklist = Enumerable.Empty<string>(); // Inicializa como uma coleção vazia se estiver vazia
            }

            aeronaveViewModels.Add(aeronaveViewModel);
        }
        return aeronaveViewModels;
    }

    public async Task<AeronaveViewModel> GetByIdAsync(int id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var obj = await _aeronaveRepository.GetByIdAsync(id, idEmpresaInt);

        if (obj == null)
        {
            throw new Exception($"Aeronave com ID {id} não encontrada.");
        }

        var aeronaveViewModel = new AeronaveViewModel
        {
            Id = obj.Id,
            Tipo = obj.Tipo,
            Fabricante = obj.Fabricante,
            Prefixo = obj.Prefixo,
            Modelo = obj.Modelo,
            SerialNumber = obj.SerialNumber,
        };

        if (!string.IsNullOrEmpty(obj.Checklist))
        {
            aeronaveViewModel.Checklist = JsonConvert.DeserializeObject<IEnumerable<string>>(obj.Checklist);
        }
        else
        {
            aeronaveViewModel.Checklist = Enumerable.Empty<string>();
        }

        return aeronaveViewModel;
    }

    public async Task<(bool, string)> AddAsync(AeronaveViewModel obj, string? idEmpresa)
    {
        var resultMsg = new StringBuilder();

        var mapAeronave = _mapper.Map<Domain.Entidades.Cadastros.Aeronave.Aeronave>(obj);
        var idEmpresaAsNumber = ConvertTypes.ConvertStringToInt(idEmpresa);

        if (obj.Checklist != null) 
        {
            var checklistJson = JsonConvert.SerializeObject(obj.Checklist);
            mapAeronave.Checklist = checklistJson;
        }

        var empresa = await _empresaRepository.GetByIdAsync(idEmpresaAsNumber);
        var limit = await _aeronaveRepository.GetAllAsync(idEmpresaAsNumber);
        var limitAeronave = limit.Count(a => a.Tipo == ETipoAeronave.Aeronave);
        if (mapAeronave.Tipo == ETipoAeronave.Aeronave && limitAeronave >= empresa.QtdAeronaves)
            return (false, resultMsg.Append("Você ja cadastrou o limite de aeronaves registrados").ToString());

        var limitDrone = limit.Count(a => a.Tipo == ETipoAeronave.Drone);
        if (mapAeronave.Tipo == ETipoAeronave.Drone && limitDrone >= empresa.QtdDrones)
            return (false, resultMsg.Append("Você ja cadastrou o limite de drones registrados").ToString());

        mapAeronave.IdEmpresa = idEmpresaAsNumber == 0 ? null : idEmpresaAsNumber;
        await _aeronaveRepository.AddAsync(mapAeronave);

        return (true, resultMsg.Append("Aeronave Registrada com Sucesso").ToString());
    }

    public async Task UpdateAsync(AeronaveViewModel obj)
    {
        var mapAeronave = _mapper.Map<Domain.Entidades.Cadastros.Aeronave.Aeronave>(obj);
        if (obj.Checklist != null)
        {
            var checklistJson = JsonConvert.SerializeObject(obj.Checklist);
            mapAeronave.Checklist = checklistJson;
        }
        await _aeronaveRepository.UpdateAsync(mapAeronave);
    }

    public async Task<IEnumerable<AeronaveViewModel>> GetByNameAsync(string name, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        var list = await _aeronaveRepository.GetByNameAsync(name, idEmpresaInt);
        return _mapper.Map<IEnumerable<AeronaveViewModel>>(list);
    }

    public async Task DeleteAsync(int id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
        await _aeronaveRepository.DeleteAsync(id, idEmpresaInt);
    }
}
