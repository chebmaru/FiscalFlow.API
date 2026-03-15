namespace FiscalFlow.API.Models
{
    public class FiscalDocument
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal VatRate { get; set; }
        public DateTime IssueDate { get; set; }
        public DocumentStatus Status { get; set; }
        public string? XmlPayload { get; set; }
        public byte[]? PdfContent { get; set; }
    }

    public enum DocumentStatus
    {
        Draft,
        Generated,
        Sent,
        Accepted,
        Rejected
    }
}