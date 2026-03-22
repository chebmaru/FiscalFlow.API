using FiscalFlow.API.Models;
using System.Xml.Linq;

namespace FiscalFlow.API.Services
{
    public class XmlGeneratorService : IXmlGeneratorService
    {

        public string GenerateXml(FiscalDocument document)
        {
            // Genera XML in formato FatturaElettronica semplificato
            var xml = new XElement("FatturaElettronica",
                new XElement("FatturaElettronicaHeader",
                    new XElement("DatiTrasmissione",
                        new XElement("IdTrasmittente", "IT01234567890"),
                        new XElement("ProgressivoInvio", document.DocumentNumber)
                    )
                ),
                new XElement("FatturaElettronicaBody",
                    new XElement("DatiGenerali",
                        new XElement("Data", document.IssueDate.ToString("yyyy-MM-dd")),
                        new XElement("Numero", document.DocumentNumber)
                    ),
                    new XElement("DatiBeniServizi",
                        new XElement("DettaglioLinee",
                            new XElement("PrezzoTotale", document.Amount),
                            new XElement("AliquotaIVA", document.VatRate)
                        )
                    )
                )
            );

            return xml.ToString();
        }
    }
}
