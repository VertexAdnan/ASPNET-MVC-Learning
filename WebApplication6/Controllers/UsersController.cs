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
    public class UsersController : BaseController
    {
        private readonly Users _users;
        private readonly ViewModel _viewModel;

        public UsersController()
        {
            _users = new Users();
            _viewModel = new ViewModel();
        }

        public ActionResult Index()
        {
            List<Users> userList = new List<Users>();

            var Users = _users.GetUsers();

            foreach( DataRow row in Users.Rows)
            {
                Users user = new Users
                {
                    UserId = Convert.ToInt32(row["UserId"]),
                    Email = row["Email"].ToString(),
                    Password = row["Password"].ToString()
                };

                userList.Add(user);
            }

            var model = new ViewModel()
            {
                Users = userList
            };

            return View(model);
        }


        [HttpGet]
        public ActionResult RemoveUser(int id)
        {
            int RemoveUser = _users.RemoveUser(id);

            if(RemoveUser == 0)
            {
                ViewBag.error = true;
                ViewBag.message = "Error";
            } else
            {
                ViewBag.error = false;
                ViewBag.message = "Successfully removed";
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult HandleAuth(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["error"] = true;
                TempData["message"] = "Email ve şifre gereklidir.";
                return View("Index");
            }

            var Auth = _users.Auth(email, password);

            if(Auth.Rows.Count > 0)
            {
                Session["UserId"] = Auth.Rows[0]["UserId"];
                Session["Email"] = Auth.Rows[0]["Email"].ToString();

                TempData["error"] = false;
                TempData["message"] = "Başarıyla giriş yapıldı!";
                return RedirectToAction("Index", "Home");
            } else
            {
                TempData["error"] = true;
                TempData["message"] = "Giriş başarısız!!";

                return View("Index");
            }
        }

        public ActionResult AddUser(int UserId = 0)
        {
            if (UserId > 0)
            {
                ViewBag.user = _users.GetUser(UserId);
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddUserHandle(string email, string password, int UserId = 0)
        {
            int Action = 0;

            ViewBag.Error = false;
            ViewBag.Message = "User Created/Updated Successfully";

            if (email.Length <= 5)
            {
                ViewBag.error = true;
                ViewBag.Message = "Email must be higher then 5";
            }

            if (password.Length <= 5)
            {
                ViewBag.error = true;
                ViewBag.Message = "Password must be higher then 5";
            }

            if (ViewBag.error)
            {
                return View("AddUser");
            }


            if (UserId > 0)
            {
                Action = _users.UpdateUser(UserId, email, password);
            }
            else
            {
                Action = _users.AddUser(email, password);
            }

            if (Action == 0)
            {
                ViewBag.Error = true;
                ViewBag.Message = "Something failed!";
            }

            return View("AddUser");
        }
       

        [HttpPost]
        public ActionResult RemoveUserHandle(int UserId)
        {
            var AddUser = _users.RemoveUser(UserId);
            return View("AddUser");
        }

        public ActionResult GetUser(int id)
        {
            var GetUser = _users.GetUser(id).Rows;

            if (GetUser.Count > 0)
            {
                ViewBag.User = GetUser[0];
            }
            else
            {
                return HttpNotFound();
            }

            return View("AddUser");
        }
    }
}