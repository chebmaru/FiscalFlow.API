using FiscalFlow.API.Models;

namespace FiscalFlow.API.Services
{
    public interface IDocumentService
    {
        // RESTITUISCE UNA LISTA DI DOCUMENTI
        Task<IEnumerable<FiscalDocument>> GetAllDocumentsAsync();

        // RESTITUISCE UN DOCUMENTO PER ID
        Task<FiscalDocument?> GetDocumentByIdAsync(int id);

        // CREA UN NUOVO DOCUMENTO
        Task<FiscalDocument> CreateDocumentAsync(FiscalDocument document);

        // GENERA XML E PDF PER UN DOCUMENTO
        Task<FiscalDocument> GenerateDocumentAsync(int id);

        // RECUPERA SOLO IL CONTENUTO XML
        Task<string?> GetXmlContentAsync(int id);

        // RECUPERA SOLO IL CONTENUTO PDF
        Task<byte[]?> GetPdfContentAsync(int id);

        // ELIMINA UN DOCUMENTO
        Task DeleteDocumentAsync(int id);
    }
}
