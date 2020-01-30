﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WebServer.Model;

namespace WebServer.Logic
{
    public class UserService
    {
        private const string connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private const string insertCommand =
            @"INSERT INTO [User] (Role, Email, FirstName, LastName, Password)
              VALUES (@Role, @Email, @FirstName, @LastName, @Password)";

        private const string selectCommand =
            @"SELECT * FROM [User]";

        private const string getCommand =
            @"SELECT * FROM [User] WHERE ID = @ID";

        private const string updateCommand =
            @"UPDATE [User] SET Role = @Role, Email = @Email, FirstName = @FirstName, 
              LastName = @LastName, Password = @Password WHERE ID = @ID";

        private const string deleteCommand =
            @"DELETE FROM [User] WHERE ID = @ID";

        public bool Create(User user)
        {
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(insertCommand, connection);
                command.Parameters.AddWithValue("@Role", user.Role);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Password", user.Password);

                connection.Open();
                var result = command.ExecuteNonQuery();

                return result > 0;
            }
        }

        public bool Update(int id, User user)
        {
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(updateCommand, connection);
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@Role", user.Role);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Password", user.Password);

                connection.Open();
                var result = command.ExecuteNonQuery();

                return result > 0;
            }
        }

        public List<User> Get()
        {
            var list = new List<User>();

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(selectCommand, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    var user = LoadRow((IDataRecord)reader);
                    list.Add(user);
                }

                // Call Close when done reading.
                reader.Close();
            }

            return list;
        }

        public User Get(int id)
        {
            var user = new User();

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(getCommand, connection);
                command.Parameters.AddWithValue("@ID", id);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    user = LoadRow((IDataRecord)reader);
                }

                // Call Close when done reading.
                reader.Close();
            }

            return user;
        }


        public bool Delete(int id)
        {
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(deleteCommand, connection);
                command.Parameters.AddWithValue("@ID", id);

                connection.Open();

                var result = command.ExecuteNonQuery();

                return result > 0;
            }
        }

        private User LoadRow(IDataRecord row)
        {
            User user = new User();

            user.ID = Convert.ToInt32(row["ID"]);

            if (!(row["Role"] is DBNull))
                user.Role = Convert.ToInt32(row["Role"]);

            user.Email = Convert.ToString(row["Email"]);
            user.Password = Convert.ToString(row["Password"]);
            user.FirstName = Convert.ToString(row["FirstName"]);
            user.LastName = Convert.ToString(row["LastName"]);

            return user;
        }
    }
}
