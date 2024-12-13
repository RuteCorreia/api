namespace Domain.Interfaces.BlobStorage
{
    public interface IBlobStorageRepository
    {
        Task<string> SavePdfAsync(Stream pdfStream, string fileName);
        Task<Stream> GetPdfAsync(string fileName);
    }
}
