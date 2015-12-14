using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieSearch.Models;

namespace MovieSearch.Controllers
{
    public class HomeController : Controller
    {
        SearchMovie searchmovie = new SearchMovie();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string searchword)
        {
            //SearchMovie searchmovie = new SearchMovie();
            searchmovie.searchWord = searchword;
            var list = searchmovie.GetListOfMoviesByTitle(searchword);
            return View(list);
        }

        //public ActionResult SearchAgain()
        //{
        //    var list = searchmovie.GetListOfMoviesByTitle(searchmovie.searchWord);
        //    return View("Search", list);
        //}

        public ActionResult Details(string id)
        {
            //SearchMovie searchmovie = new SearchMovie();
            Movie movie = searchmovie.GetMovieDetailsByMovieID(id);
            return View(movie);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}