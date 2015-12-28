using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using System.Web.Services.Description;
using System.Xml;


namespace MovieSearch.Models
{
    public class SearchMovie
    {
        [Required(ErrorMessage = " - Cannot be blank.")]
        public string searchWord { get; set; }

        public bool contactServer { get; set; }
        public bool serverResponse { get; set; }

        public List<Movie> movies { get; set; }

        public Movie movie { get; set; }

        public SearchMovie GetListOfMoviesByTitle(string searchword)
        {
            string movieResultsResponse = "";
            SearchMovie searchMovie = new SearchMovie();
            searchMovie.movies = new List<Movie>();
            HttpWebRequest request = WebRequest.Create("http://www.omdbapi.com/?s=" + searchword + "&r=xml") as HttpWebRequest;
            searchMovie.contactServer = true;

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                WebHeaderCollection header = response.Headers;
                searchMovie.serverResponse = true;
                var encoding = ASCIIEncoding.ASCII;
                using (var reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    movieResultsResponse = reader.ReadToEnd();
                }

                XmlDocument xml = new XmlDocument();
                xml.LoadXml(movieResultsResponse);
                XmlNodeList xnList = xml.SelectNodes("/root/result");
                //List<Movie> movieResults = new List<Movie>();

                foreach (XmlNode xn in xnList)
                {

                    searchMovie.movies.Add(new Movie
                    {
                        Title = xn.Attributes["Title"].Value,
                        Year = xn.Attributes["Year"].Value,
                        imdbID = xn.Attributes["imdbID"].Value,
                        type = xn.Attributes["Type"].Value
                    });
                }
                
            }
            catch (Exception ex)
            {
                // response = null; server may be down
                searchMovie.serverResponse = false;
            }
            
            return searchMovie;
        }

        public Movie GetMovieDetailsByMovieID(string movieID)
        {
            string movieDetailsResponse = "";
            HttpWebRequest request = WebRequest.Create("http://www.omdbapi.com/?i=" + movieID + "&r=xml") as HttpWebRequest;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            WebHeaderCollection header = response.Headers;

            var encoding = ASCIIEncoding.ASCII;
            using (var reader = new StreamReader(response.GetResponseStream(), encoding))
            {
                movieDetailsResponse = reader.ReadToEnd();
            }

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(movieDetailsResponse);
            XmlNodeList xnList = xml.SelectNodes("/root/movie");
            Movie movieDetails = new Movie();
            foreach (XmlNode xn in xnList)
            {
                movieDetails.Title = xn.Attributes["title"].Value;
                movieDetails.Year = xn.Attributes["year"].Value;
                movieDetails.Rated = xn.Attributes["rated"].Value;
                movieDetails.Released = xn.Attributes["released"].Value;
                movieDetails.Runtime = xn.Attributes["runtime"].Value;
                movieDetails.Genre = xn.Attributes["genre"].Value;
                movieDetails.Director = xn.Attributes["director"].Value;
                movieDetails.Writer = xn.Attributes["writer"].Value;
                movieDetails.Actors = xn.Attributes["actors"].Value;
                movieDetails.Plot = xn.Attributes["plot"].Value;
                movieDetails.Language = xn.Attributes["language"].Value;
                movieDetails.Country = xn.Attributes["country"].Value;
                movieDetails.Awards = xn.Attributes["awards"].Value;
                movieDetails.Poster = xn.Attributes["poster"].Value;
                movieDetails.metascore = xn.Attributes["metascore"].Value;
                movieDetails.imdbID = xn.Attributes["imdbID"].Value;
                movieDetails.type = xn.Attributes["type"].Value;
            }
            return movieDetails;
        }
    }
}