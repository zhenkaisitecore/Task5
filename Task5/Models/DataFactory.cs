using System;
using System.Web;
using System.Xml;
using System.Collections.Generic;

namespace Task5.Models
{
    public class DataFactory
    {
        private static Lazy<DataFactory> _lazy = new Lazy<DataFactory>(() => new DataFactory());
        private XmlNode _root;

        private DataFactory()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath(@"~/Images.xml"));
            _root = doc.DocumentElement;
        }

        public static DataFactory GetInstance() => _lazy.Value;

        public List<Image> GetAllByPreferences(Pets preference)
        {
            List<Image> result = new List<Image>();
            foreach (XmlNode node in _root.SelectNodes(preference.ToString()))
            {
                result.Add(CreateImageInstance(node));
            }

            return result;
        }

        public List<Image> GetFavourites(Pets preference)
        {
            List<Image> imgs = new List<Image>();
            var factory = FavouriteDataFactory.GetInstance();
            foreach(string fav in factory.GetFavourites())
            {
                imgs.Add(CreateImageInstance(_root.SelectSingleNode($"{preference.ToString()}[Name='{fav}']")));
            }

            return imgs;
        }

        public Image Get(string name)
        {
            XmlNode target = _root.SelectSingleNode($"*[Name='{name}']");
            return CreateImageInstance(target);
        }

        private Image CreateImageInstance(XmlNode node)
        {
            return new Image()
            {
                Name = node.SelectSingleNode("Name").InnerText,
                Url = node.SelectSingleNode("Url").InnerText,
                Alt = node.SelectSingleNode("Alt").InnerText
            };
        }
    }
}