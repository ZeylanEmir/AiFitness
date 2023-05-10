using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AiFitness
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class CoolDown
    {
        public string Exercise { get; set; }
        public string Time { get; set; }
    }

    public class Exercises
    {
        public string Exercise { get; set; }
        public string Sets { get; set; }
        public string Reps { get; set; }
    }

    public class Root
    {
        [JsonProperty("Warm Up")]
        public List<WarmUp> WarmUp { get; set; }

        [JsonProperty("Exercises")]
        public List<Exercises> Exercises { get; set; }

        [JsonProperty("Cool Down")]
        public List<CoolDown> CoolDown { get; set; }


    }

    public class WarmUp
    {
        public string Exercise { get; set; }
        public string Time { get; set; }
    }

    public partial class NWorkout : ContentPage
    {
        public NWorkout()
        {
            InitializeComponent();
        }
        

        // Выдача результата с Api Workout Planner через обработчик событий
        private async void Button_Result(object sender, EventArgs e)
        {


            // получение данных в формате Json и проверка на получение всех результатов
            Root root = null;
            await Task.Run(() =>
            {
                string json = PostAndGet(time.Text, location.Text, muscle.Text, equipment.Text);

                if (json == null)
                {
                    DisplayAlert("АШИБКА О ГОСПАДЕ!!!", "Нет данных", "OK");
                }

                root = JsonConvert.DeserializeObject<Root>(json);
            });

            // Асинхронный метод для разделение потока UI и данных
            Device.BeginInvokeOnMainThread(async () =>
            {

                // Десериализация JSON формата в строковый C# формат
                StringBuilder warmUpBuilder = new StringBuilder();
                StringBuilder coolDownBuilder = new StringBuilder();
                StringBuilder exercisesBuilder = new StringBuilder();

                //Форматирование строк
                foreach (WarmUp exercise in root.WarmUp)
                {
                    warmUpBuilder.AppendFormat("{0}: {1}\n", exercise.Exercise, exercise.Time);
                }

                foreach (Exercises exercise in root.Exercises)
                {
                    exercisesBuilder.AppendFormat("\nУпражнение: {0}\n Подходов: {1} \n Повторений: {2} \n", exercise.Exercise, exercise.Sets, exercise.Reps);
                }

                foreach (CoolDown exercise in root.CoolDown)
                {
                    coolDownBuilder.AppendFormat("{0}: {1}\n", exercise.Exercise, exercise.Time);
                }

                string langFrom = "en";
                string langTo = "ru";
                string warmUp = warmUpBuilder.ToString();
                string coolDown = coolDownBuilder.ToString();
                string exercises = exercisesBuilder.ToString();

                string warmUpTranslation = await TranslateText(warmUp, langFrom, langTo);
                string coolDownTranslation = await TranslateText(coolDown, langFrom, langTo);
                string exercisesTranslation = await TranslateText(exercises, langFrom, langTo);

                //Подстановка строк
                string data = string.Format("Разогрев:\n{0}\n\nТренировка:{1}\n\nЗавершение:\n{2}", warmUpTranslation.ToString(), exercisesTranslation.ToString(), coolDownTranslation.ToString());

                //Выдача результатов
                result.Text = data;

                //Скролл в конец для выдачи полного результата
                await scroll.ScrollToAsync(0, scroll.Height, true);
            });
        }
        private static async Task<string> TranslateText(string text, string langFrom, string langTo)
        {
            string result = string.Empty;
            string subscriptionKey = "d33fdee0e7c240cdbdfd6b90dd6a8e81";
            string endpoint = "https://api.cognitive.microsofttranslator.com";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            string route = $"/translate?api-version=3.0&from={langFrom}&to={langTo}";
            string requestBody = $@"[{{""Text"":""{text}""}}]";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = new StringContent(requestBody, Encoding.UTF8, "application/json"),
                RequestUri = new Uri(endpoint + route)
            };
            var response = await client.SendAsync(request).ConfigureAwait(false);
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                dynamic data = JsonConvert.DeserializeObject(responseBody);
                result = data[0].translations[0].text;
            }
            else
            {
                Console.WriteLine($"Error translating text: {responseBody}");
            }

            return result;
        }

        //Подключение API Workout Planner с тренировками
        private string PostAndGet(string time, string muscle, string location, string equipment)
        {
            var client = new RestClient(string.Format("https://workout-planner1.p.rapidapi.com/?time={0}&muscle={1}&location={2}&equipment={3}", time, muscle, location, equipment));
            var request = new RestRequest(Method.GET);

            request.AddHeader("content-type", "application/octet-stream");
            request.AddHeader("X-RapidAPI-Key", "707b4faf3bmsha3a4fe50b480a97p1ebd03jsn03b14a40e9d4");
            request.AddHeader("X-RapidAPI-Host", "workout-planner1.p.rapidapi.com");
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                return response.Content;
            }

            return string.Empty;

        }
    }
}