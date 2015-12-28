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
            //var list = searchmovie.GetListOfMoviesByTitle(searchword);
            searchmovie.movies = searchmovie.GetListOfMoviesByTitle(searchword).movies;
            searchmovie.serverResponse = true;

            return View(searchmovie);
        }

        public ActionResult Details(string id, string searchword)
        {
            //SearchMovie searchmovie = new SearchMovie();
            //Movie movie = searchmovie.GetMovieDetailsByMovieID(id);
            searchmovie.movie = searchmovie.GetMovieDetailsByMovieID(id);
            searchmovie.searchWord = searchword;
            // return View(movie);
            return View(searchmovie);
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