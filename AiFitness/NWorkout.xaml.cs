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
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace AiFitness
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

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
        // Словари для каждой кнопки, установка значения для дальнейшей передачи в API
        private Dictionary<string, string> timeOptions = new Dictionary<string, string>
        {
            { "10", "10 минут" },
            { "20", "20 минут" },
            { "30", "30 минут" },
            { "40", "40 минут" },
        };

        private Dictionary<string, string> muscleOptions = new Dictionary<string, string>
        {
            { "legs", "Ноги" },
            { "arms", "Руки" },
            { "chest", "Грудь" },
            { "full body", "Всё тело" },
            { "body", "Торс" },
        };

        private Dictionary<string, string> equipmentOptions = new Dictionary<string, string>
        {
            { "dumbbells", "Гантели" },
            { "barbell", "Штанга" },
            { "bodyweight", "Собственный вес" },
            // Добавьте другие варианты по вашему усмотрению
        };

        private Dictionary<string, string> fitnessLevelOptions = new Dictionary<string, string>
        {
            { "beginner", "Начинающий" },
            { "intermediate", "Средний уровень" },
            { "advanced", "Продвинутый уровень" },
            // Добавьте другие варианты по вашему усмотрению
        };

        private Dictionary<string, string> fitnessGoalsOptions = new Dictionary<string, string>
        {
            { "weight_loss", "Снижение веса" },
            { "muscle_gain", "Набор мышц" },
            { "strength_building", "Построение силы" },
            { "endurance", "Выносливость" },
        };

        // Инициализация выпадающих списков в конструкторе класса
        public NWorkout()
        {
            InitializeComponent();

            // Заполнение выпадающих списков данными
            timePicker.ItemsSource = timeOptions.Values.ToList();
            musclePicker.ItemsSource = muscleOptions.Values.ToList();
            equipmentPicker.ItemsSource = equipmentOptions.Values.ToList();
            fitnessLevelPicker.ItemsSource = fitnessLevelOptions.Values.ToList();
            fitnessGoalsPicker.ItemsSource = fitnessGoalsOptions.Values.ToList();
        }

        // Получение результата с введённых значений
        private async void Button_Result(object sender, EventArgs e)
        {
            string selectedTime = GetSelectedKey(timeOptions, timePicker.SelectedIndex);
            string selectedMuscle = GetSelectedKey(muscleOptions, musclePicker.SelectedIndex);
            string selectedEquipment = GetSelectedKey(equipmentOptions, equipmentPicker.SelectedIndex);
            string selectedFitnessLevel = GetSelectedKey(fitnessLevelOptions, fitnessLevelPicker.SelectedIndex);
            string selectedFitnessGoal = GetSelectedKey(fitnessGoalsOptions, fitnessGoalsPicker.SelectedIndex);

            if (string.IsNullOrEmpty(selectedTime) || string.IsNullOrEmpty(selectedMuscle) || string.IsNullOrEmpty(selectedEquipment) || string.IsNullOrEmpty(selectedFitnessLevel) || string.IsNullOrEmpty(selectedFitnessGoal))
            {
                await DisplayAlert("Ошибка!", "Пожалуйста, заполните все поля.", "Ок");
                return;
            }

            Root root = GetWorkoutData(selectedTime, selectedMuscle, selectedEquipment, selectedFitnessLevel, selectedFitnessGoal);

            if (root == null)
            {
                await DisplayAlert("Ошибка!", "Не удалось получить данные", "Ок");
                return;
            }

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

            // Перевод с английского на русский
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
            //Скролл в конец для выдачи полного результата, как временное решение, далее будет использован значок "загрузка"
            await scroll.ScrollToAsync(0, scroll.Height, true);
        }

        // Проверка выбора ключа из словарей
        private static string GetSelectedKey(Dictionary<string, string> options, int selectedIndex)
        {
            if (selectedIndex >= 0 && selectedIndex < options.Count)
            {
                return options.ElementAt(selectedIndex).Key;
            }

            return null;
        }

        //Получение данных с десеарилизацией
        private Root GetWorkoutData(string time, string muscle, string equipment, string fitnessLevel, string fitnessGoals)
        {
            string json = PostAndGet(time, muscle, equipment, fitnessLevel, fitnessGoals);

            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Root>(json);
        }

        //Перевод текста
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

        // Запрос в API Workout
        private string PostAndGet(string time, string muscle, string equipment, string fitness_level, string fitness_goals)
        {
            var client = new RestClient(string.Format("https://workout-planner1.p.rapidapi.com/customized?time={0}&muscle={1}&equipment={2}&fitness_level={3}&fitness_goals{4}", time, muscle, equipment, fitness_level, fitness_goals));
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

