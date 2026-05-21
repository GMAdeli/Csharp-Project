using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

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
        private static string filePath = "clients.json";

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
            SaveToFile();
        }

        public static void SaveToFile()
        {
            try
            {
                string json = JsonConvert.SerializeObject(ClientsList, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving clients: " + ex.Message);
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
                        ClientsList = JsonConvert.DeserializeObject<List<Client>>(json);
                        if (ClientsList == null) ClientsList = new List<Client>();
                        if (ClientsList.Count > 0)
                        {
                            _count = ClientsList[ClientsList.Count - 1].ClientId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading clients: " + ex.Message);
            }
        }

        public static List<Client> GetAllClients()
        {
            return ClientsList;
        }
    }
}