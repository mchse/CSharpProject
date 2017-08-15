//developed by M.Labaj
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Models;

namespace Project.Models
{
    public class SongDAO
    {
        public static List<Song> GetSongs()
        {
            var songs = new List<Song>();
            var db = MyDB.GetInstance();
            var sql = "SELECT * FROM Song";

            var result = db.ExecuteSelectSql(sql);
            while (result.Read())
            {
                songs.Add(new Song
                {
                    Id = (int)result["CustId"],
                    Title = result["Title"].ToString(),
                    Artist = result["Artist"].ToString()
                });
            }

            return songs;
        }

        public static void AddSongToCustomerLibrary(int songId, int custId)
        {
            var db = MyDB.GetInstance();
            var sql = string.Format("INSERT INTO Library VALUES ({0}, {1})", custId, songId);
            db.ExecuteSql(sql);
        }
    }
}