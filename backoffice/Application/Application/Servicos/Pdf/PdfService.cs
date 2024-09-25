using Application.DTOs.Pdf.Interface;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Application.Application.Servicos.Pdf
{
    public class PdfService : IPdfService
    {
        public async Task<byte[]> AdicionarMarcaDaguaCanceladoAsync(byte[] pdfBytes)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var reader = new PdfReader(pdfBytes))
                {
                    using (var stamper = new PdfStamper(reader, memoryStream))
                    {
                        int totalPages = reader.NumberOfPages;

                        for (int i = 1; i <= totalPages; i++)
                        {
                            var pdfContent = stamper.GetOverContent(i);
                            var font = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
                            var text = "CANCELADO";
                            var size = 90;
                            var color = BaseColor.RED;
                            var backgroundColor = BaseColor.WHITE;

                            var textWidth = font.GetWidthPoint(text, size);
                            var textHeight = size; // Altura aproximada do texto

                            // Define a posição do texto
                            float x = 298; // Posição X
                            float y = 421; // Posição Y

                            // Desenha o fundo branco
                            pdfContent.SetColorFill(backgroundColor);
                            pdfContent.Rectangle(x - (textWidth / 2) - 10, y - (textHeight / 2) - 10, textWidth + 20, textHeight + 20);
                            pdfContent.Fill();

                            pdfContent.SaveState();
                            pdfContent.BeginText();
                            pdfContent.SetFontAndSize(font, size);
                            pdfContent.SetColorFill(color);
                            pdfContent.ShowTextAligned(PdfContentByte.ALIGN_CENTER, text, x, y, 45);
                            pdfContent.EndText();
                            pdfContent.RestoreState();
                        }
                    }
                }

                return memoryStream.ToArray();
            }
        }
    }
}
