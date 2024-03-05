using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*ez kell hozzá a http-k miatt*/
using System.Net.Http;


namespace API_with_csharp
{
    class Program
    {
        static string endPointUrl = "https://retoolapi.dev/KqpqJ9/data";

        /*id, name, salary   ezek a mezőnevek a generált APIból*/

        static void Main(string[] args)
        {
            restapiAdatok().Wait();
            foreach (Adat item in adatok)
            {
                Console.WriteLine($"{item.Id}. {item.Name}");
            }


            //legjobban kereső dolgozó meghívása lentről
            legjobbanKereso();

            Console.WriteLine("Program Vége!");
            Console.ReadLine();
        }


        /*legjobban kereső dolgozó nevét irassuk ki*/
        private static void legjobbanKereso()
        {
            long maxSalary = adatok.Max(a => a.Salary);
            Adat legmagasabb = adatok.Find(a => a.Salary == maxSalary);
            Console.WriteLine("1. feladat");
            Console.WriteLine($"\tA legjobban kereső dolgozó: {legmagasabb.Name}, a fizetése: {legmagasabb.Salary}");
        }

        static List<Adat> adatok = new List<Adat>();

        static async Task restapiAdatok()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, endPointUrl);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            
            /*Console.WriteLine(await response.Content.ReadAsStringAsync());*/

            string jsonString = await response.Content.ReadAsStringAsync();
            adatok = Adat.FromJson(jsonString).ToList();
        }
    }
}
