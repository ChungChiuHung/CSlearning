using SimpleServerCommon;
using System;
using System.Collections.Generic;


namespace DbHttpServer
{
    internal class Program
    {
        static List<User> users = new List<User>();

        static void Main(string[] args)
        {
            string[] prefixes = { "http://localhost:8080/" };
            var users = GenerateDemoUsers();
            LoginManager.GetUsers(users);

            HttpServer server = new HttpServer(prefixes, users);

            server.Start();

            Console.WriteLine("Press Enter to Stop the Server... ");

            Console.ReadLine();
        }


        static List<User> GenerateDemoUsers()
        {
            List<User> users = new List<User>();

            string[] names = { "Hank", "Johnny", "Stacy", "Alice", "Bob", "Emma", "Liam", "Olivia", "Jacob", "Sophia" };

            string salt = LoginManager.GenerateSalt();

            for (int i = 0; i < names.Length; i++)
            {
                string name = names[i];
                string password = "password123"; // Set a default password for all users

                string hashedPassword = LoginManager.GenerateHashedPassword(password, salt);

                User user = new User
                {
                    Id = 100 + (i + 1), // Generate user ID starting from 101
                    UserName = name,
                    Salt = salt,
                    HashedPassword = hashedPassword,
                    // Set other properties as needed
                };

                users.Add(user);
            }

            return users;
        }
    }
}
