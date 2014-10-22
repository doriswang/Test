using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Test.Entities.Entity.Songs;
using Test.Framework.Extensions;

namespace Test.Data.File.Xml
{
    public class XmlDatabase
    {
        public XmlDatabase(string xmlAlbumFile, string xmlSongFile)
        {
            this.xmlAlbumFile = xmlAlbumFile;
            this.xmlSongFile = xmlSongFile;
        }
        public bool Insert<T>(List<T> list)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

            string fileName = string.Empty;

            fileName = GetXmlFilename<T>(fileName);

            if (fileName.IsNullOrEmpty())
                return false;

            using (TextWriter textWriter = new StreamWriter(fileName))
            {
                try
                {
                    serializer.Serialize(textWriter, list);
                    return true;
                }
                catch (Exception)
                {
                }
            }
            return false;
        }

        private string GetXmlFilename<T>(string fileName)
        {
            if (typeof(T) == typeof(Album))
                fileName = xmlAlbumFile;
            else if (typeof(T) == typeof(Song))
                fileName = xmlSongFile;
            return fileName;
        }

        public List<T> Select<T>()
        {
            List<T> result = new List<T>();

            string fileName = string.Empty;

            fileName = GetXmlFilename<T>(fileName);

            if (fileName.IsNullOrEmpty())
                return null;

            XmlSerializer deserializer = new XmlSerializer(typeof(List<T>));
            using (TextReader textReader = new StreamReader(fileName))
            {
                try
                {
                    result = (List<T>)deserializer.Deserialize(textReader);
                }
                catch (Exception)
                {
                }
            }
            return result;
        }

        public string xmlSongFile { get; set; }

        public string xmlAlbumFile { get; set; }
    }
}
