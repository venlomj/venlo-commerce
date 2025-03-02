using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Domain.Models;
using iText.Layout.Borders;

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

                using (var writer = new PdfWriter(fullFilePath))
                using (var pdf = new PdfDocument(writer))
                using (var document = new Document(pdf))
                {
                    var titleFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                    var normalFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                    // Titel
                    document.Add(new Paragraph("FACTUUR").SetFont(titleFont).SetFontSize(18).SetTextAlignment(TextAlignment.CENTER));

                    // Factuurinformatie
                    Table headerTable = new Table(new float[] { 1, 1 }).UseAllAvailableWidth();
                    headerTable.AddCell(new Cell().Add(new Paragraph("Venlo Commerce\nKerkstraat 3\n2430 AB, Laakdal\nBTW-nummer: BE1234567890").SetFont(normalFont)).SetBorder(Border.NO_BORDER));
                    headerTable.AddCell(new Cell().Add(new Paragraph($"Factuurnummer: {invoice.InvoiceNumber}\nDatum: {invoice.DateCreated:dd/MM/yyyy}\nVervaldatum: {invoice.DateCreated.AddDays(30):dd/MM/yyyy}").SetFont(normalFont)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
                    document.Add(headerTable);

                    document.Add(new Paragraph(" "));

                    // Factuurtabel
                    Table table = new Table(new float[] { 10, 35, 15, 15, 15 }).UseAllAvailableWidth();
                    AddCell(table, "SKU", titleFont, new DeviceGray(0.9f));
                    AddCell(table, "Product", titleFont, new DeviceGray(0.9f));
                    AddCell(table, "Aantal", titleFont, new DeviceGray(0.9f));
                    AddCell(table, "Prijs per stuk", titleFont, new DeviceGray(0.9f));
                    AddCell(table, "Totaal", titleFont, new DeviceGray(0.9f));

                    foreach (var item in invoice.LineItems)
                    {
                        AddCell(table, item.SkuCode, normalFont);
                        AddCell(table, item.ProductName, normalFont);
                        AddCell(table, item.Quantity.ToString(), normalFont);
                        AddCell(table, $"€{item.UnitPrice:F2}", normalFont);
                        AddCell(table, $"€{item.TotalPrice:F2}", normalFont);
                    }

                    document.Add(table);
                    document.Add(new Paragraph(" "));

                    // Subtotaal, BTW en totaal
                    decimal subtotal = invoice.TotalAmount;
                    decimal tax = subtotal * 0.21m;
                    decimal grandTotal = subtotal + tax;

                    Table summaryTable = new Table(new float[] { 60, 40 }).SetHorizontalAlignment(HorizontalAlignment.RIGHT);
                    AddSummaryCell(summaryTable, "Subtotaal:", subtotal, titleFont);
                    AddSummaryCell(summaryTable, "BTW (21%):", tax, titleFont);
                    AddSummaryCell(summaryTable, "Totaal te betalen:", grandTotal, titleFont, new DeviceRgb(255, 255, 0));

                    document.Add(summaryTable);
                    document.Add(new Paragraph(" "));

                    // Voetnoot
                    document.Add(new Paragraph("Gelieve het totaalbedrag binnen 30 dagen te voldoen.\nBedankt voor uw bestelling!").SetFont(normalFont).SetTextAlignment(TextAlignment.CENTER));
                }

                Console.WriteLine($"Factuur succesvol gegenereerd: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij genereren factuur: {ex.Message}");
            }
        }

        private static void AddCell(Table table, string text, PdfFont font, Color bgColor = null)
        {
            var cell = new Cell().Add(new Paragraph(text).SetFont(font)).SetTextAlignment(TextAlignment.CENTER);
            if (bgColor != null) cell.SetBackgroundColor(bgColor);
            table.AddCell(cell);
        }

        private static void AddSummaryCell(Table table, string label, decimal amount, PdfFont font, Color bgColor = null)
        {
            var labelCell = new Cell().Add(new Paragraph(label).SetFont(font)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.LEFT);
            var valueCell = new Cell().Add(new Paragraph($"€{amount:F2}").SetFont(font)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT);

            if (bgColor != null)
            {
                labelCell.SetBackgroundColor(bgColor);
                valueCell.SetBackgroundColor(bgColor);
            }

            table.AddCell(labelCell);
            table.AddCell(valueCell);
        }
    }
}