using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Helpers;
using System.Diagnostics;
using WebApplication6.Models;
using System.Data;
using System.Web.Script.Serialization;

namespace WebApplication6.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ViewModel _viewModel;
        private readonly Users _users;

        public HomeController()
        {
            _users = new Users();
            _viewModel = new ViewModel();
        }

        public ActionResult Index()
        {
            var users = _users.GetUsers();
            List<Users> userList = new List<Users>();

            foreach (DataRow row in users.Rows)
            {
                Users user = new Users
                {
                    UserId = Convert.ToInt32(row["UserId"]),
                    Email = row["Email"].ToString(),
                    Password = row["Password"].ToString()
                };

                userList.Add(user);
            }

            var model = new ViewModel
            {
                Users = userList
            };

            return View(model);
        }

 
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}