using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Write;
using plc;
using System.Xml;
namespace Reader
{
    public class OSMReader
    {
        string name;
        string name_en;
        string name_uk;
        string name_ru;
        place pl;
        int population;
        string lat;
        string lon;
        int k = 0;
        DBManager temp = new DBManager();
        public void read(XmlReader re)
        {
            temp.create();
            while (re.Read())
            {
                if (k != 0 && re.GetAttribute("id") != null)
                {
                    temp.write(name, name_en, name_uk, name_ru, pl, population, lat, lon);
                    name = " ";
                    name_en = " ";
                    name_uk = " ";
                    name_ru = " ";
                }
                if (re.GetAttribute("k") == "name")
                {
                    name = re.GetAttribute("v");
                    k = 1;
                }
                if (re.GetAttribute("k") == "name:en")
                {
                    name_en = re.GetAttribute("v");
                }
                if (re.GetAttribute("k") == "name:uk")
                {
                    name_uk = re.GetAttribute("v");
                }
                if (re.GetAttribute("k") == "name:ru")
                {
                    name_ru = re.GetAttribute("v");
                }
                if (re.GetAttribute("k") == "place")
                {
                    string temp = re.GetAttribute("v");
                    switch (temp)
                    {
                        case "town":
                            pl = place.town;
                            break;
                        case "village":
                            pl = place.village;
                            break;
                        case "hamlet":
                            pl = place.hamlet;
                            break;
                        case "isolated_dwelling":
                            pl = place.isolated_dwelling;
                            break;
                        case "farm":
                            pl = place.farm;
                            break;
                        case "allotments":
                            pl = place.allotments;
                            break;
                    }
                }
                if (re.GetAttribute("k") == "population")
                {
                    population = int.Parse(re.GetAttribute("v"));
                }
                if (re.GetAttribute("lat") != null)
                {
                    lat = re.GetAttribute("lat");
                }
                if (re.GetAttribute("lon") != null)
                {
                    lon = re.GetAttribute("lon");
                }
            }
        }
    }
}
