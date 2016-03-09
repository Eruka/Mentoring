using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace AdvancedXML
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlReaderSettings settings = new XmlReaderSettings();

            settings.Schemas.Add("http://library.by/catalog", "Data/booksSchema.xsd");
            settings.ValidationEventHandler +=
                delegate(object sender, ValidationEventArgs e)
                {
                    var xmlReader = (XmlReader)sender;
                    var lineInfo = (IXmlLineInfo)sender;
                    Console.WriteLine(string.Format("Element {0} has an exception at line {1} position {2}:",xmlReader.Name, lineInfo.LineNumber, lineInfo.LinePosition));
                    Console.WriteLine(string.Format("   {0}",e.Message));
                };

            settings.ValidationFlags = settings.ValidationFlags | XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationType = ValidationType.Schema;

            XmlReader reader = XmlReader.Create("Data/books.xml", settings);

            while (reader.Read()) ;

            Console.ReadKey();
        }
    }
}
