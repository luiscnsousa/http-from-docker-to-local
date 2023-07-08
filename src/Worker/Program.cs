using Flurl;
using Flurl.Http;

namespace Worker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var apiBaseUrl = Environment.GetEnvironmentVariable("API_BASE_URL");

            Console.WriteLine($"HTTP GET {apiBaseUrl}/api/values");

            var response = await apiBaseUrl
                .AppendPathSegment("api")
                .AppendPathSegment("values")
                .GetJsonAsync<IEnumerable<string>>();

            foreach (var item in response)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Done!");
        }
    }
}