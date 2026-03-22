using FiscalFlow.API.Models;
using FiscalFlow.API.Repositories;

namespace FiscalFlow.API.Services
{
    public class DocumentService : IDocumentService
    {
        #region Fields

        private readonly IDocumentRepository _repository;
        private readonly IXmlGeneratorService _xmlGenerator;
        private readonly IPdfService _pdfService;
        #endregion


        #region Ctor
        public DocumentService( IDocumentRepository repository,
                                IXmlGeneratorService xmlGenerator,
                                IPdfService pdfService )
        {
            _repository = repository;
            _xmlGenerator = xmlGenerator;
            _pdfService = pdfService;
        }

        #endregion

        #region Methods

        public async Task<FiscalDocument> CreateDocumentAsync(FiscalDocument document)
        {
            document.Status = DocumentStatus.Draft;
            await _repository.AddAsync(document);
            return document;
        }

        public async Task<IEnumerable<FiscalDocument>> GetAllDocumentsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<FiscalDocument?> GetDocumentByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }


        public async Task<FiscalDocument> GenerateDocumentAsync(int id)
        {
            var document = await _repository.GetByIdAsync(id);
            if (document == null)
                throw new KeyNotFoundException($"Documento con ID {id} non trovato");

            // Genera XML
            var xml = _xmlGenerator.GenerateXml(document);
            document.XmlPayload = xml;

            // Genera PDF
            var pdf = _pdfService.GeneratePdf(document);
            document.PdfContent = pdf;

            document.Status = DocumentStatus.Generated;
            await _repository.UpdateAsync(document);

            return document;
        }

        public async Task<string?> GetXmlContentAsync(int id)
        {
            var document = await _repository.GetByIdAsync(id);
            return document?.XmlPayload;
        }

        public async Task<byte[]?> GetPdfContentAsync(int id)
        {
            var document = await _repository.GetByIdAsync(id);
            return document?.PdfContent;
        }

        public async Task DeleteDocumentAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
        #endregion
    }
}
