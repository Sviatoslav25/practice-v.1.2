using System;
using System.Data.SQLite;
using System.Globalization;
using System.Xml;
using plc;
using Reader;
using Write;
namespace practice
{ 
    class Program
    {
        static void Main(string[] args)
        {
            try
            {       
                XmlReader city = XmlReader.Create("C:\\Users\\User\\source\\repos\\practice\\practice\\urk_town.osm");
                OSMReader read=new OSMReader();
                read.read(city);
            }
            catch
            {
                Console.WriteLine("Помилка");
                Console.ReadLine();
            }  
        }
    }
}
