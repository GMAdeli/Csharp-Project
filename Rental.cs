using System;
using System.Collections.Generic;

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

        public int RentalId { get => rentalId; }
        public int MovieId { get => movieId; set => movieId = value; }
        public int ClientId { get => clientId; set => clientId = value; }
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
        }

        public static List<Rental> GetAllRentals()
        {
            return RentalsList;
        }
    }
}