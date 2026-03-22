using FiscalFlow.API.Models;

namespace FiscalFlow.API.Services
{
    public interface IXmlGeneratorService
    {
        string GenerateXml(FiscalDocument document);
    }
}
