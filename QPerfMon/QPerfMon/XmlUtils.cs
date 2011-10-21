using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace QPerfMon
{
    public static class XmlUtils
    {
        public static XmlElement CreateElementWithText(this XmlElement parent, string elementName, string text)
        {
            XmlElement newElement = parent.OwnerDocument.CreateElement(elementName);
            newElement.InnerText = text;
            return newElement;
        }
    }
}
