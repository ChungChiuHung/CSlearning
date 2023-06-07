using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleServerCommon
{
    public class HttpServer
    {
        private readonly HttpListener _listener;
        private readonly List<User> users;
        private readonly Dictionary<string, string> htmlPaths;

        public HttpServer(string[] prefixes, List<User> users)
        {
            this.users = users;
            _listener = new HttpListener();
            foreach (string prefix in prefixes)
            {
                _listener.Prefixes.Add(prefix);
            }

            this.users = users;

            htmlPaths = new Dictionary<string, string>();
            htmlPaths.Add("/", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Views", "index.html"));
            htmlPaths.Add("/home", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Views", "home.html"));
            htmlPaths.Add("/login", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Views", "login.html"));


        }

        public void Start()
        {
            _listener.Start();
            Console.WriteLine("Server started.");

            ThreadPool.QueueUserWorkItem((o) =>
            {
                try
                {
                    while (true)
                    {
                        HttpListenerContext context = _listener.GetContext();
                        ProcessRequest(context);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                finally
                {
                    Console.WriteLine("Listener Stop!");
                    _listener.Stop();
                }
            });
        }


        public void ProcessRequest(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            string method = request.HttpMethod;
            string url = request.Url.AbsolutePath;

            if (htmlPaths.ContainsKey(url))
            {
                ServerIndexHtml(context, htmlPaths[url]);
            }
            else
            {
                switch (url)
                {
                    case "/api/users":
                        if (method == "GET")
                        {
                            if (ValidateAuthToken(request))
                            {
                                ServerUserList(response);
                            }
                            else
                            {
                                Unauthorized(response);
                            }
                        }
                        else if (method == "POST")
                        {
                            if (ValidateAuthToken(request))
                            {
                                CreateUser(request, response);
                            }
                            else
                            {
                                Unauthorized(response);
                            }
                        }
                        break;
                    case "/api/records":
                        if (method == "GET")
                        {
                            if (ValidateAuthToken(request))
                            {
                                GetRecords(response);
                            }
                            else
                            {
                                Unauthorized(response);
                            }
                        }
                        else if (method == "POST")
                        {
                            if (ValidateAuthToken(request))
                            {
                                CreateNewRecord(request, response);
                            }
                            else
                            {
                                Unauthorized(response);
                            }
                        }
                        break;
                    case "/api/login":
                        if (method == "POST")
                        {
                            Login(request, response);
                        }
                        break;
                    default:
                        NotFound(response);
                        break;
                }
            }

            response.Close();
        }


        public static string Login(HttpListenerRequest request, HttpListenerResponse response)
        {
            if (request.HttpMethod == "POST")
            {
                // Read the request body
                string requestBody;
                using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    requestBody = reader.ReadToEnd();
                }

                // Deserialize the login request data
                LoginRequest loginRequest = JsonConvert.DeserializeObject<LoginRequest>(requestBody);

                // Validate the username and password
                bool isAuthenticated = LoginManager.AuthenticateUser(loginRequest.UserName, loginRequest.Password);

                if (isAuthenticated)
                {
                    // Generate a new authentication token
                    string authToken = LoginManager.GenerateAuthToken(loginRequest.UserName);

                    // Return the token as a respomes
                    string responseJson = JsonConvert.SerializeObject(new { Token = authToken });
                    response.ContentType = "application/json";
                    response.StatusCode = (int)HttpStatusCode.OK;

                    using (StreamWriter writer = new StreamWriter(response.OutputStream))
                    {
                        writer.Write(responseJson);
                    }

                    return authToken;

                }
                else
                {
                    // Unauthorized access
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
            }
            else
            {
                // Invalid request method
                response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
            }

            response.Close();
            return null;


        }


        private bool ValidateAuthToken(HttpListenerRequest request)
        {
            string authToken = request.Headers["Authorization"];
            if(!string.IsNullOrEmpty(authToken))
            {
                string[] tokenParts = authToken.Split(' ');
                if(tokenParts.Length == 2 && tokenParts[0] == "Bearer")
                {
                    string token = tokenParts[1];
                    return LoginManager.ValidateAuthToken(token);
                }
            }
            return false;
        }

        private void UserLogin(HttpListenerRequest request, HttpListenerResponse response)
        {
            // Login endpoint
            string requestBody = new StreamReader(request.InputStream).ReadToEnd();
            LoginRequest loginRequest = JsonConvert.DeserializeObject<LoginRequest>(requestBody);

            LoginManager.GetUsers(users);
            if (LoginManager.AuthenticateUser(loginRequest.UserName, loginRequest.Password))
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

        private void CreateNewRecord(HttpListenerRequest request, HttpListenerResponse response)
        {
            // Todo: Create a new record
        }

        private void GetRecords(HttpListenerResponse response)
        {
            // Todo: Get Records
        }

        private void WriteResponse(HttpListenerResponse response,
            string responseBody,
            string contentType,
            HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            response.ContentType = contentType;
            response.StatusCode = (int)statusCode;

            byte[] buffer = Encoding.UTF8.GetBytes(responseBody);
            response.ContentLength64 = buffer.Length;

            using (Stream outputStream = response.OutputStream)
            {
                outputStream.Write(buffer, 0, buffer.Length);
            }
        }

        private void CreateUser(HttpListenerRequest request, HttpListenerResponse response)
        {
            string requestBody = new StreamReader(request.InputStream).ReadToEnd();
            User newUser = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(requestBody);

            if(newUser!=null)
            {
                // Generate salt and hash the passwords
                string salt = GenerateSalt();
                string hashedPassword = LoginManager.GenerateHashedPassword(newUser.Password, salt);

                newUser.Salt = salt;
                newUser.HashedPassword = hashedPassword;
                newUser.Id = LoginManager.GetNextUserId();

                users.Add(newUser);

                string responseBody = Newtonsoft.Json.JsonConvert.SerializeObject(newUser);
                WriteResponse(response, responseBody, "application/json", HttpStatusCode.Created);
            }
            else
            {
                WriteResponse(response, requestBody, "application/json", HttpStatusCode.BadRequest);
            }
            
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private void ServerUserList(HttpListenerResponse response)
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

        private void ServerIndexHtml(HttpListenerContext context, string filePath)
        {
            if (File.Exists(filePath))
            {
                string html = File.ReadAllText(filePath);
                byte[] buffer = Encoding.UTF8.GetBytes(html);

                context.Response.ContentType = "text/html";
                context.Response.ContentLength64 = buffer.Length;

                using (Stream output = context.Response.OutputStream)
                {
                    output.Write(buffer, 0, buffer.Length);
                }
            }
            else
            {
                ServerNotFound(context);
            }
        }

        private void ServerNotFound(HttpListenerContext context)
        {
            NotFound(context.Response);
        }

        private void ServerBadRequest(HttpListenerContext context)
        {
            context.Response.StatusCode = 400;
            context.Response.Close();
        }

        private void Unauthorized(HttpListenerResponse response)
        {
            WriteResponse(response, "Unauthorized", "text/plain", HttpStatusCode.Unauthorized);
        }

        private void NotFound(HttpListenerResponse response)
        {
            WriteResponse(response, "Not Found", "text/plain", HttpStatusCode.NotFound);
        }
    }


    class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    class LoginResponse
    {
        public string Token { get; set; }
    }
}
