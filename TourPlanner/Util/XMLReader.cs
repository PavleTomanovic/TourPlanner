using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TourPlanner.Util
{
    public class XMLReader
    {
        private XmlDocument doc;

        public XMLReader(string xmlPath)
        {
            this.doc = new XmlDocument();
            this.doc.Load(xmlPath);
        }
        public string getSingleNodeString(string element)
        {
            XmlNode node = this.doc.SelectSingleNode(element);

            if (node == null)
            {
                throw new Exception("XML Fehler beim Einlesen von " + element);
            }

            if (node.InnerXml == null)
            {
                throw new Exception("XML Fehler beim Einlesen von " + element + "\n Wert darf nicht null sein.");
            }

            return node.InnerText;
        }
    }
}
