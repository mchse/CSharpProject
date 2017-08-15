using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class SongController : Controller
    {
        // GET: Song
        public ActionResult SongLibrary()
        {
            var songs = SongDAO.GetSongs();
            return View(songs);
        }

        [Route("Song/RegisterSong/{songId}/{custId}")]
        public ActionResult RegisterSong(int songId, int custId)
        {
            SongDAO.AddSongToCustomerLibrary(songId, custId);
            return RedirectToAction("SongLibrary");
        }
    }
}