using System;
using System.Net.Sockets;
using System.Xml;
namespace practice
{
    class Program
    {
        static void Main(string[] args)
        {
            string name_e = "null", name = "null";
            int k = 0;
            try
            {
                XmlReader city = XmlReader.Create("C:\\Users\\User\\source\\repos\\practice\\practice\\urk_town.osm");
                while (city.Read())
                {
                    if (city.GetAttribute("k") == "name")
                    {
                        name = city.GetAttribute("v");
                        k = 2;
                    }
                    if (city.GetAttribute("k") == "name:en")
                    {
                        name_e = city.GetAttribute("v");
                        k = 1;
                    }
                    if (city.GetAttribute("k") == "name:prefix" && city.GetAttribute("v") == "місто" && k == 1)
                    {
                        Console.WriteLine(name_e);
                        k = 0;
                    }
                    else if (city.GetAttribute("k") == "name:prefix" && city.GetAttribute("v") == "місто" && k == 2)
                    {
                        Console.WriteLine(name);
                        k = 0;
                    }
                }
                Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("Помилка вiдкриття файлу");
                Console.ReadLine();
            }
        }
    }
}
