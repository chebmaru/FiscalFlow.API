using FiscalFlow.API.Models;

namespace FiscalFlow.API.Repositories
{
    public interface IDocumentRepository
    {
        Task<IEnumerable<FiscalDocument>> GetAllAsync();
        Task<FiscalDocument?> GetByIdAsync(int id);
        Task<FiscalDocument?> GetByDocumentNumberAsync(string documentNumber);
        Task AddAsync(FiscalDocument document);
        Task UpdateAsync(FiscalDocument document);
        Task DeleteAsync(int id);
    }
}
