using Application.DTOs.Pdf.Interface;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Properties;

namespace Application.Application.Servicos.Pdf
{
    public class PdfService : IPdfService
    {
        public async Task<byte[]> AdicionarMarcaDaguaCanceladoAsync(byte[] pdfBytes)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Leitura do PDF de entrada
                using (var reader = new PdfReader(new MemoryStream(pdfBytes)))
                {
                    using (var pdfWriter = new PdfWriter(memoryStream))
                    {
                        using (var pdfDoc = new PdfDocument(reader, pdfWriter))
                        {
                            int totalPages = pdfDoc.GetNumberOfPages();

                            // Carregar fonte (substituto do BaseFont)
                            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                            for (int i = 1; i <= totalPages; i++)
                            {
                                PdfPage page = pdfDoc.GetPage(i);

                                // Usar o Canvas para escrever texto
                                var canvas = new Canvas(page, page.GetPageSize());

                                var text = "CANCELADO";
                                var size = 90;
                                var color = ColorConstants.RED;

                                // Posição e ângulo da marca d'água
                                float x = page.GetPageSize().GetWidth() / 2;
                                float y = page.GetPageSize().GetHeight() / 2;
                                float angle = 45; // Ângulo de rotação

                                // Adiciona o texto ao PDF
                                canvas
                                    .SetFontColor(color)
                                    .SetFontSize(size)
                                    .SetFont(font)
                                    .ShowTextAligned(text, x, y, TextAlignment.CENTER, VerticalAlignment.MIDDLE, angle);

                                canvas.Close();
                            }
                        }
                    }
                }

                return memoryStream.ToArray();
            }
        }

    }
}
