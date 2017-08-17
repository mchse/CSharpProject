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
                    Id = (int)result["SongId"],
                    Title = result["Title"].ToString(),
                    Artist = result["Artist"].ToString()
                });
            }

            return songs;
        }

        public static void AddSongToCustomerLibrary(int custId, int songId)
        {
            var db = MyDB.GetInstance();
            //var sql = string.Format("INSERT INTO Library VALUES ({0}, {1})", custId, songId);
            var sql = string.Format("INSERT INTO Library (CustId, SongId) VALUES ({0}, {1})", songId, custId);
            db.ExecuteSql(sql);
        }

        public static List<Song> GetCustomerSongs(Customer customer)
        {
            var custSongs = new List<Song>();
            var db = MyDB.GetInstance();
            var sql = string.Format("SELECT Song.SongId, Song.Title, Song.Artist " +
                                    "FROM Song INNER JOIN Library " +
                                    "ON Library.SongId = Song.SongId " +
                                    "WHERE Library.CustId = {0}", customer.Id);

            var result = db.ExecuteSelectSql(sql);
            while (result.Read())
            {
                custSongs.Add(new Song
                {
                    Id = (int)result["SongId"],
                    Title = result["Title"].ToString(),
                    Artist = result["Artist"].ToString()
                });
            }
            return custSongs;
        }
    }
}