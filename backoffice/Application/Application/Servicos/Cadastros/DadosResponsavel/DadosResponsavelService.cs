using Application.DTOs.Cadastros.DadosResponsavel.Interface;
using Application.DTOs.Cadastros.DadosResponsavel.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.DadosResponsavel;
using Helpers;

namespace Application.Application.Servicos.Cadastros.DadosResponsavel;

public class DadosResponsavelService : IDadosResponsavelService
{
    private readonly IDadosResponsavelRepository _dadosResponsavelRepository;
    private readonly IMapper _mapper;

    public DadosResponsavelService(
        IMapper mapper,
        IDadosResponsavelRepository dadosResponsavelRepository
    )
    {
        _dadosResponsavelRepository = dadosResponsavelRepository;
        _mapper = mapper;
    }

    public async Task<int> AddAsync(DadosResponsavelViewModel obj, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var entityToCreate = new Domain.Entidades.Cadastros.DadosResponsavel.DadosResponsavel
        {
            Data = obj.Data,
            UF = obj.UF,
            Cidade = obj.Cidade,
            NomeCompleto = obj.NomeCompleto,
            Documento = obj.Documento,
            Telefone = obj.Telefone,
            assinaturaResponsavel =obj.assinaturaResponsavel==null?null:Convert.FromBase64String(obj.assinaturaResponsavel),
            IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt
        };

        if (obj.Id > 0)
        {
            entityToCreate.Id = obj.Id;
            await _dadosResponsavelRepository.UpdateAsync(entityToCreate);
            return entityToCreate.Id;
        }
        else
        {
            var dadosResponsavel = _dadosResponsavelRepository.AddAsync(entityToCreate);
            return dadosResponsavel.Result;
        }   
    }

    public async Task DeleteAsync(int id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        await _dadosResponsavelRepository.DeleteAsync(id, idEmpresaInt);
    }

    public async Task<IEnumerable<DadosResponsavelViewModel>> GetAllAsync(string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var list = await _dadosResponsavelRepository.GetAllAsync(idEmpresaInt);
        var returnList = list.Select(x => new DadosResponsavelViewModel
        {
            Data = x.Data,
            UF = x.UF,
            Cidade = x.Cidade,
            NomeCompleto = x.NomeCompleto,
            Documento = x.Documento,
            Telefone = x.Telefone,
            assinaturaResponsavel = Convert.ToBase64String(x.assinaturaResponsavel)
        });
        return returnList;
    }

    public async Task<DadosResponsavelViewModel?> GetByIdAsync(int id, string? idEmpresa)
    {
        var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
        var obj = await _dadosResponsavelRepository.GetByIdAsync(id, idEmpresaInt);
        var returnObj = obj is not null ? _mapper.Map<DadosResponsavelViewModel>(obj) : null;
        if (returnObj is not null)
            returnObj.assinaturaResponsavel = Convert.ToBase64String(obj?.assinaturaResponsavel ?? []);
       
        return returnObj;
    }

    public async Task UpdateAsync(DadosResponsavelViewModel obj)
    {
        var entityToUpdate = new Domain.Entidades.Cadastros.DadosResponsavel.DadosResponsavel
        {
            Data = obj.Data,
            UF = obj.UF,
            Cidade = obj.Cidade,
            NomeCompleto = obj.NomeCompleto,
            Documento = obj.Documento,
            Telefone = obj.Telefone,
            assinaturaResponsavel = Convert.FromBase64String(obj.assinaturaResponsavel),
        };
        await _dadosResponsavelRepository.UpdateAsync(entityToUpdate);
    }
}
