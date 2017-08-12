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
        public ActionResult Index()
        {
            songs = SongDAO.GetSongs();
            return View(songs);
        }

        public ActionResult SongDetails(int id)
        {
            return Content("Song: " + id,ToString());
        }
    }
}