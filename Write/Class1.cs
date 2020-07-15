using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using plc;
namespace Write
{
    public class DBManager
    {
        SQLiteCommand ukr_db;
        public void create()
        {
            SQLiteConnection.CreateFile("ukr_town_db.db");
            SQLiteConnection db_connect = new SQLiteConnection("Data Source=ukr_town_db.db");
            db_connect.Open();
            string sql = "CREATE TABLE ukr_town(id integer primary key,name TEXT, name_en TEXT, name_uk TEXT, name_ru TEXT, place INT, population INT, lat REAL, lon REAL)";
            ukr_db = new SQLiteCommand(db_connect);
            ukr_db.CommandText = sql;
            ukr_db.ExecuteNonQuery();
        }
        public void write(string name, string name_en, string name_uk, string name_ru, place pl, int population, string lat, string lon)
        {
            string temp = "insert into ukr_town(name, name_en, name_uk, name_ru, place, population, lat, lon) values(\"" + name + "\", \"" + name_en + "\", \"" + name_uk + "\", \"" + name_ru + "\", " + (int)pl + ", " + population + ", " + lat + ", " + lon + ");";
            ukr_db.CommandText = temp;
            ukr_db.ExecuteNonQuery();
        }
    };
}
