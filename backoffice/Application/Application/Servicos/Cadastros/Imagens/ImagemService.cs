using Application.DTOs.Cadastros.Imagens.Interface;

namespace Application.Application.Servicos.Cadastros.Imagens
{
    public class ImagemService : IImagemService
    {
        private readonly string _baseFolderPath;
        public ImagemService()
        {
            _baseFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagens");
        }

        private string GetFolderPath(string subFolder)
        {
            return Path.Combine(_baseFolderPath, subFolder);
        }

        private string GetFilePath(string prefix, string subFolder, int? id)
        {
            return Path.Combine(GetFolderPath(subFolder), $"{prefix}_{id}.json");
        }

        public async Task SaveJsonToFileAsync(string json, string prefix, string subFolder, int? id)
        {
            var folderPath = GetFolderPath(subFolder);

            // Crie a pasta se não existir
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = GetFilePath(prefix,subFolder, id);
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task<string?> GetJsonFromFileAsync(string prefix, string subFolder, int? id)
        {
            var filePath = GetFilePath(prefix, subFolder, id);

            if (File.Exists(filePath))
            {
                return await File.ReadAllTextAsync(filePath);
            }

            return null; // Arquivo não encontrado
        }
    }
}
