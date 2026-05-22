using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace MovieRental
{
    public class Rental
    {
        private static int _count = 0;
        private int rentalId;
        private int movieId;
        private int clientId;
        private DateTime rentalDate;
        private DateTime? returnDate;
        private double totalPrice;

        public static List<Rental> RentalsList = new List<Rental>();
        private static string filePath = "rentals.json";

        public int RentalId { get => rentalId; }

        public int MovieId
        {
            get => movieId;
            set
            {
                if (value <= 0)
                    throw new MovieRentalException("Invalid Movie ID!");
                movieId = value;
            }
        }

        public int ClientId
        {
            get => clientId;
            set
            {
                if (value <= 0)
                    throw new MovieRentalException("Invalid Client ID!");
                clientId = value;
            }
        }

        public DateTime RentalDate { get => rentalDate; set => rentalDate = value; }
        public DateTime? ReturnDate { get => returnDate; set => returnDate = value; }
        public double TotalPrice { get => totalPrice; set => totalPrice = value; }

        public Rental(int movieId, int clientId, DateTime rentalDate, DateTime? returnDate, double totalPrice)
        {
            _count++;
            rentalId = _count;
            MovieId = movieId;
            ClientId = clientId;
            RentalDate = rentalDate;
            ReturnDate = returnDate;
            TotalPrice = totalPrice;
            RentalsList.Add(this);
            SaveToFile();
        }

        public static void SaveToFile()
        {
            try
            {
                string json = JsonConvert.SerializeObject(RentalsList, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving rentals: " + ex.Message);
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
                        RentalsList = JsonConvert.DeserializeObject<List<Rental>>(json);
                        if (RentalsList == null) RentalsList = new List<Rental>();
                        if (RentalsList.Count > 0)
                        {
                            _count = RentalsList[RentalsList.Count - 1].RentalId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading rentals: " + ex.Message);
            }
        }

        public static List<Rental> GetAllRentals()
        {
            return RentalsList;
        }
    }
}