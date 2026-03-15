using FiscalFlow.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.API.Data
{
    public class AppDbContext : DbContext
    {
        // Costruttore che accetta le opzioni (FONDAMENTALE!)
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSet per i documenti fiscali (la tabella nel database)
        public DbSet<FiscalDocument> FiscalDocuments { get; set; }

        // Configurazione del modello (qui definiamo come mappare le classi sul database)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurazione per FiscalDocument
            modelBuilder.Entity<FiscalDocument>(entity =>
            {
                // Chiave primaria
                entity.HasKey(e => e.Id);

                // Configurazione proprietà
                entity.Property(e => e.DocumentNumber)
                    .IsRequired()           // NOT NULL
                    .HasMaxLength(50);       // Lunghezza massima 50

                entity.Property(e => e.Amount)
                    .HasPrecision(18, 2);    // 18 cifre totali, 2 decimali

                entity.Property(e => e.VatRate)
                    .HasPrecision(18, 2);    // 18 cifre totali, 2 decimali

                entity.Property(e => e.IssueDate)
                    .IsRequired();

                // Configura l'enum come stringa nel database
                entity.Property(e => e.Status)
                    .HasConversion<string>()  // Salva l'enum come stringa
                    .IsRequired();

                // Indice univoco su DocumentNumber
                entity.HasIndex(e => e.DocumentNumber)
                    .IsUnique();              // Non possono esserci due documenti con lo stesso numero

                // Campi opzionali
                entity.Property(e => e.XmlPayload)
                    .HasColumnType("nvarchar(max)");  // Per XML lunghi

                entity.Property(e => e.PdfContent)
                    .HasColumnType("varbinary(max)"); // Per file PDF
            });
        }
    }
}