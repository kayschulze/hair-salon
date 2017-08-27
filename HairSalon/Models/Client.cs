using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
    public class Client
    {
        private string _name;
        private string _address;
        private string _phonenumber;
        private int _id;
        private int _stylistId;

        public Client(string name, string address, string phonenumber, int stylistId, int id = 0)
        {
            _name = name;
            _address = address;
            _phonenumber = phonenumber;
            _stylistId = stylistId;
            _id = id;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetAddress()
        {
            return _address;
        }

        public string GetPhoneNumber()
        {
            return _phonenumber;
        }

        public int GetStylistId()
        {
            return _stylistId;
        }

        public int GetId()
        {
            return _id;
        }

        public void SetId(int id)
        {
            _id = id;
        }

        public override bool Equals(System.Object otherClient)
        {
            if (!(otherClient is Client))
            {
                return false;
            }
            else
            {
                Client newClient = (Client) otherClient;
                bool idEquality = this.GetId() == newClient.GetId();
                bool nameEquality = this.GetName() == newClient.GetName();
                bool addressEquality = this.GetAddress() == newClient.GetAddress();
                bool phoneNumberEquality = this.GetPhoneNumber() == newClient.GetPhoneNumber();
                bool stylistIdEquality = this.GetStylistId() == newClient.GetStylistId();
                return(idEquality && nameEquality && addressEquality && phoneNumberEquality && stylistIdEquality);
            }
        }

        public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO clients(name, address, phone_number, stylist_id) VALUES (@name, @address, @phone_number, @stylist_id);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            MySqlParameter address = new MySqlParameter();
            address.ParameterName = "@address";
            address.Value = this._address;
            cmd.Parameters.Add(address);

            MySqlParameter phonenumber = new MySqlParameter();
            phonenumber.ParameterName = "@phone_number";
            phonenumber.Value = this._phonenumber;
            cmd.Parameters.Add(phonenumber);

            MySqlParameter stylistId = new MySqlParameter();
            stylistId.ParameterName = "@stylist_id";
            stylistId.Value = this._stylistId;
            cmd.Parameters.Add(stylistId);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client> {};

            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients;";

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            while(rdr.Read())
            {
                string clientAddress = rdr.GetString(3);
                int clientId = rdr.GetInt32(0);
                string clientName = rdr.GetString(1);
                string clientPhoneNumber = rdr.GetString(2);
                int clientStylistId = rdr.GetInt32(4);

                Client newClient = new Client(clientName, clientAddress, clientPhoneNumber, clientStylistId, clientId);

                allClients.Add(newClient);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allClients;
        }

        public static List<Client> GetAllStylistClients(int stylistid)
        {
            List<Client> allStylistClients = new List<Client> {};

            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = (@searchStylistId);";

            MySqlParameter searchStylistId = new MySqlParameter();
            searchStylistId.ParameterName = "@searchStylistId";
            searchStylistId.Value = stylistid;
            cmd.Parameters.Add(searchStylistId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            while(rdr.Read())
            {
                string clientAddress = rdr.GetString(3);
                int clientId = rdr.GetInt32(0);
                string clientName = rdr.GetString(1);
                string clientPhoneNumber = rdr.GetString(2);
                int clientStylistId = rdr.GetInt32(4);

                Client newClient = new Client(clientName, clientAddress, clientPhoneNumber, clientStylistId, clientId);

                allStylistClients.Add(newClient);
                Console.WriteLine(newClient.GetName());
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return allStylistClients;
        }

        public static Client Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int clientId = 0;
            string clientName = "";
            string clientAddress = "";
            string clientPhoneNumber = "";
            int stylistId = 0;

            while(rdr.Read())
            {
                clientId = rdr.GetInt32(0);
                clientName = rdr.GetString(1);
                clientAddress = rdr.GetString(2);
                clientPhoneNumber = rdr.GetString(3);
                stylistId = rdr.GetInt32(4);
            }

            Client newClient = new Client(clientName, clientAddress, clientPhoneNumber, stylistId, clientId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return newClient;
        }

        public void UpdateClient(string newName, string newAddress, string newPhoneNumber, int clientId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE clients SET name = @newName, address = @newAddress, phone_number = @newPhoneNumber WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@newName";
            name.Value = newName;
            cmd.Parameters.Add(name);

            MySqlParameter address = new MySqlParameter();
            address.ParameterName = "@newAddress";
            address.Value = newAddress;
            cmd.Parameters.Add(address);

            MySqlParameter phonenumber = new MySqlParameter();
            phonenumber.ParameterName = "@newPhoneNumber";
            phonenumber.Value = newPhoneNumber;
            cmd.Parameters.Add(phonenumber);

            cmd.ExecuteNonQuery();
            _name = newName;
            _address = newAddress;
            _phonenumber = newPhoneNumber;

            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void DeleteClient(int clientId)
        {
            MySqlConnection conn = DB.Connection() as MySqlConnection;
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients WHERE id = @clientId;";

            MySqlParameter thisClientId = new MySqlParameter();
            thisClientId.ParameterName = "@clientId";
            thisClientId.Value = clientId;
            cmd.Parameters.Add(thisClientId);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients;";
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
