using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    // Ваш API ключ для OpenWeatherMap
    private static readonly string apiKey = "YOUR_API_KEY";

    static async Task Main(string[] args)
    {
        Console.Write("Введите название города: ");
        string city = Console.ReadLine();

        await GetWeatherAsync(city);
    }

    static async Task GetWeatherAsync(string city)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                string url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
                HttpResponseMessage response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(responseBody);

                Console.WriteLine($"Город: {weatherResponse.Name}");
                Console.WriteLine($"Температура: {weatherResponse.Main.Temp}°C");
                Console.WriteLine($"Погодные условия: {weatherResponse.Weather[0].Description}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nПроизошла ошибка при получении данных.");
                Console.WriteLine($"Сообщение: {e.Message}");
            }
        }
    }
}

public class WeatherResponse
{
    public Main Main { get; set; }
    public Weather[] Weather { get; set; }
    public string Name { get; set; }
}

public class Main
{
    public double Temp { get; set; }
}

public class Weather
{
    public string Description { get; set; }
}
