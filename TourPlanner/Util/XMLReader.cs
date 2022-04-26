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

        public string getElement(string element)
        {
            XmlNodeList elementsByTagName = this.doc.DocumentElement.GetElementsByTagName(element);

            if (elementsByTagName.Count != 1)
            {
                return null;
            }

            return elementsByTagName[0].InnerXml;
        }

        public string getAttribute(string element, string attribute)
        {
            XmlNodeList elementsByTagName = this.doc.DocumentElement.GetElementsByTagName(element);

            if (elementsByTagName.Count != 1)
            {
                return null;
            }

            return elementsByTagName[0].Attributes[attribute].Value;
        }

        public XmlNodeList getElements(string element)
        {
            XmlNodeList elementsByTagName = this.doc.DocumentElement.GetElementsByTagName(element);


            if (elementsByTagName.Count < 1)
            {
                return null;
            }

            return elementsByTagName;
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

        public int getSingleNodeInt(string element)
        {
            try
            {
                XmlNode node = this.doc.SelectSingleNode(element);
                return Int32.Parse(node.InnerXml);
            }
            catch (Exception e)
            {
                throw new Exception("XML Fehler beim Einlesen von " + element + "\n" + e.Message);
            }
        }

        
    }
}
