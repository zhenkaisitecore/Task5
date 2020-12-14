using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task5.Models
{
    public class ImageDataFactory
    {
        private const string XML_LOC = @"~/Comments.xml";
        private static Lazy<ImageDataFactory> _lazy = new Lazy<ImageDataFactory>(() => new ImageDataFactory());
        private XmlDocument _doc;
        private XmlNode _root;

        private ImageDataFactory()
        {
            _doc = new XmlDocument();
            _doc.Load(HttpContext.Current.Server.MapPath(XML_LOC));
            _root = _doc.DocumentElement;
        }

        public List<Comment> GetCommentsByImageName(string name)
        {
            List<Comment> comments = new List<Comment>();

            foreach(XmlNode commentNode in _root.SelectNodes($"Comment[ImageName='{name}']"))
            {
                comments.Add(new Comment() { ImageName = name, Message = commentNode.SelectSingleNode("Message").InnerText});
            }

            return comments;
        }

        public bool AddComment(string name, string msg)
        {
           try
            {
                XmlElement commentElement = _doc.CreateElement("Comment");
                XmlElement imagenameElement = _doc.CreateElement("ImageName");
                XmlElement messageElement = _doc.CreateElement("Message");
                imagenameElement.AppendChild(_doc.CreateTextNode(name));
                messageElement.AppendChild(_doc.CreateTextNode(msg));
                commentElement.AppendChild(imagenameElement);
                commentElement.AppendChild(messageElement);
                _root.AppendChild(commentElement);

                System.Diagnostics.Debug.WriteLine(_root);
                _doc.Save(HttpContext.Current.Server.MapPath(XML_LOC));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public static ImageDataFactory GetInstance() => _lazy.Value;
    }
}