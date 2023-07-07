using Flurl;
using Flurl.Http;

namespace Worker
{
    public class Program
    {
        const string ApiBaseUrl = "http://localhost:55173";

        public static async Task Main(string[] args)
        {
            Console.WriteLine($"Hello, I'm about to call {ApiBaseUrl}!");

            var response = await ApiBaseUrl
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