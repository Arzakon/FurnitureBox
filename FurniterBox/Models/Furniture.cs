using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace FurniterBox.Models
{
    //Gemensamma egenskaper för samtliga möbler
    public class Furniture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Designer { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public int Points { get; set; }
        public int InitialCount { get; set; }
        public bool Delivery { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Lenght { get; set; }
        public int tempPointsInitialCount { get; set; }
        public int tempPointsCount { get; set; }
        public string ImgUrl { get; set; }



        public static List<Furniture> CreateData()
        {
            List<Furniture> FurnitureList = new List<Furniture>();


            FurnitureList.Add(new Sofa { Id = 1, Type = "Soffa", Name = "Myssoffan", Designer = "Alex Gigi", Color = "Grå", Height = 50, Lenght = 150, Width = 200, Count = 5, InitialCount = 5, Price = 5000,  Delivery = true, ImgUrl = "/Content/Images/1.jpg" });
            FurnitureList.Add(new Sofa { Id = 2, Type = "Soffa", Name = "Gamesoffan", Designer = "Allan Ballan", Color = "Svart", Height = 50, Lenght = 150, Width = 200, Count = 5, InitialCount = 5, Price = 6500,  Delivery = true, ImgUrl = "/Content/Images/2.jpg" });
            FurnitureList.Add(new Sofa { Id = 3, Type = "Soffa", Name = "Farmorsoffan", Designer = "Guci Versace", Color = "Grön", Height = 50, Lenght = 150, Width = 200, Count = 5, InitialCount = 5, Price = 4000,  Delivery = true, ImgUrl = "/Content/Images/3.jpg" });
            FurnitureList.Add(new Sofa { Id = 4, Type = "Soffa", Name = "Familjsoffan", Designer = "Alex Gigi", Color = "Brun", Height = 50, Lenght = 150, Width = 200, Count = 5, InitialCount = 5, Price = 9000, Delivery = true, ImgUrl = "/Content/Images/4.jpg" });
            FurnitureList.Add(new Table { Id = 5, Type = "Bord", Name = "Köksbord", Designer = "Alex Gigi", Color = "Vit", Height = 100, Lenght = 100, Width = 100, Count = 5, InitialCount = 5, Price = 5000,  MaxWidth = 150, Seating = 6, Delivery = true, ImgUrl = "/Content/Images/5.jpg" });
            FurnitureList.Add(new Table { Id = 6, Type = "Bord", Name = "Gamingbord", Designer = "PUBG", Color = "Grå", Height = 100, Lenght = 100, Width = 100, Count = 5, InitialCount = 5, Price = 5000,  Seating = 1, Delivery = true, ImgUrl = "/Content/Images/6.jpg" });
            FurnitureList.Add(new Shelf { Id = 7, Type = "Hylla", Name = "Vardagsrumshylla", Designer = "Alex Gigi", Color = "Vit", Height = 200, Lenght = 50, Width = 50, Count = 5, InitialCount = 5, Price = 1500, NumberOfShelves = 10, Delivery = true, ImgUrl = "/Content/Images/7.jpg" });

            FurnitureList.Add(new Chair { Id = 8, Type = "Stol", Name = "Köksbordsstol", Designer = "Alex Gigi", Color = "Vit", Height = 70, Lenght = 30, Width = 40, Count = 5, InitialCount = 5, Price = 500,TypeOfChair="Köksstol", Delivery = true, ImgUrl = "/Content/Images/8.jpg" });
            FurnitureList.Add(new Chair { Id = 9, Type = "Stol", Name = "Insane", Designer = "ACER", Color = "Svart", Height = 70, Lenght = 30, Width = 40, Count = 5, InitialCount = 5, Price = 2000, TypeOfChair = "Gamerstol", Delivery = true, ImgUrl = "/Content/Images/9.jpg" });

            return FurnitureList;
        }

        public static string filepath = HttpContext.Current.Server.MapPath("~/App_Data/Storage/library.json");

        public static bool SaveData(List<Furniture> furniturelist)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(furniturelist.ToArray(), settings);
            System.IO.File.WriteAllText(filepath, json);

            return true;
        }


        public static List<Furniture> GetData()
        {
            List<Furniture> data;
            if (System.IO.File.Exists(filepath))
            {
                var settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Objects
                };
                var json = System.IO.File.ReadAllText(filepath);
                data = JsonConvert.DeserializeObject<List<Furniture>>(json, settings);
            }
            else
            {
                data = CreateData();
            }

            // Algoritmen som rangordnar den mest köpta och populära varan

            data = data.OrderBy(x => x.InitialCount).ToList();
            int points = 0;
            foreach (var d1 in data)
            {
                points = points + 5;
                d1.tempPointsInitialCount = points;
                d1.Points = points;
            }


            data = data.OrderBy(x => x.Count).ToList();
            points = 0;
            foreach (var d2 in data)
            {
                points = points + 3;
                d2.tempPointsCount = points;
                d2.Points += points;
            }

            data = data.OrderByDescending(x => x.Points).ToList();
            SaveData(data);
            return data;
        }
    }
    //Unika egenskaperna för varje möbel
        public class Sofa : Furniture
        {
            public bool Bäddsoffa { get; set; }
        }
        public class Table : Furniture
        {
            public int Seating { get; set; }
            public int MaxWidth { get; set; }
            public bool Resizable { get; set; }
        }

        public class Shelf : Furniture
        {
            public int NumberOfShelves { get; set; }
        }

        public class Chair : Furniture
        {
            public string TypeOfChair { get; set; }
        }
    
}
