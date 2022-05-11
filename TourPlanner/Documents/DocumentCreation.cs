﻿using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using TourPlanner.DTO;

namespace TourPlanner.BussinesLayer
{
    public class DocumentCreation : IDocumentCreation
    {
        public void RouteReportCreation(HttpResponseDTO HttpResponseDTO)
        {
            PdfWriter writer = new PdfWriter(BussinessFactory.Instance.DirectoryDTO.ReportPath + DateTime.Now.ToString("fff") + HttpResponseDTO.Route.Id + "_" + HttpResponseDTO.Route.Name + ".pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            Paragraph tableHeader = new Paragraph("Report Table - Route" + HttpResponseDTO.Route.Name)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                    .SetFontSize(18)
                    .SetBold()
                    .SetFontColor(ColorConstants.BLACK);
            document.Add(tableHeader);
            Table table = new Table(UnitValue.CreatePercentArray(8)).UseAllAvailableWidth();
            table.AddHeaderCell(getHeaderCell("Id"));
            table.AddHeaderCell(getHeaderCell("Name"));
            table.AddHeaderCell(getHeaderCell("Description"));
            table.AddHeaderCell(getHeaderCell("From"));
            table.AddHeaderCell(getHeaderCell("To"));
            table.AddHeaderCell(getHeaderCell("Transport"));
            table.AddHeaderCell(getHeaderCell("Distance"));
            table.AddHeaderCell(getHeaderCell("Time"));
            table.SetFontSize(14).SetBackgroundColor(ColorConstants.WHITE);
            table.AddCell(HttpResponseDTO.Route.Id);
            table.AddCell(HttpResponseDTO.Route.Name);
            table.AddCell(HttpResponseDTO.Route.Description);
            table.AddCell(HttpResponseDTO.Route.From);
            table.AddCell(HttpResponseDTO.Route.To);
            table.AddCell(HttpResponseDTO.Route.Transport);
            table.AddCell(HttpResponseDTO.Route.Distance);
            table.AddCell(HttpResponseDTO.Route.FormattedTime);
            document.Add(table);

            Paragraph imageHeader = new Paragraph("Route Image")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                    .SetFontSize(18)
                    .SetBold()
                    .SetFontColor(ColorConstants.BLACK);
            document.Add(imageHeader);
            ImageData imageData = ImageDataFactory.Create(HttpResponseDTO.Route.ImageUrl);
            document.Add(new Image(imageData));

            document.Close();
        }

        public void RouteSummarizeReportCreation(double time, double rating, string distance)
        {
            PdfWriter writer = new PdfWriter(BussinessFactory.Instance.DirectoryDTO.ReportPath + DateTime.Now.ToString("fffff") + "RouteSummarize" + ".pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            Paragraph tableHeader = new Paragraph("Report Table - Summarize Report")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                    .SetFontSize(18)
                    .SetBold()
                    .SetFontColor(ColorConstants.BLACK);
            document.Add(tableHeader);
            Table table = new Table(UnitValue.CreatePercentArray(3)).UseAllAvailableWidth();
            table.AddHeaderCell(getHeaderCell("Average Time"));
            table.AddHeaderCell(getHeaderCell("Average Rating"));
            table.AddHeaderCell(getHeaderCell("Distance"));
            table.SetFontSize(14).SetBackgroundColor(ColorConstants.WHITE);
            table.AddCell(time.ToString());
            table.AddCell(rating.ToString());
            table.AddCell(distance);
            document.Add(table);

            document.Close();
        }

        private static Cell getHeaderCell(String s)
        {
            return new Cell().Add(new Paragraph(s)).SetBold().SetBackgroundColor(ColorConstants.GRAY);
        }
    }
}