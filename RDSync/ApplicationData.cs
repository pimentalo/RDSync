using System;
using System.IO;
using System.Xml.Serialization;


namespace RDSync
{
    /// <summary>
    /// Class to manage storing of application data
    /// </summary>
    public class ApplicationData
    {
        public static string DataFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                            "RDSync");


        /// <summary>
        ///  Fullpath to config file.
        /// </summary>
        public static string DataFile = Path.Combine(
                                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                            "RDSync",
                                            "config.xml");

        /// <summary>
        /// Load config from XML
        /// </summary>
        /// <returns></returns>
        public static MainWindowDataContext? LoadData()
        {
            var serializer = new XmlSerializer(typeof(MainWindowDataContext));
            using (var f = System.IO.File.OpenText(DataFile))
            { 
                return (MainWindowDataContext) serializer.Deserialize(f)!;
            }
        }

        /// <summary>
        /// Save config to XML
        /// </summary>
        /// <param name="data"></param>
        public static void SaveData(MainWindowDataContext data)
        {
            var serializer = new XmlSerializer(typeof(MainWindowDataContext));
            if (!Directory.Exists(DataFilePath))
            {
                Directory.CreateDirectory(DataFilePath);
            }
            using (var f = System.IO.File.OpenWrite(DataFile))
            {
                serializer.Serialize(f, data);
            }
        }
    }
}
