using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace SupportTools
{
    class WriteXMLData
    {
        public static void SaveData (object obj, string filename)
        {
            XmlSerializer xmlSr = new XmlSerializer(obj.GetType());
            TextWriter textWr = new StreamWriter(filename);
            xmlSr.Serialize(textWr, obj);
            textWr.Close();
        }
    }
}
