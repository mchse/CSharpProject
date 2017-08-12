//developed by M.Labaj
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Models;

namespace Project.Models
{
    public class CustomerDAO
    {

        public static Customer GetCustomerbyUsername(string username)
        {
            var db = MyDB.GetInstance();
            var sql =
                string.Format("Select * from Customer where Username = '{0}'", username);
            var result = db.ExecuteSelectSql(sql);
            if (result.HasRows)
            {
                result.Read();
                return new Customer
                {
                    Id = (int)result["CustID"], 
                    Username = result["Username"].ToString(),
                    Password = result["Password"].ToString(),
                    Email = result["Email"].ToString(),
                    IsSubscriber = (bool)result["IsSubscriber"]
                };
            }
            return null;
        }

        public static void Create(Customer customer)
        {
            //Not sure if you want password created with customer form. Can easily remove it.
            var db = MyDB.GetInstance();
            var sql =
                string.Format("INSERT INTO Customer VALUES ('{0}', '{1}', '{2}', {3})", customer.Username, customer.Password, customer.Email, customer.IsSubscriber? 1 : 0);
            db.ExecuteSql(sql);
        }

        public static void Update(Customer customer)
        {
            var sql = string.Format("Update Customer SET Username = '{0}'" +
                                    ", Password = '{1}', Email = '{2}', IsSubscriber = {3}, Where CustId = {4}",
                customer.Username, customer.Password, customer.Email, customer.IsSubscriber? 1 : 0, customer.Id);
            MyDB.GetInstance().ExecuteSql(sql);

        }
    }
}