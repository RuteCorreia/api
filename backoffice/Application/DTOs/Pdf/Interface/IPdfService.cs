namespace Application.DTOs.Pdf.Interface
{
    public interface IPdfService
    {
        Task<byte[]> AdicionarMarcaDaguaCanceladoAsync(byte[] pdfBytes);
    }
}
