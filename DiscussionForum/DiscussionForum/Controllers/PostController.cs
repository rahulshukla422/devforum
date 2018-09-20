using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiscussionForum.Models;
using System.Net.Mail;
using System.Net;
using PagedList;

namespace DiscussionForum.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index(string SearchString, int? page)
        {
            int pageSize = 10;
            int pageNumber = page.HasValue ? Convert.ToInt32(page) : 1;

            SampleDBContext db = new SampleDBContext();

            IPagedList<POST> post = null;
            COMMENT cmt = new Models.COMMENT();
            ViewBag.comm = cmt;
            if (!string.IsNullOrEmpty(SearchString))
            {
                post = db.POSTs.Where(x => x.postTitle.Contains(SearchString)).OrderByDescending(x => x.postedDate).ToPagedList(pageNumber, pageSize);
            }
            else
            {
                post = db.POSTs.OrderByDescending(x => x.postedDate).ToPagedList(pageNumber, pageSize);
            }
            return View(post);

        }
        public ActionResult Index1(string SearchString, int? page)
        {
            int pageSize = 10;
            int pageNumber = page.HasValue ? Convert.ToInt32(page) : 1;

            SampleDBContext db = new SampleDBContext();

            IPagedList<POST> post = null;
            COMMENT cmt = new Models.COMMENT();
            ViewBag.comm = cmt;
            if (!string.IsNullOrEmpty(SearchString))
            {
                post = db.POSTs.Where(x => x.postTitle.Contains(SearchString)).OrderByDescending(x => x.postedDate).ToPagedList(pageNumber, pageSize);
            }
            else
            {
                post = db.POSTs.OrderByDescending(x => x.postedDate).ToPagedList(pageNumber, pageSize);
            }
            return View(post);

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(POST post)
        {
            using (SampleDBContext db = new Models.SampleDBContext())
            {
                if (ModelState.IsValid)
                {
                    post.postedDate = DateTime.Now;
                    db.POSTs.Add(post);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }

            }
        }

        [HttpGet]
        public ActionResult Comment(int postId)
        {
            ViewBag.postId = postId;

            return View();
        }
        [HttpPost]
        public ActionResult Comment(COMMENT comment)
        {
            using (SampleDBContext db = new Models.SampleDBContext())
            {
                if (ModelState.IsValid)
                {
                    comment.postedDate = DateTime.Now;
                    db.COMMENTs.Add(comment);
                    db.SaveChanges();
                    return RedirectToAction("Details", new { postId = comment.postId });
                }
                else
                {
                    return View();
                }
            }

        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Details(int postId)
        {
            SampleDBContext db = new Models.SampleDBContext();

            POST data = db.POSTs.Where(x => x.postId == postId).FirstOrDefault();

            return View(data);
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            try
            {
                string from = "youremail@gmail.com";
                string pass = "yourpassword";


                MailMessage mail = new MailMessage();

                // Assiging values 
                mail.From = new MailAddress(from);
                mail.To.Add(new MailAddress(from));
                mail.Body = contact.Message;
                mail.Subject = "From :" + contact.Email + "  " + contact.Subject;

                // Calling the smtp server

                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";

                NetworkCredential net = new NetworkCredential(from, pass);
                client.EnableSsl = true;
                client.Credentials = net;

                client.Send(mail);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.error = "Some error occured on the while sending query ! Please try after some time ";
                return View();
            }
        }

        public ActionResult MVC()
        {
            return View();
        }

        #region AngularJS Data

        [HttpPost]
        public JsonResult AddCommments(COMMENT com)
        {
            using (SampleDBContext db = new Models.SampleDBContext())
            {
                com.postedDate = DateTime.Now;
                db.COMMENTs.Add(com);
                db.SaveChanges();

                return new JsonResult() { Data = db.COMMENTs.Where(x => x.postId == com.postId).ToList(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }

        #endregion
    }
}
