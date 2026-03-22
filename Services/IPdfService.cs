using FiscalFlow.API.Models;

namespace FiscalFlow.API.Services
{
    public interface IPdfService
    {
        byte[] GeneratePdf(FiscalDocument document);
    }
}
