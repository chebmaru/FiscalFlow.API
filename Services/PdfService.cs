using FiscalFlow.API.Models;
using System.Text;

namespace FiscalFlow.API.Services
{
    public class PdfService : IPdfService
    {
        public byte[] GeneratePdf(FiscalDocument document)
        {
            // STUB: versione semplificata che restituisce byte array fittizi
            // In produzione si pò usare iText7 o altra libreria PDF
            string pdfContent = $@"
                PDF GENERATO PER DOCUMENTO FISCALE
                ===================================
                Numero: {document.DocumentNumber}
                Data: {document.IssueDate:dd/MM/yyyy}
                Importo: {document.Amount:C}
                IVA: {document.VatRate}%
                Stato: {document.Status}
            ";

            return Encoding.UTF8.GetBytes(pdfContent);
        }
    }
}
