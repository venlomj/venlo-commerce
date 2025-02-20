using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Domain.Models;

namespace Domain.Utils
{
    public static class PdfUtility
    {
        public static void GeneratePdf(string filePath, Invoice invoice)
        {
            try
            {
                var documentsPath = Path.Combine(Directory.GetCurrentDirectory(), "Documents");
                if (!Directory.Exists(documentsPath))
                {
                    Directory.CreateDirectory(documentsPath);
                }

                var fullFilePath = Path.Combine(documentsPath, filePath);

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    var document = new Document(PageSize.A4, 50, 50, 25, 25);
                    var writer = PdfWriter.GetInstance(document, stream);

                    document.Open();

                    var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                    var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
                    var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);

                    // 🏢 Bedrijfsinformatie en klantgegevens
                    var headerTable = new PdfPTable(2) { WidthPercentage = 100 };
                    headerTable.SetWidths(new float[] { 50, 50 });

                    var companyInfo = new Paragraph("Venlo Commerce\nKerstraat 3\n2430 AB, Laakdal\nBTW-nummer: BE1234567890", normalFont);
                    var invoiceInfo = new Paragraph($"Factuurnummer: {invoice.InvoiceNumber}\nDatum: {invoice.DateCreated:dd/MM/yyyy}\nVervaldatum: {invoice.DateCreated.AddDays(30):dd/MM/yyyy}", normalFont);

                    var cell1 = new PdfPCell(companyInfo) { Border = Rectangle.NO_BORDER };
                    var cell2 = new PdfPCell(invoiceInfo) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };

                    headerTable.AddCell(cell1);
                    headerTable.AddCell(cell2);
                    document.Add(headerTable);

                    document.Add(new Paragraph("\n"));

                    // 📌 Factuurtitel
                    var title = new Paragraph("FACTUUR", titleFont)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingAfter = 20
                    };
                    document.Add(title);

                    // 📋 Factuurtabel
                    var table = new PdfPTable(5) { WidthPercentage = 100 };
                    table.SetWidths(new float[] { 10, 35, 15, 15, 15 });

                    AddCell(table, "SKU", boldFont, BaseColor.LIGHT_GRAY);
                    AddCell(table, "Product", boldFont, BaseColor.LIGHT_GRAY);
                    AddCell(table, "Aantal", boldFont, BaseColor.LIGHT_GRAY);
                    AddCell(table, "Prijs per stuk", boldFont, BaseColor.LIGHT_GRAY);
                    AddCell(table, "Totaal", boldFont, BaseColor.LIGHT_GRAY);

                    foreach (var item in invoice.LineItems)
                    {
                        AddCell(table, item.SkuCode, normalFont);
                        AddCell(table, item.ProductName, normalFont);
                        AddCell(table, item.Quantity.ToString(), normalFont);
                        AddCell(table, $"€{item.UnitPrice:F2}", normalFont);
                        AddCell(table, $"€{item.TotalPrice:F2}", normalFont);
                    }

                    document.Add(table);
                    document.Add(new Paragraph("\n"));

                    // 🔢 Subtotaal & BTW berekening
                    decimal subtotal = invoice.TotalAmount;
                    decimal tax = subtotal * 0.21m;
                    decimal grandTotal = subtotal + tax;

                    var summaryTable = new PdfPTable(2) { WidthPercentage = 50, HorizontalAlignment = Element.ALIGN_RIGHT };
                    summaryTable.SetWidths(new float[] { 60, 40 });

                    AddSummaryCell(summaryTable, "Subtotaal:", subtotal, boldFont);
                    AddSummaryCell(summaryTable, "BTW (21%):", tax, boldFont);
                    AddSummaryCell(summaryTable, "Totaal te betalen:", grandTotal, boldFont, BaseColor.YELLOW);

                    document.Add(summaryTable);
                    document.Add(new Paragraph("\n"));

                    // 📌 Betaal- en afsluitende boodschap
                    var footer = new Paragraph("Gelieve het totaalbedrag binnen 30 dagen te voldoen.\nBedankt voor uw bestelling!", normalFont)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingBefore = 20
                    };
                    document.Add(footer);

                    document.Close();
                    writer.Close();
                }

                Console.WriteLine($"Factuur succesvol gegenereerd: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij genereren factuur: {ex.Message}");
            }
        }

        private static void AddCell(PdfPTable table, string text, Font font, BaseColor bgColor = null)
        {
            var cell = new PdfPCell(new Phrase(text, font));
            if (bgColor != null) cell.BackgroundColor = bgColor;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);
        }

        private static void AddSummaryCell(PdfPTable table, string label, decimal amount, Font font, BaseColor bgColor = null)
        {
            var labelCell = new PdfPCell(new Phrase(label, font)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            var valueCell = new PdfPCell(new Phrase($"€{amount:F2}", font)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };

            if (bgColor != null)
            {
                labelCell.BackgroundColor = bgColor;
                valueCell.BackgroundColor = bgColor;
            }

            table.AddCell(labelCell);
            table.AddCell(valueCell);
        }
    }
}
