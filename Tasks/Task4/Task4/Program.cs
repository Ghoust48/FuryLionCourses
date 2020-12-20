// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Task4
{
    public enum Cities
    {
        Vitebsk = 1,
        Berlyn,
        Minsk,
        Polatsk,
        Navapolatsk,
        Other
    }

    /// <summary>
    /// Хранилище ошибок
    /// </summary>
    [Serializable]
    public class ErrorMessage
    {
        [JsonProperty("cod")]
        public int StatusCode { get; set; }

        public string Message { get; set; }
    }

    /// <summary>
    /// Хранилище скорости ветра
    /// </summary>
    [Serializable]
    public class Wind
    {
        /// <summary>
        /// Скорость ветра
        /// </summary>
        public float Speed { get; set; }

    }

    /// <summary>
    /// Хранилище облачности
    /// </summary>
    [Serializable]
    public class Clouds
    {
        /// <summary>
        /// Облачность
        /// </summary>
        [JsonProperty("all")]
        public int Cloudiness { get; set; } // Облачность, %

    }

    /// <summary>
    /// Хранилище состояния погоды
    /// </summary>
    [Serializable]
    public class Weather
    {
        /// <summary>
        /// Состояния погоды
        /// </summary>
        public string Description { get; set; }
        
    }

    /// <summary>
    /// Хранилище главных погодых условий
    /// </summary>
    [Serializable]
    public class Main
    {
        /// <summary>
        /// Температура
        /// </summary>
        public float Temp { get; set; }

        [JsonProperty("temp_min")]
        public float TempMin { get; set; }

        [JsonProperty("temp_max")]
        public float TempMax { get; set; }

        /// <summary>
        /// Атмосферное давление на уровне моря
        /// </summary>
        public float Pressure { get; set; } // Атмосферное давление на уровне моря, по умолчанию, гПа

        /// <summary>
        /// Влажность воздуха
        /// </summary>
        public int Humidity { get; set; }    // Влажность, %
    }

    /// <summary>
    /// Хранилище информации о городе
    /// </summary>
    [Serializable]
    public class City
    {
        /// <summary>
        /// Наименование города
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Наименование страны, в которой находится город
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Насиление
        /// </summary>
        public long Population { get; set; }

        public override string ToString()
        {
            return $"Страна: {Country}\nГород: {Name}\nЧисленность насиления: {Population}";
        }
    }

    /// <summary>
    /// Структура, которая хранит в себе остальные структуры погодных условий
    /// </summary>
    [Serializable]
    public class List
    {
        public Main Main { get; set; }

        public Clouds Clouds { get; set; }

        public Wind Wind { get; set; }

        public Weather[] Weather { get; set; }

        [JsonProperty("dt_txt")]
        public string Date { get; set; }
    }

    /// <summary>
    /// Хранилище списка инфорации о погодных условиях
    /// </summary>
    [Serializable]
    public class ForecastData
    {
        public List[] List { get; set; }
        public City City { get; set; }
    }

    internal static class Program
    {
        private const string ApiKey = "3eecab8aa6e77e8487d78f63e9548847";
        private const string FilePath = "data.bin";
        private static ObservableDictionary<string, ForecastData> WeatherCache;

        private static void Main(string[] args)
        {
            WeatherCache = Storage.Load<ObservableDictionary<string, ForecastData>>(FilePath);
            WeatherCache.Changed += OnWeatherCacheChanged;
            
            int selectedMenu;
            
            do
            {
                Console.Clear();

                Console.WriteLine("1. Погода на сегодня.\n" +
                                  "2. Погода на 5 дней.\n" +
                                  "3. Вывести погоду из кэша\n" +
                                  "0. Выход");
                while (!ReadInt(out selectedMenu) || selectedMenu < 0 || selectedMenu > 3)
                    Console.Write("Ошибка ввода. Попробуйте еще раз: ");

                switch (selectedMenu)
                {
                    case 1:
                        {
                            Console.Clear();
                            ChoiceCity(PrintCurrentForecast);
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            ChoiceCity(PrintForecastForFiveDays);
                            break;
                        }
                    case 3:
                    {
                        Console.Clear();
                        ShowCache();
                        break;
                    }
                }

            } while (selectedMenu != 0);
        }
        
        private static void OnWeatherCacheChanged()
        {
            Storage.Save(FilePath, WeatherCache);
        }

        private static void ShowCache()
        {
            if (WeatherCache.Count != 0)
            {
                foreach (var value in WeatherCache.Values)
                    PrintCurrentForecast(value);
            }
            else
            {
                Console.WriteLine("Кэш пуст! Просмотрите погоду.");
            }
            
            Console.WriteLine("Чтобы продолжить нажмите любую клавишу...");
            Console.ReadKey();
        }

        private static void ChoiceCity(Action<ForecastData> received)
        {
            int selectedMenu;
            do
            {
                var cities = Enum.GetNames(typeof(Cities));

                for (var i = 0; i < cities.Length; i++)
                    Console.WriteLine($"{i + 1}. {cities[i]}");

                Console.WriteLine("0. Назад");

                while (!ReadInt(out selectedMenu) || selectedMenu < 0 || selectedMenu > cities.Length)
                    Console.Write("Ошибка ввода. Попробуйте еще раз: ");

                if (selectedMenu != 0)
                {
                    if ((Cities)selectedMenu != Cities.Other)
                        DownloadForecast(((Cities)selectedMenu).ToString(), received);
                    else
                    {
                        Console.Write("Введите название города: ");
                        DownloadForecast(Console.ReadLine(), received);
                    }
                }

            } while (selectedMenu != 0);
        }

        private static void DownloadForecast(string cityName, Action<ForecastData> received)
        {
            var url = $"http://api.openweathermap.org/data/2.5/forecast?q={cityName}&&units=metric&appid={ApiKey}";
            var webClient = new WebClient();

            try
            {
                Console.Clear();

                var response = webClient.DownloadString(url);
                var data = JsonConvert.DeserializeObject<ForecastData>(response);
                received?.Invoke(data);

                WeatherCache.Add(cityName, data);
            }
            catch (WebException webException)
            {
                if (webException.Response is HttpWebResponse response)
                {
                    string responseJson;

                    using (var reader = new StreamReader(response.GetResponseStream()))
                        responseJson = reader.ReadToEnd();

                    var error = JsonConvert.DeserializeObject<ErrorMessage>(responseJson);
                    Console.WriteLine("Возникла ошибка при обращении к сервису: " +
                                      $"{error.Message} ({(int)response.StatusCode} - {response.StatusDescription})");
                }
                else
                    Console.WriteLine("Проверьте подключение к интернету...\n");
            }

            Console.WriteLine("Чтобы продолжить нажмите любую клавишу...");
            Console.ReadKey();
            Console.Clear();
        }

        private static void PrintCurrentForecast(ForecastData forecastData)
        {
            Console.WriteLine(forecastData.City);
            
            CheckedForecast(0, forecastData);   
        }
        
        private static void PrintForecastForFiveDays(ForecastData forecastData)
        {
            Console.WriteLine(forecastData.City);
            
            for(var i = 0; i < forecastData.List.Length; i++)
                CheckedForecast(i, forecastData);
        }

        private static void CheckedForecast(int n, ForecastData forecastData)
        {
            var weather = forecastData.List[n].Weather[0].Description;
            SkyTranslation.ContainsKey(forecastData.List[n].Weather[0].Description);
            
            Console.Write(" ___________________________________________________\n");
            Console.Write($"|                {forecastData.List[n].Date}                |\n");
            Console.Write("|___________________________________________________|\n");
            var top = Console.CursorTop;
            Console.CursorLeft = 52; Console.Write("|");Console.CursorLeft = 16;
            Console.Write($"|Погода: {SkyTranslation[weather]}\n");Console.CursorLeft = 52; Console.Write("|");Console.CursorLeft = 16;
            Console.Write($"|Температура: {forecastData.List[n].Main.Temp} °C\n");Console.CursorLeft = 52;Console.Write("|");Console.CursorLeft = 16;
            Console.Write($"|Скорость ветра: {forecastData.List[n].Wind.Speed} м/с\n");Console.CursorLeft = 52;Console.Write("|");Console.CursorLeft = 16;
            Console.Write($"|Облачность: {forecastData.List[n].Clouds.Cloudiness} %\n");Console.CursorLeft = 52;Console.Write("|");Console.CursorLeft = 16;
            Console.Write($"|Атмосферное давление: {forecastData.List[n].Main.Pressure} гПа\n");Console.CursorLeft = 52;Console.Write("|");Console.CursorLeft = 16;
            Console.Write($"|Влажность: {forecastData.List[n].Main.Humidity} %\n");Console.CursorLeft = 52;Console.Write("|");Console.CursorLeft = 16;
            Console.Write("|___________________________________|\n");

            DrawForecast(top, weather);
        }

        private static void DrawForecast(int top, string weather)
        {
            if (weather == "few clouds")
            {
                Console.CursorTop = top++;Console.CursorLeft = 0; Console.Write("|               \n");Console.CursorLeft = 0;
                Console.CursorTop = top++;Console.CursorLeft = 0;Console.Write("|   \\  |        \n");Console.CursorLeft = 0;
                Console.CursorTop = top++;Console.CursorLeft = 0;Console.Write("|    .- .-.     \n");Console.CursorLeft = 0;
                Console.CursorTop = top++;Console.CursorLeft = 0;Console.Write("|-- (  (   )_   \n");Console.CursorLeft = 0;
                Console.CursorTop = top++;Console.CursorLeft = 0;Console.Write("|    '(___(__)  \n");Console.CursorLeft = 0;
                Console.CursorTop = top++;Console.CursorLeft = 0;Console.Write("|   /           \n");Console.CursorLeft = 0;
                Console.CursorTop = top;Console.CursorLeft = 0;Console.Write("|_______________\n");Console.CursorLeft = 0;
            }

            if (weather == "clear sky")
            {
                Console.CursorTop = top++;Console.CursorLeft = 0; Console.Write("|               \n");Console.CursorLeft = 0;
                Console.CursorTop = top++;Console.CursorLeft = 0;Console.Write("|   \\     /     \n");Console.CursorLeft = 0;
                Console.CursorTop = top++;Console.CursorLeft = 0;Console.Write("|     .-.       \n");Console.CursorLeft = 0;
                Console.CursorTop = top++;Console.CursorLeft = 0;Console.Write("| -- (   ) --   \n");Console.CursorLeft = 0;
                Console.CursorTop = top++;Console.CursorLeft = 0;Console.Write("|     '-'       \n");Console.CursorLeft = 0;
                Console.CursorTop = top++;Console.CursorLeft = 0;Console.Write("|    /   \\      \n");Console.CursorLeft = 0;
                Console.CursorTop = top++;Console.CursorLeft = 0;Console.Write("|_______________\n");Console.CursorLeft = 0;        
            }

            if (weather == "scattered clouds" || weather == "broken clouds" || weather == "mist" || 
                weather == "overcast clouds" || weather == "smoke" || weather == "haze" || weather == "fog")
            {
                Console.CursorTop = top++; Console.CursorLeft = 0; Console.Write("|               \n");Console.CursorLeft = 0;
                Console.CursorTop = top++; Console.CursorLeft = 0; Console.Write("|               \n");Console.CursorLeft = 0;
                Console.CursorTop = top++; Console.CursorLeft = 0; Console.Write("|     .-.       \n");Console.CursorLeft = 0;
                Console.CursorTop = top++; Console.CursorLeft = 0; Console.Write("|    (   )_     \n");Console.CursorLeft = 0;
                Console.CursorTop = top++; Console.CursorLeft = 0; Console.Write("|   (___(__)    \n");Console.CursorLeft = 0;
                Console.CursorTop = top++; Console.CursorLeft = 0; Console.Write("|               \n");Console.CursorLeft = 0;
                Console.CursorTop = top++; Console.CursorLeft = 0; Console.Write("|_______________\n");Console.CursorLeft = 0;
            }

            if (weather == "thunderstorm" || weather == "thunderstorm with light rain" ||
                weather == "thunderstorm with rain" || weather == "thunderstorm with heavy rain" ||
                weather == "light thunderstorm" || weather == "heavy thunderstorm" ||
                weather == "ragged thunderstorm" || weather == "thunderstorm with light drizzle" ||
                weather == "thunderstorm with drizzle" || weather == "thunderstorm with heavy drizzle")
            {
                Console.CursorTop = top++; Console.CursorLeft = 0; Console.Write("|               \n");Console.CursorLeft = 0;
                Console.CursorTop = top++; Console.CursorLeft = 0; Console.Write("|               \n");Console.CursorLeft = 0;
                Console.CursorTop = top++; Console.CursorLeft = 0; Console.Write("|     .-.       \n");Console.CursorLeft = 0;
                Console.CursorTop = top++; Console.CursorLeft = 0; Console.Write("|    (   )_     \n");Console.CursorLeft = 0;
                Console.CursorTop = top++; Console.CursorLeft = 0; Console.Write("|   (___(__)    \n");Console.CursorLeft = 0;
                Console.CursorTop = top++; Console.CursorLeft = 0; Console.Write("|   .'ϟ.'ϟ.'    \n");Console.CursorLeft = 0;
                Console.CursorTop = top++; Console.CursorLeft = 0; Console.Write("|_______________\n");Console.CursorLeft = 0;
            }

            if (weather == "rain" || weather == "drizzle" || weather == "heavy intensity drizzle" ||
                weather == "drizzle rain" || weather == "heavy intensity drizzle rain" ||
                weather == "shower rain and drizzle" || weather == "heavy shower rain and drizzle" ||
                weather == "shower drizzle" || weather == "light rain" || weather == "moderate rain" ||
                weather == "heavy intensity rain" || weather == "very heavy rain" || weather == "extreme rain" ||
                weather == "freezing rain" || weather == "light intensity shower rain" ||
                weather == "ragged shower rain")
            {
                Console.CursorTop = top++; Console.CursorLeft = 0; Console.Write("|               \n");Console.CursorLeft = 0;
                Console.CursorTop = top++; Console.CursorLeft = 0; Console.Write("|               \n");Console.CursorLeft = 0;
                Console.CursorTop = top++; Console.CursorLeft = 0; Console.Write("|     .-.       \n");Console.CursorLeft = 0;
                Console.CursorTop = top++; Console.CursorLeft = 0; Console.Write("|    (   )_     \n");Console.CursorLeft = 0;
                Console.CursorTop = top++; Console.CursorLeft = 0; Console.Write("|   (___(__)    \n");Console.CursorLeft = 0;
                Console.CursorTop = top++; Console.CursorLeft = 0; Console.Write("|   .'.'.'.'    \n");Console.CursorLeft = 0;
                Console.CursorTop = top++; Console.CursorLeft = 0; Console.Write("|_______________\n");Console.CursorLeft = 0;
            }
        }
        
        private static readonly Dictionary<string, string> SkyTranslation = new Dictionary<string, string>
        {
            {"clear sky", "Чистое небо"},
            {"few clouds", "Малооблачно"},
            {"scattered clouds", "Рассеяные облака"},
            {"broken clouds", "Облачно"},
            {"shower rain", "Маленький дождь"},
            {"rain", "Дождь"},
            {"thunderstorm", "Гроза"},
            {"snow", "Снег"},
            {"mist", "Туман"},
            {"thunderstorm with light rain", "Гроза с небольшим дождем"},
            {"thunderstorm with rain", "Гроза с дождем"},
            {"thunderstorm with heavy rain", "Гроза с проливным дождем"},
            {"light thunderstorm", "Легкая гроза"},
            {"heavy thunderstorm", "Сильная гроза"},
            {"ragged thunderstorm", "рваная гроза"},
            {"thunderstorm with light drizzle", "Гроза с легкой моросью"},
            {"thunderstorm with drizzle", "Гроза с моросящим дождём"},
            {"thunderstorm with heavy drizzle", "Гроза с сильным моросящим дождем"},
            {"light intensity drizzle", "Лёгкий моросящий дождь"},
            {"drizzle", "Мелкий дождь"},
            {"heavy intensity drizzle", "Сильная морось"},
            {"drizzle rain", "Моросящий дождь"},
            {"heavy intensity drizzle rain", "Сильный дождь"},
            {"shower rain and drizzle", "Дождь"},
            {"heavy shower rain and drizzle", "Ливень"},
            {"shower drizzle", "Моросящий дождь"},
            {"light rain", "Легкий дождь"},
            {"moderate rain", "Умеренный дождь"},
            {"heavy intensity rain", "Сильный дождь"},
            {"very heavy rain", "Очень сильный дождь"},
            {"extreme rain", "Сильный дождь"},
            {"freezing rain", "Ледяной"},
            {"light intensity shower rain", "Интенсивный дождь"},
            {"ragged shower rain", "Сильный дождь"},
            {"overcast clouds", "Пасмурно"},
            {"smoke", "Cмог"},
            {"haze", "Мгла"},
            {"fog", "Туман"}
        };

        private static bool ReadInt(out int value)
        {
            return int.TryParse(Console.ReadLine(), out value);
        }
    }
}