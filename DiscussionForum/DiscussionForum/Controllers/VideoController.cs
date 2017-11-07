using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiscussionForum.Models;
using PagedList;
namespace DiscussionForum.Controllers
{
    public class VideoController : Controller
    {
        // GET: Video
        public ActionResult Index()
        {
            SampleDBContext db = new SampleDBContext();

            return View(db.Videos.OrderByDescending(x => x.videoId).ToList());
        }

        [HttpGet]
        public ActionResult Add()
        {
            SampleDBContext db = new SampleDBContext();

            return View();
        }

        [HttpPost]
        public ActionResult Add(Video video)
        {
            using (SampleDBContext db = new Models.SampleDBContext())
            {
                db.Videos.Add(video);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        public ActionResult VideoPlayList(string videoCat)
        {
            using (SampleDBContext db = new SampleDBContext())
            {
                var data = db.Videos.Where(x=>x.VideoCategory==videoCat).ToList();

                return View(data);
            }
        }
    }

}