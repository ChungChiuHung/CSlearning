using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpClient
{
    class LoginData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Base URL of the server
            string baseUrl = "http://localhost:8080/";

            LoginData loginData = new LoginData { 
              UserName = "Hank",
              Password = "password123"
            };

            // Create an HttpClient instance
            using (HttpClient client = new HttpClient())
            {
               // Perform login and retrieve authentication token
               string authToken = await PerformLogin(client, baseUrl, loginData);

               // If login was successful and we obtained an authentication token
               if(!string.IsNullOrEmpty(authToken))
               {
                  // Use the authentication token to make subsequent requests
                  await RetriveRecords(client, baseUrl, authToken);
               }
               else
               {
                  Console.WriteLine("Login failed. Unable to retrieve records.");
               }
                
            }

            Console.ReadLine();
        }

        private static async Task<string> PerformLogin(HttpClient client, string baseUrl, LoginData loginData)
        {
            // Convert the login data to JSON
            string jsonRequest = JsonConvert.SerializeObject(loginData);

            try
            {
                // Send the login request to the server
                HttpResponseMessage response = await client.PostAsync(baseUrl + "/api/login",
                    new StringContent(jsonRequest, Encoding.UTF8, "application/json"));

                // Read the response content as a string
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);

                // If login was successful, retrieve the authentication token
                if(response.IsSuccessStatusCode)
                {
                    // Login successful
                    Console.WriteLine("Login successful!");

                    dynamic responseData = JsonConvert.DeserializeObject(responseContent);
                    return responseData.Token;
                }
                else
                {
                    Console.WriteLine($"Login failed with status code {response.StatusCode}"); 
                    return null;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred: {ex.Message}");

                return null;
            }
        }

        static async Task RetriveRecords(HttpClient client, string baseUrl, string authToken)
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authToken}");
            try
            {
                // Send the request to retrieve records
                HttpResponseMessage response = await client.GetAsync(baseUrl + "/api/records");

                // Read the response content as a string
                string responseContent = await response.Content.ReadAsStringAsync();

                if(response.IsSuccessStatusCode)
                {
                    var records = JsonConvert.DeserializeObject(responseContent);

                    await Console.Out.WriteLineAsync("Records: ");

                    // Print records
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


    }
}
