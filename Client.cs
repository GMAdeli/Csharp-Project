using System;
using System.Collections.Generic;

namespace MovieRental
{
    public class Client
    {
        private static int _count = 0;
        private int clientId;
        private string firstName;
        private string lastName;
        private string email;

        public static List<Client> ClientsList = new List<Client>();

        public string FirstName
        {
            get => firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("First name cannot be empty!");
                firstName = value;
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Last name cannot be empty!");
                lastName = value;
            }
        }

        public string Email
        {
            get => email;
            set
            {
                if (!value.Contains("@"))
                    throw new Exception("Invalid email address!");
                email = value;
            }
        }

        public int ClientId { get => clientId; }

        public Client(string firstName, string lastName, string email)
        {
            _count++;
            clientId = _count;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ClientsList.Add(this);
        }

        public static List<Client> GetAllClients()
        {
            return ClientsList;
        }
    }
}