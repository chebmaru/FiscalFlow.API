using FiscalFlow.API.Models;
using FiscalFlow.API.Repositories;
using FiscalFlow.API.Services;
using Moq;
using Xunit;

namespace FiscalFlow.API.Tests
{
    public class DocumentServiceTests
    {
        private readonly Mock<IDocumentRepository> _mockRepo;
        private readonly Mock<IXmlGeneratorService> _mockXmlGen;
        private readonly Mock<IPdfService> _mockPdfService;
        private readonly DocumentService _service;

        public DocumentServiceTests()
        {
            _mockRepo = new Mock<IDocumentRepository>();
            _mockXmlGen = new Mock<IXmlGeneratorService>();
            _mockPdfService = new Mock<IPdfService>();
            _service = new DocumentService(_mockRepo.Object, _mockXmlGen.Object, _mockPdfService.Object);
        }

        [Fact]
        public async Task CreateAsync_SetsStatusToDraft()
        {
            // Arrange
            var document = new FiscalDocument
            {
                DocumentNumber = "TEST-001",
                Amount = 100,
                VatRate = 22,
                IssueDate = DateTime.Now
            };

            // Act
            var result = await _service.CreateDocumentAsync(document);

            // Assert
            Assert.Equal(DocumentStatus.Draft, result.Status);
            _mockRepo.Verify(r => r.AddAsync(document), Times.Once);
        }

        [Fact]
        public async Task GenerateAsync_SetsStatusToGenerated()
        {
            // Arrange
            var document = new FiscalDocument
            {
                Id = 1,
                DocumentNumber = "TEST-001",
                Amount = 100,
                VatRate = 22,
                IssueDate = DateTime.Now,
                Status = DocumentStatus.Draft
            };

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(document);
            _mockXmlGen.Setup(x => x.GenerateXml(document)).Returns("<xml></xml>");
            _mockPdfService.Setup(p => p.GeneratePdf(document)).Returns(new byte[] { 1, 2, 3 });

            // Act
            var result = await _service.GenerateDocumentAsync(1);

            // Assert
            Assert.Equal(DocumentStatus.Generated, result.Status);
            Assert.NotNull(result.XmlPayload);
            Assert.NotNull(result.PdfContent);
            _mockRepo.Verify(r => r.UpdateAsync(document), Times.Once);
        }

        [Fact]
        public async Task GenerateAsync_ThrowsWhenNotFound()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((FiscalDocument?)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GenerateDocumentAsync(999));
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllDocuments()
        {
            // Arrange
            var documents = new List<FiscalDocument>
            {
                new FiscalDocument { Id = 1, DocumentNumber = "001" },
                new FiscalDocument { Id = 2, DocumentNumber = "002" }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(documents);

            // Act
            var result = await _service.GetAllDocumentsAsync();

            // Assert
            Assert.Equal(2, result.Count());
            _mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
