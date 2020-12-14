using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task5.Models;

namespace Task5.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private ImageDataFactory factory = ImageDataFactory.GetInstance();
        private FavouriteDataFactory favFactory = FavouriteDataFactory.GetInstance();
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetComments(string name) => Json(factory.GetCommentsByImageName(name), JsonRequestBehavior.AllowGet);

        [HttpPost]
        public JsonResult AddComment(Comment comment) => Json(factory.AddComment(comment.ImageName, comment.Message));

        [HttpGet]
        public JsonResult CheckFavourite(string ImageName) => Json(favFactory.CheckFavourite(ImageName), JsonRequestBehavior.AllowGet);

        [HttpPost]
        public JsonResult AddFavourite(string ImageName) => Json(favFactory.AddFavourite(ImageName));
    }
}