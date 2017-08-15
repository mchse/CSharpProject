//developed by M.Chasse
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Project.Models;

namespace Project.Controllers
{
    public class CustomersController : Controller
    {
        //index goes to login page
        public ActionResult Index()
        {
            return RedirectToAction("CustomerLogin");
        }

        //load /Customers/CustomerLogin
        public ActionResult CustomerLogin()
        {
            return View();
        }

        //load /Customers/Authenticate
        public ActionResult Authenticate(Customer customer)
        {
            var username = Request["Username"];
            var password = Request["Password"];
            var user = CustomerDAO.GetCustomerbyUsername(username);

            if (user != null)
            {
                if (user.Password == password)
                {
                    //set cookies
                    var cookieId = new HttpCookie("custId", user.Id.ToString());
                    Response.AppendCookie(cookieId);

                    var cookieName = new HttpCookie("Username", user.Username);
                    Response.AppendCookie(cookieName);

                    return View("CustomerLibrary", user);
                }
                Response.Write("<script>alert('Incorrect Password. Please retry.');</script>");
                //bootbox attempt
                /*Response.Write("<script> function test() {" +
                               "event.preventDefault();" +
                               "bootbox.alert('Incorrect Password. Please retry.'," +
                               " function() {});" +
                               "}</script>");*/
                return View("CustomerLogin", customer);
            }
            Response.Write("<script>alert('Username not found. Please retry.')</script>");
            return View("CustomerLogin", customer);
        }

        //load /Customers/Signup
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                //if invalid return to form with built-in error messages
                return View("Signup", customer);
            }

            var custID = int.Parse(Request["custId"]);
            customer.Id = custID;

            //if id is 0 doesn't exist in db
            if (customer.Id == 0) 
            {
                CustomerDAO.Create(customer);
                Response.Write("<script>alert('You are Registered! Please login now.')</script>");
                return View("CustomerLogin", customer);
            }
            //future app feature: update customer would go on its own page after authentication has occurred
/*            else
            {
                CustomerDAO.Update(customer);
                Response.Write("<script>alert('Your account was Updated! Please login now.')</script>");
                return View("CustomerLogin", customer);
            }*/
            return View("CustomerLogin", customer);
        }


        public ActionResult CustomerLibrary(Customer customer)
        {
            //from library table, load all songs with cust id
            var songs = LibraryDAO.GetCustomerSongs(customer);
            return View(songs);
        }

        [Route("Customer/AddSongToLibrary/{songId}/{custId}")]
        public ActionResult AddSongToLibrary(int songId, int custId)
        {
            //in library table, add row with song id and cust id
            LibraryDAO.AddSongToCustomerLibrary(songId, custId);
            return RedirectToAction("CustomerLibrary");
        }

    }
}