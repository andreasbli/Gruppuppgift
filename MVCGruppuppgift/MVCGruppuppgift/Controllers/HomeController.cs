using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCGruppuppgift.Models;

namespace MVCGruppuppgift.Controllers
{

    public class HomeController : Controller
    {
        tvdbEntities db = new tvdbEntities();
        //GET: Home
        public ActionResult Index(string searchString, string genreSearch)
        {
            var program = db.program.Include(p => p.channel);
            if (!String.IsNullOrEmpty(searchString))
            {
                program = program.Where(p => p.name.Contains(searchString) || p.genre.Contains(searchString) || p.channel.name.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(genreSearch))
            {
                program = program.Where(p => p.genre.Contains(genreSearch));
            }
            ViewBag.newslist = db.news.ToList();
            return View(program.ToList());
        }

        public ActionResult MyPage(person User)
        {
            var Usr = Convert.ToInt32(Session["id"]);
            if (Session["id"] != null)
            {
                ViewBag.favoritelist = db.personchannel.Where(x => x.FK_personid == Usr).Select(z => z.FK_channelid).ToList();
                return View(db.program.Include("channel").ToList());
            }
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult chanlist()
        {
            ViewBag.addfavorite = db.channel.Select(f => new SelectListItem { Value = f.channelid.ToString(), Text = f.name });
            personchannel favorite = new personchannel();
            return View(favorite);
        }
        [HttpPost]
        public ActionResult chanlist(personchannel favorite)
        {
            var Usr = Convert.ToInt32(Session["id"]);
            try
            {
                personchannel newfavorite = new personchannel();
                newfavorite.FK_channelid = favorite.FK_channelid;
                newfavorite.FK_personid = Usr;
                db.personchannel.Add(newfavorite);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("MyPage", "Home");
        }

        public ActionResult addfavorite()
        {
            return View("chanlist");
        }
        [HttpGet]
        public ActionResult AdminPage()
        {
            ViewBag.programList = db.program.Select(f => new SelectListItem { Value = f.programid.ToString(), Text = f.name });
            news newNews = new news();
            return View(newNews);
        }
        [HttpPost]
        public ActionResult AdminPage(news newNews)
        {
            if (ModelState.IsValid)
            {
                news updatenews = new news();
                updatenews.programid = newNews.programid;
                updatenews.information = newNews.information;
                db.news.Add(updatenews);
                db.SaveChanges();
            }
            ModelState.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult newsinfo(int Id)
        {
            List<program> news = db.program.Where(s => s.programid == Id).ToList();
            return View(news);
        }

        [HttpGet]
        public ActionResult channelinfo(int cID)
        {
            List<program> prog = db.program.Where(p => p.FK_channelid == cID).ToList();
            return View(prog);
        }

    }
}