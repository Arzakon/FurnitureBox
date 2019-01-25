using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace FurniterBox.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Cart> CartList { get; set; }

        public static List<Users> UserList = GetUsers();

        public static Users GetUserData(string Email)
        {
            var selected = UserList.Where(x => x.Email == Email).FirstOrDefault();
            return selected;
        }
        public static Users GetUserData(int id)
        {
            Users userdata;
            string filepath = HttpContext.Current.Server.MapPath("~/App_Data/Storage/user" + id + ".json");

            if (System.IO.File.Exists(filepath))
            {
                var settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Objects
                };
                var json = System.IO.File.ReadAllText(filepath);
                userdata = JsonConvert.DeserializeObject<Users>(json, settings);
            }
            else
            {
                userdata = UserList.Where(x => x.Id == id).FirstOrDefault();
            }

            return userdata;
        }
        public static void SaveUserData(Users user)
        {
            string filepath = HttpContext.Current.Server.MapPath("~/App_Data/Storage/user" + user.Id + ".json");
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented
            };
            string json = JsonConvert.SerializeObject(user, settings);
            System.IO.File.WriteAllText(filepath, json);
        }


        public static List<Users> GetUsers()
        {
            List<Users> UserList = new List<Users>();
            UserList.Add(new Users { Id = 1, Email = "micke@primat.se", Password = "hejsan", Name = "Mikael Engström" });
            UserList.Add(new Users { Id = 2, Email = "nisse@abc.se", Password = "hejsan", Name = "Nisse Hult" });
            return UserList;
        }


        public class Cart
        {
            public int id { get; set; }
            
            public double Price { get; set; }
        }


    }
}

