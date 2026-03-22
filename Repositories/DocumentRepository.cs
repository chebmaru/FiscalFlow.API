using FiscalFlow.API.Data;
using FiscalFlow.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.API.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AppDbContext _context;

        public DocumentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FiscalDocument>> GetAllAsync()
        {
            return await _context.FiscalDocuments
                .AsNoTracking()  // Ottimizzazione per letture
                .ToListAsync();
        }

        public async Task<FiscalDocument?> GetByIdAsync(int id)
        {
            return await _context.FiscalDocuments.FindAsync(id);
        }

        public async Task<FiscalDocument?> GetByDocumentNumberAsync(string documentNumber)
        {
            return await _context.FiscalDocuments
                .FirstOrDefaultAsync(f => f.DocumentNumber == documentNumber);
        }

        public async Task AddAsync(FiscalDocument document)
        {
            await _context.FiscalDocuments.AddAsync(document);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FiscalDocument document)
        {
            _context.FiscalDocuments.Update(document);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var document = await GetByIdAsync(id);
            if (document != null)
            {
                _context.FiscalDocuments.Remove(document);
                await _context.SaveChangesAsync();
            }
        }
    }
}
