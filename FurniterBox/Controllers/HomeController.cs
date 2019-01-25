using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FurniterBox.Models;

namespace FurniterBox.Controllers
{
    public class HomeController : Controller
    {
        public List<Furniture> Furniturelist = Furniture.GetData();
        public Users userdata;

        public ActionResult Index()
        {
            if (Session["UserId"] is int)
            {
                userdata = Users.GetUserData((int)Session["UserId"]);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            View VM = Models.View.view(Furniturelist, userdata);

            return View(VM);
        }

        public ActionResult Lend(int Id)
        {
            foreach (Models.Furniture Furniture in Furniturelist)
            {
                if (Furniture.Id == Id)
                {
                    Furniture.Count--;
                    Furniture.Count++;
                    Models.Furniture.SaveData(Furniturelist);
                    userdata = Users.GetUserData((int)Session["UserId"]);
                    if (userdata.CartList == null)
                    {
                        userdata.CartList = new List<Models.Users.Cart>();
                    }
                    userdata.CartList.Add(new Models.Users.Cart { id = Furniture.Id, Price = Furniture.Price });
                    Models.Users.SaveUserData(userdata);
                }
            }
            //userdata = Users.GetUserData((int)Session["UserId"]);
            View VM = Models.View.view(Furniturelist, userdata);
            return View("Index", VM);
        }

        public ActionResult Return(int Id)
        {
            foreach (Models.Furniture Furniture in Furniturelist)
            {
                if (Furniture.Id == Id)
                {
                    Furniture.Count++;
                    Models.Furniture.SaveData(Furniturelist);
                    userdata = Users.GetUserData((int)Session["UserId"]);
                    var itemsToRemove = userdata.CartList.FirstOrDefault(r => r.id == Id);
                    if (itemsToRemove != null)
                    {
                        userdata.CartList.Remove(itemsToRemove);
                        Models.Users.SaveUserData(userdata);
                    }
                }
            }


            View VM = Models.View.view(Furniturelist, userdata);

            return View("Index", VM);
        }

        public ActionResult Buy()
        {
            Users userdata = Users.GetUserData((int)Session["UserId"]);
            userdata.CartList.Clear();
            Users.SaveUserData(userdata);

            return RedirectToAction("Index", "Home");
        }
    }
}