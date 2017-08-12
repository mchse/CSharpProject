//developed by M.Chasse
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class CustomersController : Controller
    {
        //index goes to form page
        public ActionResult Index()
        {
            return RedirectToAction("CustomerForm");
        }

        //load /Customers/CustomerForm
        public ActionResult CustomerForm()
        {
            return View();
        }

        public ActionResult Authenticate()
        {
            var username = Request["Username"];
            var password = Request["Password"];
            var user = CustomerDAO.GetCustomerbyUsername(username);

            if (user != null)
            {
                if (user.Password == password)
                {
                    return Content("SUCCESS: user authenticated!");

                    //set cookies
/*                    var cookie = new HttpCookie("custId", customer.Id.ToString());
                    Response.AppendCookie(cookie);

                    var cookieName = new HttpCookie("Username", customer.Username);
                    Response.AppendCookie(cookieName);

                    return RedirectToAction("CustomerLibrary");*/
                }
                return Content("incorrect password");
                //insert message to user to retry password
                //return RedirectToAction("CustomerForm");
            }
            return Content("username not found");
            //insert message to user to retry username
            //return RedirectToAction("CustomerForm");
        }


        /*        public ActionResult Save(Customer customer)
                {
                    //Validation: based on annotations rules [] put in model 
                    if (!ModelState.IsValid)
                    {
                        return View("CustomerForm", customer);
                    }

                    var custId = int.Parse(Request["Id"]);

                    //took hidden field cust id from form and passed to customer.Id
                    customer.Id = custId;

                    if (customer.Id == 0) //if id is 0 doesn't exist in db
                    {
                        //create customer on db
                        //CustomerDAO.Create(customer);

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //update customer on db
                        //CustomerDAO.Update(customer);

                        return RedirectToAction("Index");
                    }

                }*/
    }
}