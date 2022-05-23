﻿using System.Xml;
using TourPlanner.BussinesLayer;
using TourPlanner.DataAccessLayer;
using TourPlanner.DTO;
using TourPlanner.Util;

namespace TourPlanner.Documents
{
    public class ImportExport
    {
        private XMLReader xmlReader;

        private static ImportExport instance = new ImportExport();
        public static ImportExport Instance
        {
            get
            {
                return instance;
            }
        }

        public HttpResponseDTO ImportFile(string filename)
        {
            xmlReader = new XMLReader(filename);
            HttpDTO httpDTO = BussinessFactory.Instance.HttpDTO;

            httpDTO.From = xmlReader.getSingleNodeString("Route/From");
            httpDTO.To = xmlReader.getSingleNodeString("Route/To");

            HttpResponseDTO httpResponseDTO = HttpRequest.Instance.GetRoutes(httpDTO);

            httpResponseDTO.Route.ImageUrl = HttpRequest.Instance.GetRouteImage(httpDTO);
            httpResponseDTO.Route.Name = xmlReader.getSingleNodeString("Route/Name");
            httpResponseDTO.Route.Comment = xmlReader.getSingleNodeString("Route/Comment");
            httpResponseDTO.Route.From = xmlReader.getSingleNodeString("Route/From");
            httpResponseDTO.Route.To = xmlReader.getSingleNodeString("Route/To");
            httpResponseDTO.Route.Transport = xmlReader.getSingleNodeString("Route/Transport");
            httpResponseDTO.Route.Favorite = "No";

            return httpResponseDTO;
        }
        public void ExportFile(string filename, HttpResponseDTO httpResponseDTO)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t"
            };

            using (XmlWriter x = XmlWriter.Create(filename, xmlWriterSettings))
            {
                x.WriteStartDocument();
                x.WriteStartElement("Export"); // Export

                //ID Node
                if (httpResponseDTO.Route.Id != null)
                {
                    x.WriteStartElement("Id", null);
                    x.WriteString((httpResponseDTO.Route.Id.ToString()));
                    x.WriteEndElement(); //Id
                }
                else
                {
                    x.WriteStartElement("Id", null);
                    x.WriteEndElement(); //Id
                }

                //Name Node
                if (httpResponseDTO.Route.Name != null)
                {
                    x.WriteStartElement("Name", null);
                    x.WriteString((httpResponseDTO.Route.Name.ToString()));
                    x.WriteEndElement(); //Name
                }
                else
                {
                    x.WriteStartElement("Name", null);
                    x.WriteEndElement(); //Name
                }

                //Comment Node
                if (httpResponseDTO.Route.Comment != null)
                {
                    x.WriteStartElement("Comment", null);
                    x.WriteString((httpResponseDTO.Route.Comment.ToString()));
                    x.WriteEndElement(); //Comment
                }
                else
                {
                    x.WriteStartElement("Comment", null);
                    x.WriteEndElement(); //Comment
                }

                //From Node
                if (httpResponseDTO.Route.From != null)
                {
                    x.WriteStartElement("From", null);
                    x.WriteString((httpResponseDTO.Route.From.ToString()));
                    x.WriteEndElement(); //From
                }
                else
                {
                    x.WriteStartElement("From", null);
                    x.WriteEndElement(); //From
                }

                //To Node
                if (httpResponseDTO.Route.To != null)
                {
                    x.WriteStartElement("To", null);
                    x.WriteString((httpResponseDTO.Route.To.ToString()));
                    x.WriteEndElement(); //To
                }
                else
                {
                    x.WriteStartElement("To", null);
                    x.WriteEndElement(); //To
                }

                //Transport Node
                if (httpResponseDTO.Route.Transport != null)
                {
                    x.WriteStartElement("Transport", null);
                    x.WriteString((httpResponseDTO.Route.Transport.ToString()));
                    x.WriteEndElement(); //Transport
                }
                else
                {
                    x.WriteStartElement("Transport", null);
                    x.WriteEndElement(); //Transport
                }

                //Distance Node
                if (httpResponseDTO.Route.Distance != null)
                {
                    x.WriteStartElement("Distance", null);
                    x.WriteString((httpResponseDTO.Route.Distance.ToString()));
                    x.WriteEndElement(); //Distance
                }
                else
                {
                    x.WriteStartElement("Distance", null);
                    x.WriteEndElement(); //Distance
                }

                //Time Node
                if (httpResponseDTO.Route.Time != null)
                {
                    x.WriteStartElement("FormattedTime", null);
                    x.WriteString((httpResponseDTO.Route.Time.ToString()));
                    x.WriteEndElement(); //FormattedTime
                }
                else
                {
                    x.WriteStartElement("FormattedTime", null);
                    x.WriteEndElement(); //FormattedTime
                }

                //Image Node
                if (httpResponseDTO.Route.ImageUrl != null)
                {
                    x.WriteStartElement("ImageUrl", null);
                    x.WriteString((httpResponseDTO.Route.ImageUrl.ToString()));
                    x.WriteEndElement(); //ImageUrl
                }
                else
                {
                    x.WriteStartElement("ImageUrl", null);
                    x.WriteEndElement(); //ImageUrl
                }

                x.WriteEndElement();
            }
        }
    }
}
