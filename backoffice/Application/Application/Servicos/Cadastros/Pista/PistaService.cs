using Application.DTOs.Cadastros.Pistas.Interface;
using Application.DTOs.Cadastros.Pistas.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Pista;
using Helpers;
using System.Text.RegularExpressions;

namespace Application.Application.Servicos.Cadastros.Pista;

public class PistaService : IPistaService
{
    private readonly IPistaRepository _pistaRepository;
    private readonly IMapper _mapper;

    public PistaService(IMapper mapper, IPistaRepository pistaRepository)
    {
        _pistaRepository = pistaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PistaViewModel>> GetAllAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var list = await _pistaRepository.GetAllAsync(idEmpresaInt);
        return _mapper.Map<IEnumerable<PistaViewModel>>(list);
    }

    public async Task<IEnumerable<PistaAppViewModel>> GetAllAppAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var list = await _pistaRepository.GetAllAsync(idEmpresaInt);
        var viewModelList = list.Select(pista => new PistaAppViewModel
        {
            Id = pista.Id,
            Nome = pista.Nome,
            LAT = ConvertDMSStringToDecimal(pista.LAT),
            LONG = ConvertDMSStringToDecimal(pista.LONG),
            IdEmpresa = pista.IdEmpresa
        });
        return viewModelList;
    }

    private double ConvertDMSStringToDecimal(string dms)
    {
        // Exemplo: "49° 34' 45.30" W"
        var parts = dms.Split(new[] { '°', '\'', '"' }, StringSplitOptions.RemoveEmptyEntries);

        int graus = int.Parse(parts[0].Trim());
        int minutos = int.Parse(parts[1].Trim());
        double segundos = double.Parse(parts[2].Trim());
        char direcao = parts[3].Trim()[0]; // Pega a direção (W ou S)

        return ConvertDMSToDecimal(graus, minutos, segundos, direcao);
    }

    private double ConvertDMSToDecimal(int graus, int minutos, double segundos, char direcao)
    {
        double decimalDegrees = graus + (minutos / 60.0) + (segundos / 3600.0);

        if (direcao == 'W' || direcao == 'S')
        {
            decimalDegrees *= -1;
        }

        return decimalDegrees;
    }

    public async Task<PistaViewModel> GetByIdAsync(int id)
    {
        var obj = await _pistaRepository.GetByIdAsync(id);
        return _mapper.Map<PistaViewModel>(obj);
    }

    public async Task<IEnumerable<PistaViewModel>> GetByNameAsync(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("O nome não pode ser nulo ou vazio.", nameof(name));
        }
        var pistas = await _pistaRepository.GetByNameAsync(name);

        return pistas.Select(p => _mapper.Map<PistaViewModel>(p));
    }

    public async Task<int> AddAsync(PistaViewModel obj)
    {
        var mapPista = _mapper.Map<Domain.Entidades.Cadastros.Pistas.Pista>(obj);
        return await _pistaRepository.AddAsync(mapPista);
    }

    public async Task UpdateAsync(PistaViewModel obj)
    {
        var mapPista = _mapper.Map<Domain.Entidades.Cadastros.Pistas.Pista>(obj);
        await _pistaRepository.UpdateAsync(mapPista);
    }

    public async Task DeleteAsync(int id)
    {
        await _pistaRepository.DeleteAsync(id);
    }
}
