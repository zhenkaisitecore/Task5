using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace Task5.Models
{
    public class FavouriteDataFactory
    {
        private const string XML_LOC = @"~/Favourites.xml";
        private static Lazy<FavouriteDataFactory> _lazy = new Lazy<FavouriteDataFactory>(() => new FavouriteDataFactory());
        private XmlDocument _doc;
        private XmlNode _root;
        private FavouriteDataFactory()
        {
            _doc = new XmlDocument();
            _doc.Load(HttpContext.Current.Server.MapPath(XML_LOC));
            _root = _doc.DocumentElement;
        }

        public bool CheckFavourite(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("name should not be null or empty!");
            if (_root.SelectSingleNode($"Favourite[User='{HttpContext.Current.User.Identity.Name}' and ImageName='{name}']") != null)
            {
                return true;
            }
            return false;
        }

        public List<string> GetFavourites()
        {
            List<string> favs = new List<string>();
            foreach (XmlNode node in _root.SelectNodes($"Favourite[User='{HttpContext.Current.User.Identity.Name}']"))
            {
                favs.Add(node.SelectSingleNode("ImageName").InnerText);
            }

            return favs;
        }

        public bool AddFavourite(string name)
        {
           try
            {
                var favElement = _doc.CreateElement("Favourite");
                var nameElement = _doc.CreateElement("ImageName");
                var userElement = _doc.CreateElement("User");

                userElement.AppendChild(_doc.CreateTextNode(HttpContext.Current.User.Identity.Name));
                nameElement.AppendChild(_doc.CreateTextNode(name));
                favElement.AppendChild(userElement);
                favElement.AppendChild(nameElement);

                _root.AppendChild(favElement);
                _doc.Save(HttpContext.Current.Server.MapPath(XML_LOC));
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public static FavouriteDataFactory GetInstance() => _lazy.Value;
    }
}