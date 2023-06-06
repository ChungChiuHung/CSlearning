using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace SimpleHttpServer
{

    internal partial class Program
    {
        static int currentId = 100; // Initial Id value for the default user
        static List<User> users = new List<User>();
        static Dictionary<string, string> authenticatedUsers = new Dictionary<string, string>();

        static void Main(string[] args)
        {

            GenerateDemoData();

            string[] prefixes = { "http://localhost:8080/" };
            var users = GenerateDemoUsers();
            LoginManager.GetUsers(GenerateDemoUsers());
            HttpServer server = new HttpServer(prefixes, users);

            server.Start();

            Console.WriteLine("Press Enter to stop the server...");
            Console.ReadLine();

            #region Previous Version 
            /*
            string url = "http://localhost:8080/";
            using (HttpListener listener = new HttpListener())
            {
                listener.Prefixes.Add(url);
                listener.Start();
                Console.WriteLine("Listening for requests...");

                while (true)
                {
                    HttpListenerContext context = listener.GetContext();
                    ProcessRequest(context);
                }
            }
            */
            #endregion


            #region Direct Request Without AuthToken
            /*
            // Create a listener for incoming HTTP requests
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add($"http://{ipAddress}:{port}/");

            try
            {
                // Start the listener to begin handling requests
                listener.Start();
                Console.WriteLine("Server started. Listening for requests...");

                // Handle incomming requests in a loop
                while (true)
                {
                    // Wait for a request to come in
                    HttpListenerContext context = listener.GetContext();



                    // Process the request in a separate thread
                    ThreadPool.QueueUserWorkItem((state) =>
                    {
                        HttpListenerContext ctx = (HttpListenerContext)state;
                        HttpListenerRequest request = ctx.Request;
                        HttpListenerResponse response = ctx.Response;

                        // Extract information from the request
                        string method = request.HttpMethod;
                        string url = request.Url.AbsolutePath;

                        if (url == "/")
                        {
                            // Read the index.html file
                            string indexPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Views", "index.html");

                            // Check if the index.html file exists
                            if (File.Exists(indexPath))
                            {
                                string responseString = File.ReadAllText(indexPath);

                                // Set the response headers
                                response.ContentType = "text/html";
                                response.ContentLength64 = responseString.Length;

                                // Write the response content
                                using (Stream output = response.OutputStream)
                                {
                                    using (StreamWriter writer = new StreamWriter(output))
                                    {
                                        writer.Write(responseString);
                                    }
                                }
                            }
                            else
                            {
                                response.StatusCode = (int)HttpStatusCode.NotFound;
                                response.Close();
                            }
                        }
                        else if (url == "/home.html")
                        {
                            string homePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Views", "home.html");
                            if (File.Exists(homePath))
                            {
                                string responseString = File.ReadAllText(homePath);
                                WriteResponse(response, responseString, "text/html");
                            }
                            else
                            {
                                WriteResponse(response, "File not found", "text/plain", HttpStatusCode.NotFound);
                            }
                        }
                        // Handle the API endpoints
                        else if (url == "/api/users" && method == "GET")
                        {
                            // Get all users without password
                            var usersWithoutPassword = users.Select(u => new UserWithoutPassword
                            {
                                Name = u.UserName,
                                Id = u.Id
                            }).ToList();

                            string responseBody = JsonConvert.SerializeObject(usersWithoutPassword);
                            WriteResponse(response, responseBody, "application/json");
                        }
                        else if (url == "/api/users" && method == "POST")
                        {
                            // Add a new user
                            string requestBody = new StreamReader(request.InputStream).ReadToEnd();

                            // If the requestBody is null or empty, create a default new User
                            User newUser = !string.IsNullOrEmpty(requestBody)
                                ? JsonConvert.DeserializeObject<User>(requestBody)
                                : new User { UserName = "Guest User", Id = currentId++, Password = $"guest{currentId}" };

                            users.Add(newUser);

                            // Return the newly added user without password
                            var newUserWithoutPassword = new UserWithoutPassword
                            {
                                Name = newUser.UserName,
                                Id = newUser.Id
                            };
                            string responseBody = JsonConvert.SerializeObject(newUser);
                            WriteResponse(response, responseBody, "application/json");
                        }
                        else if (url == "/api/login" && method == "POST")
                        {
                            // Login endpoint
                            string requestBody = new StreamReader(request.InputStream).ReadToEnd();
                            LoginRequest loginRequest = JsonConvert.DeserializeObject<LoginRequest>(requestBody);

                            LoginManager.GetUsers(users);
                            if (LoginManager.AuthenticateUser(loginRequest.Usernmae, loginRequest.Password))
                            {
                                string responseBody = "Login successful";
                                WriteResponse(response, responseBody, "text/plain");
                            }
                            else
                            {
                                string responseBody = "Invalid username or password";
                                WriteResponse(response, responseBody, "text/plain", HttpStatusCode.Unauthorized);
                            }
                        }
                        else
                        {
                            // Unknown endpoint, return a 404 response
                            WriteResponse(response, "Not Found", "text/plain", HttpStatusCode.NotFound);
                        }

                    }, context);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.ToString()}");
            }
            finally
            {
                listener.Stop();
            }
            */
            #endregion

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

        static void ProcessRequest(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            string method = request.HttpMethod;
            string url = request.Url.AbsolutePath;

            if (url == "/api/login" && method == "POST")
            {
                string requestBody = new StreamReader(request.InputStream).ReadToEnd();
                LoginRequest loginRequest = JsonConvert.DeserializeObject<LoginRequest>(requestBody);

                LoginManager.GetUsers(users);

                // Check if the user is valid
                if (LoginManager.AuthenticateUser(loginRequest.UserName, loginRequest.Password))
                {
                    // Generate an authentication token
                    string authToken = Guid.NewGuid().ToString();

                    // Create a response object containing the authentication token
                    LoginResponse loginResponse = new LoginResponse
                    {
                        Token = authToken
                    };

                    // Serialize the resonse object to JSON
                    string responseBody = JsonConvert.SerializeObject(loginResponse);

                    WriteResponse(response, responseBody, "application/json");
                }
                else
                {
                    WriteResponse(response, "Invalid username or password", "text/plain", HttpStatusCode.Unauthorized);
                }
            }
            else if (url == "/api/records" && method == "GET")
            {
                // Check if the request includes the authentication token
                if (request.Headers["Authorization"] != null)
                {
                    string authToken = request.Headers["Authorization"].Replace("Bearer ", "");

                    // Check if the authentication token is valid
                    if (LoginManager.ValidateAuthToken(authToken))
                    {
                        // Return the records
                        // Return the records
                        List<Record> records = new List<Record>
                        {
                        new Record { Id = 1, Name = "Record 1" },
                        new Record { Id = 2, Name = "Record 2" },
                        new Record { Id = 3, Name = "Record 3" }
                        };
                        string responseBody = JsonConvert.SerializeObject(records);
                        WriteResponse(response, responseBody, "application/json");

                    }
                    else
                    {
                        WriteResponse(response, "Unauthorized", "text/plain", HttpStatusCode.Unauthorized);
                    }
                }
                else
                {
                    WriteResponse(response, "Unauthorized", "text/plain", HttpStatusCode.Unauthorized);

                }
            }
            else
            {
                WriteResponse(response, "Not Found", "text/plain", HttpStatusCode.NotFound);
            }

            response.Close();
        }


        static void WriteResponse(
            HttpListenerResponse response,
            string responseBody,
            string contentType,
            HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(responseBody);
            response.ContentLength64 = buffer.Length;
            response.ContentType = contentType;
            response.StatusCode = (int)statusCode;

            // Write the response to the client
            using (Stream outputStream = response.OutputStream)
            {
                outputStream.Write(buffer, 0, buffer.Length);
            }
        }

        static void GenerateDemoData()
        {
            users.Add(new User { Id = 1, UserName = "王先生", Password = "qwer1234" });
            users.Add(new User { Id = 2, UserName = "陳先生", Password = "qwer2345" });
            users.Add(new User { Id = 3, UserName = "李小姐", Password = "qwer3456" });
            users.Add(new User { Id = 4, UserName = "莊小姐", Password = "qwer7890" });
            users.Add(new User { Id = 4, UserName = "admin", Password = "admin" });
        }

    }
}
