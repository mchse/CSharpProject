// Developed by M.Chasse
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class CustSongViewModel
    {
        public Customer CurrentCustomer { get; set; }
        public List<Song> Songs { get; set; }
    }
}