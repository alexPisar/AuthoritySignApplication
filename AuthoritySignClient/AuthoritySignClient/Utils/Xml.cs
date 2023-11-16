using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AuthoritySignClient.Utils
{
    public static class Xml
    {
        public static TModel DeserializeString<TModel>(string rawDocument)
        {
            TModel obj;
            XmlSerializer ser = new XmlSerializer(typeof(TModel));
            var stream = new StringReader(rawDocument);

            using (XmlReader reader = XmlReader.Create(stream))
            {
                obj = (TModel)ser.Deserialize(reader);
            }

            return obj;
        }
    }
}
