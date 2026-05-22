using System;

namespace MovieRental
{
    public class MovieRentalException : Exception
    {
        public MovieRentalException() : base() { }

        public MovieRentalException(string message) : base(message) { }

        public MovieRentalException(string message, Exception innerException) : base(message, innerException) { }
    }
}