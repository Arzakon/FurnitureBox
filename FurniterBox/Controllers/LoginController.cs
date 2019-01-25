using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FurniterBox.Models;

namespace FurniterBox.Controllers
{
    public class LoginController : Controller
    {
        // Logga in delen, där Email och Lösenord ska skapas
        public ActionResult Index()
        {

            if (HttpContext.Request.RequestType == "POST")
            {
                var Email = Request.Form["email"];
                var Password = Request.Form["password"];

                var CurrentUser = Users.GetUserData(Email);
                if (CurrentUser != null && CurrentUser.Password == Password)
                {
                    Session["UserId"] = CurrentUser.Id;
                    return RedirectToAction("Index", "Home");

                }
            }
            return View();
        }
    }
}