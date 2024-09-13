namespace Application.DTOs.Cadastros.Imagens.Interface
{
    public interface IImagemService
    {
        Task SaveJsonToFileAsync(string json, string prefix, string subFolder, int? id);
        Task<string?> GetJsonFromFileAsync(string prefix, string subFolder, int? id);
    }
}
