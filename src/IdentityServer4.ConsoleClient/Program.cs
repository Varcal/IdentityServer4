using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace IdentityServer4.ConsoleClient
{
    public class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            await RequestUsingPassword();

            await RequestUsingCredentials();

            Console.ReadKey();
        }

        private static async Task<bool> RequestUsingPassword()
        {
            #region request token

            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return true;
            }

            if (disco.TokenEndpoint == null) return false;

            using (var tokenClient = new TokenClient(disco.TokenEndpoint, "var.client", "secret"))
            {
                var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("varcal", "123456", "Api");

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                    return true;
                }

                Console.WriteLine("Usando usuario e senha");
                Console.WriteLine(tokenResponse.Json);
                Console.WriteLine("\n\n");
                #endregion

                #region call api

                using (var client = new HttpClient())
                {
                    client.SetBearerToken(tokenResponse.AccessToken);

                    var response = await client.GetAsync("http://localhost:5001/api/identity");

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode);
                    }
                    else
                    {
                        Console.WriteLine("Chamndo a api");
                        var content = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(JArray.Parse(content));
                        Console.WriteLine("\n\n");
                    }

                    #endregion

                    return false;
                }
            }
        }

        private static async Task<bool> RequestUsingCredentials()
        {
            #region request token

            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return true;
            }

            using (var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret"))
            {
                var tokenResponse = await tokenClient.RequestClientCredentialsAsync("Api");

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                    return true;
                }

                Console.WriteLine("Usando credenciais do client");
                Console.WriteLine(tokenResponse.Json);
                Console.WriteLine("\n\n");

                #endregion

                #region call api

                using (var client = new HttpClient())
                {
                    client.SetBearerToken(tokenResponse.AccessToken);

                    var response = await client.GetAsync("http://localhost:5001/api/identity");

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode);
                    }
                    else
                    {
                        Console.WriteLine("Chamndo a api");
                        var content = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(JArray.Parse(content));
                        Console.WriteLine("\n\n");
                    }

                    #endregion

                    return false;
                }
            }
        }
    }
}
