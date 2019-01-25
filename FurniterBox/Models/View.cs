using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurniterBox.Models
{
    public class View
    {
        public List<Furniture> FurnitureList { get; set; }
        public Users UserData { get; set; }

        public static View view(List<Furniture> furniturelist, Users userdata)
        {
            View VM = new View();
            VM.FurnitureList = furniturelist;
            VM.UserData = userdata;
            return VM;
        }
    }
}
