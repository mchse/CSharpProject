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
        public ActionResult Authenticate(Customer user)
        {
            var username = Request["Username"];
            var password = Request["Password"];
            var customer = CustomerDAO.GetCustomerbyUsername(username);

            if (customer != null)
            {
                if (customer.Password == password)
                {
                    //set cookies
                    var cookieId = new HttpCookie("custId", customer.Id.ToString());
                    Response.AppendCookie(cookieId);

                    var cookieName = new HttpCookie("Username", customer.Username);
                    Response.AppendCookie(cookieName);

                    return RedirectToAction("CustomerLibrary");
                }

                Response.Write("<script>alert('Incorrect Password. Please retry.');</script>");

                //bootbox attempt
                /*Response.Write("<script> function test() {" +
                               "event.preventDefault();" +
                               "bootbox.alert('Incorrect Password. Please retry.'," +
                               " function() {});" +
                               "}</script>");*/

                return View("CustomerLogin", user);
            }
            Response.Write("<script>alert('Username not found. Please retry.')</script>");
            return View("CustomerLogin", user);
        }

        public ActionResult CustomerLibrary()
        {
            //get cookie data
            var cookieUsername = Request.Cookies["Username"];
            string Username = "";
            if (cookieUsername != null)
            {
                Username = cookieUsername.Value;
            }

            //get customer object using Username cookie var
            var customer = CustomerDAO.GetCustomerbyUsername(Username);

            //from library table, load all songs with cust id
            var songs = SongDAO.GetCustomerSongs(customer);

            var custSongViewModel = new CustSongViewModel
            {
                CurrentCustomer = customer,
                Songs = songs
            };
            return View("CustomerLibrary", custSongViewModel);
        }

        //load /Customers/Signup
        public ActionResult Signup()
        {
            return View();
        }

        // Register new customer
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
            //future app feature: update customer would go on its own page after authentication
            /*            else
                        {
                            CustomerDAO.Update(customer);
                            Response.Write("<script>alert('Your account was Updated! Please login now.')</script>");
                            return View("CustomerLogin", customer);
                        }*/
            return View("CustomerLogin", customer);
        }

        public ActionResult DeleteSong(int id, int songId)
        {
            SongDAO.DeleteSongFromCustomerLibrary(id, songId);
            return RedirectToAction("CustomerLibrary");
        }

    }
}