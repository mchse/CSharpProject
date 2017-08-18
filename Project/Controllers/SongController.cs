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

        public ActionResult RegisterSong(int id, int custId)
        {
            //Add songs to the current user
            SongDAO.AddSongToCustomerLibrary(id, custId);
            return RedirectToAction("CustomerLibrary", "Customers");
        }
    }
}