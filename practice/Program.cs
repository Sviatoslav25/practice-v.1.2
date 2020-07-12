using System;
using System.Data.SQLite;
using System.Globalization;
using System.Xml;
namespace practice
{
   enum place {town, village, hamlet, isolated_dwelling, farm, allotments };
    class DBManager
    {
        public void write(string name, string name_en, string name_uk, string name_ru, place pl, int population, string lat, string lon, SQLiteCommand db)
        {
            string temp= "insert into ukr_town(name, name_en, name_uk, name_ru, place, population, lat, lon) values(\""+name+ "\", \"" + name_en+ "\", \"" + name_uk+ "\", \"" + name_ru+ "\", " + (int)pl+", "+population+", "+lat+", "+lon+");";
            db.CommandText = temp;
            db.ExecuteNonQuery();
        }
    };
    class OSMReader
    {
        public string name;
        public string name_en;
        public string name_uk;
        public string name_ru;
        public place pl;
        public int population;
        string lat;
        string lon;
        int k= 0;
     public void read(XmlReader re,SQLiteCommand te)
        {
            while (re.Read())
            {
                if (k!=0&&re.GetAttribute("id")!=null) {
                    DBManager temp = new DBManager();
                    temp.write(name, name_en, name_uk, name_ru, pl, population, lat, lon, te);
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
                if (re.GetAttribute("k")== "name:en") {
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
                if(re.GetAttribute("k")== "population")
                {
                    population = int.Parse(re.GetAttribute("v"));
                }
               if (re.GetAttribute("lat")!=null) 
               {
                    lat= re.GetAttribute("lat");
               }
                if (re.GetAttribute("lon") != null)
                {
                    lon = re.GetAttribute("lon");
                }
            }
     }
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {       
                XmlReader city = XmlReader.Create("C:\\Users\\User\\source\\repos\\practice\\practice\\urk_town.osm");
                if (!System.IO.File.Exists("ukr_town_db")) SQLiteConnection.CreateFile("ukr_town_db.db");
                SQLiteConnection db_connect = new SQLiteConnection("Data Source=ukr_town_db.db");
                db_connect.Open();
                string sql = "CREATE TABLE ukr_town(id integer primary key,name TEXT, name_en TEXT, name_uk TEXT, name_ru TEXT, place INT, population INT, lat REAL, lon REAL)";
                SQLiteCommand ukr_db = new SQLiteCommand(db_connect);
                ukr_db.CommandText = sql;
                ukr_db.ExecuteNonQuery();
                OSMReader read=new OSMReader();
                read.read(city, ukr_db);
                Console.ReadLine();
                db_connect.Close();
            }
            catch
            {
                Console.WriteLine("Помилка");
                Console.ReadLine();
            }  
        }
    }
}
