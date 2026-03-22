using FiscalFlow.API.Models;
using FiscalFlow.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FiscalFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        // GET: api/documents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FiscalDocument>>> GetAll()
        {
            var documents = await _documentService.GetAllDocumentsAsync();
            return Ok(documents);
        }

        // GET: api/documents/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FiscalDocument>> GetById(int id)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);
            if (document == null)
                return NotFound();
            return Ok(document);
        }

        // POST: api/documents
        [HttpPost]
        public async Task<ActionResult<FiscalDocument>> Create(FiscalDocument document)
        {
            var created = await _documentService.CreateDocumentAsync(document);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // POST: api/documents/{id}/generate
        [HttpPost("{id}/generate")]
        public async Task<ActionResult<FiscalDocument>> Generate(int id)
        {
            try
            {
                var document = await _documentService.GenerateDocumentAsync(id);
                return Ok(document);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: api/documents/{id}/xml
        [HttpGet("{id}/xml")]
        public async Task<IActionResult> DownloadXml(int id)
        {
            var xml = await _documentService.GetXmlContentAsync(id);
            if (xml == null)
                return NotFound();

            var bytes = System.Text.Encoding.UTF8.GetBytes(xml);
            return File(bytes, "application/xml", $"documento-{id}.xml");
        }

        // GET: api/documents/{id}/pdf
        [HttpGet("{id}/pdf")]
        public async Task<IActionResult> DownloadPdf(int id)
        {
            var pdf = await _documentService.GetPdfContentAsync(id);
            if (pdf == null)
                return NotFound();

            return File(pdf, "application/pdf", $"documento-{id}.pdf");
        }

        // DELETE: api/documents/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _documentService.DeleteDocumentAsync(id);
            return NoContent();
        }
    }
}
