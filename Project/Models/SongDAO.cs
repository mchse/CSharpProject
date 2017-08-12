//developed by M.Labaj
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class SongDAO
    {
        public static List<Song> GetSongs(string title)
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

        public static void AddSong(int custId, int songId)
        {
            var db = MyDB.GetInstance();
            var sql = string.Format("INSERT INTO Library VALUES ({0}, {1})", custId, songId);
            db.ExecuteSql(sql);
        }
    }
}