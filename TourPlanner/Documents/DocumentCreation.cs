using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using log4net;
using System;
using TourPlanner.DTO;
using TourPlanner.Util;

namespace TourPlanner.BussinesLayer
{
    public class DocumentCreation
    {
        private static DocumentCreation instance = new DocumentCreation();
        ILog log = LogManager.GetLogger(typeof(App));
        public static DocumentCreation Instance
        {
            get
            {
                return instance;
            }
        }
        public bool RouteReportCreation(HttpResponseDTO HttpResponseDTO)
        {
            try
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
                table.AddHeaderCell(GetHeaderCell("Id"));
                table.AddHeaderCell(GetHeaderCell("Name"));
                table.AddHeaderCell(GetHeaderCell("Comment"));
                table.AddHeaderCell(GetHeaderCell("From"));
                table.AddHeaderCell(GetHeaderCell("To"));
                table.AddHeaderCell(GetHeaderCell("Transport"));
                table.AddHeaderCell(GetHeaderCell("Distance"));
                table.AddHeaderCell(GetHeaderCell("Time"));
                table.SetFontSize(14).SetBackgroundColor(ColorConstants.WHITE);
                table.AddCell(HttpResponseDTO.Route.Id);
                table.AddCell(HttpResponseDTO.Route.Name);
                table.AddCell(HttpResponseDTO.Route.Comment);
                table.AddCell(HttpResponseDTO.Route.From);
                table.AddCell(HttpResponseDTO.Route.To);
                table.AddCell(HttpResponseDTO.Route.Transport);
                table.AddCell(HttpResponseDTO.Route.Distance);
                table.AddCell(HttpResponseDTO.Route.Time);
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

                log.Debug("RouteReportCreation done");

                return true;
            }
            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("RouteReportCreation Error" + e.Message + " - " + e.StackTrace);
                return false;
            }
        }

        public bool RouteSummarizeReportCreation(string time, double rating, string distance, string routeName)
        {
            try
            {
                PdfWriter writer = new PdfWriter(BussinessFactory.Instance.DirectoryDTO.ReportPath + DateTime.Now.ToString("fffff") + "_SummarizeReport_" + routeName + ".pdf");
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                Paragraph tableHeader = new Paragraph("Report Table - Summarize Report of " + routeName)
                        .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                        .SetFontSize(18)
                        .SetBold()
                        .SetFontColor(ColorConstants.BLACK);
                document.Add(tableHeader);
                Table table = new Table(UnitValue.CreatePercentArray(3)).UseAllAvailableWidth();
                table.AddHeaderCell(GetHeaderCell("Average Time"));
                table.AddHeaderCell(GetHeaderCell("Average Rating"));
                table.AddHeaderCell(GetHeaderCell("Distance"));
                table.SetFontSize(14).SetBackgroundColor(ColorConstants.WHITE);
                table.AddCell(time.ToString());
                table.AddCell(rating.ToString());
                table.AddCell(distance);
                document.Add(table);

                document.Close();

                log.Debug("RouteSummarizeReportCreation done");

                return true;
            }

            catch (Exception e)
            {
                LoggerToFile.LogError(e.Message + "\n" + e.StackTrace);
                log.Error("RouteSummarizeReportCreation Error" + e.Message + " - " + e.StackTrace);
                
                return false;
            }
        }

        private static Cell GetHeaderCell(String s)
        {
            return new Cell().Add(new Paragraph(s)).SetBold().SetBackgroundColor(ColorConstants.GRAY);
        }
    }
}
