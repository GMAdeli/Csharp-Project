using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace MovieRental
{
    public enum Genre { Horror, Comedy, Science, Action, Thriller }

    public class Movie
    {
        private static int _count = 0;
        private int movieId;
        private string movieTitle;
        private Genre genre;
        private int year;
        private bool isAvailable;
        private double pricePerDay;

        public static List<Movie> MoviesList = new List<Movie>();
        private static string filePath = "movies.json";

        public string MovieTitle { get => movieTitle; set => movieTitle = value; }
        public Genre Genre { get => genre; set => genre = value; }
        public int Year
        {
            get => year;
            set
            {
                if (value < 1940)
                    throw new Exception("Year can't be below 1940!");
                year = value;
            }
        }
        public bool IsAvailable { get => isAvailable; set => isAvailable = value; }
        public int MovieId { get => movieId; }
        public double PricePerDay { get => pricePerDay; set => pricePerDay = value; }

        public Movie(string movieTitle, Genre genre, int year, bool isAvailable)
        {
            _count++;
            movieId = _count;
            MovieTitle = movieTitle;
            Year = year;
            Genre = genre;
            IsAvailable = isAvailable;
            PricePerDay = 15.0;
            MoviesList.Add(this);
            SaveToFile();
        }

        public static void SaveToFile()
        {
            try
            {
                string json = JsonConvert.SerializeObject(MoviesList, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving movies: " + ex.Message);
            }
        }

        public static void LoadFromFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        MoviesList = JsonConvert.DeserializeObject<List<Movie>>(json);
                        if (MoviesList == null) MoviesList = new List<Movie>();
                        if (MoviesList.Count > 0)
                        {
                            _count = MoviesList[MoviesList.Count - 1].MovieId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading movies: " + ex.Message);
            }
        }

        public static List<Movie> GetAllMovies()
        {
            return MoviesList;
        }
    }
}