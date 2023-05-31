using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer
{

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    internal class Program
    {
        static List<User> users = new List<User>();

        static void Main(string[] args)
        {

            Initialize();

            string ipAddress = "127.0.0.1";
            int port = 8080;

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

                    // Process the request and generate a response
                    HttpListenerRequest request = context.Request;
                    HttpListenerResponse response = context.Response;

                    // Extract information from the request
                    string method = request.HttpMethod;
                    string url = request.Url.AbsolutePath;

                    // Handle the API endpoints
                    if (url == "/api/users" && method == "GET")
                    {
                        // Get all users
                        string responseBody = JsonConvert.SerializeObject(users);
                        WriteResponse(response, responseBody, "application/json");
                    }
                    else if (url == "/api/users" && method == "POST")
                    {
                        // Add a new user
                        string requestBody = new StreamReader(request.InputStream).ReadToEnd();
                        User newUser = JsonConvert.DeserializeObject<User>(requestBody);
                        users.Add(newUser);

                        string responseBody = JsonConvert.SerializeObject(newUser);
                        WriteResponse(response, responseBody, "application/json");
                    }
                    else
                    {
                        // Unknown endpoint, return a 404 response
                        WriteResponse(response, "Not Found", "text/plain", HttpStatusCode.NotFound);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                listener.Stop();
            }
        }

        static void WriteResponse(HttpListenerResponse response,
            string responseBody,
            string contentType,
            HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            // Set the response content type and length
            response.ContentType = contentType;
            response.ContentLength64 = Encoding.UTF8.GetByteCount(responseBody);

            // Set the response status code
            response.StatusCode = (int)statusCode;

            // Write the response to the client
            using (Stream outputStream = response.OutputStream)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(responseBody);
                outputStream.Write(buffer, 0, buffer.Length);
            }

            // Close the response to send it to the client
            response.Close();
        }

        static void Initialize()
        {
            users.Add(new User { Id = 1, UserName = "王先生", Password = "qwer1234" });
            users.Add(new User { Id = 2, UserName = "陳先生", Password = "qwer2345" });
            users.Add(new User { Id = 3, UserName = "李小姐", Password = "qwer3456" });
            users.Add(new User { Id = 4, UserName = "莊小姐", Password = "qwer7890" });
        }
    }
}
