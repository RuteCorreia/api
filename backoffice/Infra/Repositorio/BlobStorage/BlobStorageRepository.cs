using Azure.Storage.Blobs;
using Domain.Interfaces.BlobStorage;
using Microsoft.Extensions.Configuration;

namespace Infra.Repositorio.BlobStorage
{
    public class BlobStorageRepository : IBlobStorageRepository
    {
        private readonly BlobContainerClient _containerClient;

        public BlobStorageRepository(IConfiguration configuration)
        {
            var sasUri = configuration["BlobStorage:SasUri"];

            if (!Uri.TryCreate(sasUri, UriKind.Absolute, out var uri))
            {
                throw new ArgumentException("A URI SAS do Blob Storage é inválida.");
            }

            _containerClient = new BlobContainerClient(uri);
        }

        public async Task<string> SavePdfAsync(Stream pdfStream, string fileName)
        {
            if (pdfStream == null) throw new ArgumentNullException(nameof(pdfStream), "O stream de PDF não pode ser nulo.");

            var blobClient = _containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(pdfStream, overwrite: true);

            return fileName;
        }

        public async Task<Stream> GetPdfAsync(string fileName)
        {
            var blobClient = _containerClient.GetBlobClient(fileName);

            if (await blobClient.ExistsAsync())
            {
                var downloadInfo = await blobClient.DownloadAsync();
                return downloadInfo.Value.Content;
            }
            else
            {
                throw new FileNotFoundException($"O arquivo '{fileName}' não foi encontrado no Blob Storage.");
            }
        }
    }
}
