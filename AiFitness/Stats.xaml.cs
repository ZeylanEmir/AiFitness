using AiFitness.ViewModels;
using Microcharts;
using Microcharts.Forms;
using Newtonsoft.Json;
using RestSharp;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AiFitness
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class Data1
    {
        public string burnedCalorie { get; set; }
        public string unit { get; set; }
    }

    public class Root1
    {
        public int status_code { get; set; }
        public string request_result { get; set; }
        public Data1 data { get; set; }
    }

    public partial class Stats : ContentPage
    {
        // Словари для калькулятора сжённых калорий
        private Dictionary<string, string> activityOptions = new Dictionary<string, string>
        {
            { "ho_45", "Домашняя уборка"},
            { "co_46", "Лёгкая домашняя тренировка, йога"},
            { "ho_69", "Прогулка, лёгкий бег по парку"},
            { "co_47", "Средняя домашняя тренировка"},
            { "ru_4", "Бег с трусцой средний"},
            { "co_25", "Тренировки в спортзале, силовые упражнения"},
            { "sp_114", "Игра в волейбол"},
            { "ru_21", "Бег"},
            { "ru_2", "Бег с трусцой в полную силу"},
            { "sp_121", "Лёгкая атлетика"},
            { "co_12", "Занятия на велотренажёре, велосипед"},
        };

        private Dictionary<string, string> activityTimeOptions = new Dictionary<string, string>
        {
            { "10", "10 минут"},
            { "20", "20 минут"},
            { "30", "30 минут"},
            { "40", "40 минут"},
            { "50", "50 минут"},
            { "60", "60 минут"},
            { "70", "70 минут"},
            { "80", "80 минут"},
            { "90", "90 минут"},
            { "100", "100 минут"},
            { "110", "110 минут"},
            { "120", "120 минут"},
        };

        private Dictionary<string, string> weightOptions = new Dictionary<string, string>
        {
            { "40", "40 кг"},
            { "45", "45 кг"},
            { "50", "50 кг"},
            { "55", "55 кг"},
            { "60", "60 кг"},
            { "65", "65 кг"},
            { "70", "70 кг"},
            { "75", "75 кг"},
            { "80", "80 кг"},
            { "85", "85 кг"},
            { "90", "90 кг"},
            { "95", "95 кг"},
            { "100", "100 кг"},
            { "110", "110 кг"},
            { "120", "120 кг"},
            { "130", "130 кг"},
            { "140", "140 кг"},
            { "150", "150 кг"},
            { "160", "160 кг"},
        };

        // Словари для BMI
        private Dictionary<string, string> heightOptions = new Dictionary<string, string>
        {
            { "150", "150 см" },
            { "155", "155 см" },
            { "160", "160 см" },
            { "165", "165 см" },
            { "170", "170 см" },
            { "175", "175 см" },
            { "180", "180 см" },
            { "185", "185 см" },
            { "190", "190 см" },
            { "195", "195 см" },
            { "200", "200 см" }
        };

        private const string UnderweightCategory = "Недостаточный вес";
        private const string NormalWeightCategory = "Нормальный вес";
        private const string OverweightCategory = "Избыточный вес";
        private const string ObesityCategory1 = "Ожирение I степени";
        private const string ObesityCategory2 = "Ожирение II степени";
        private const string ObesityCategory3 = "Тяжёлая стадия ожирения, срочно обратитесь к врачу!";

        private Dictionary<string, string> bmiCategories = new Dictionary<string, string>
        {
            { UnderweightCategory, "Недостаточный вес" },
            { NormalWeightCategory, "Нормальный вес" },
            { OverweightCategory, "Избыточный вес" },
            { ObesityCategory1, "Ожирение I степени" },
            { ObesityCategory2, "Ожирение II степени" },
            { ObesityCategory3, "Тяжёлая стадия ожирения, срочно обратитесь к врачу!" },
        };



        //конструктор класса с инциализацией словаря и picker
        public Stats()
        {
            InitializeComponent();

            activityidPicker.ItemsSource = activityOptions.Values.ToList();
            activityminPicker.ItemsSource = activityTimeOptions.Values.ToList();
            weightPicker.ItemsSource = weightOptions.Values.ToList();
            weightPickerBMI.ItemsSource = weightOptions.Values.ToList();
            heightPicker.ItemsSource = heightOptions.Values.ToList();

            weightPickerIdeal.ItemsSource = weightOptions.Values.ToList();
            heightPickerIdeal.ItemsSource = heightOptions.Values.ToList();
        }

        // Выдача результата калькуляторе сожжённых калорий
        private async void Button_Clicked(object sender, EventArgs e)
        {
            string selectedActivity = GetSelectedKey(activityOptions, activityidPicker.SelectedIndex);
            string selectedActivityTime = GetSelectedKey(activityTimeOptions, activityminPicker.SelectedIndex);
            string selectedWeight = GetSelectedKey(weightOptions, weightPicker.SelectedIndex);

            Root1 root1 = GetCalorieData(selectedActivity, selectedActivityTime, selectedWeight);

            if (root1 == null)
            {
                await DisplayAlert("Ошибка!", "Не удалось получить данные, повторите попытку или заполните данные", "Ok");
            }
            else
            {
                string data = string.Format("Сожжено калорий за активность: {0:F2} Калорий", root1.data.burnedCalorie);
                result.Text = data;
            }
        }

        // Настройка выбора из Picker
        private static string GetSelectedKey(Dictionary<string, string> options, int selectedIndex)
        {
            if (selectedIndex >= 0 && selectedIndex < options.Count)
            {
                return options.ElementAt(selectedIndex).Key;
            }
            return null;
        }

        //Передача данных с десериализацией
        private Root1 GetCalorieData(string activityid, string activitymin, string weight)
        {
            string json = PostAndGet(activityid, activitymin, weight);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<Root1>(json);
        }

        // API калькулятора сожжённых калорий
        private string PostAndGet(string activityid, string activitymin, string weight)
        {
            var client = new RestClient($"https://fitness-calculator.p.rapidapi.com/burnedcalorie?activityid={activityid}&activitymin={activitymin}&weight={weight}");
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-RapidAPI-Key", "707b4faf3bmsha3a4fe50b480a97p1ebd03jsn03b14a40e9d4");
            request.AddHeader("X-RapidAPI-Host", "fitness-calculator.p.rapidapi.com");
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                return response.Content;
            }
            return string.Empty;
        }

        private void CalculateBMI_Clicked(object sender, EventArgs e)
        {
            string selectedWeight = GetSelectedKey(weightOptions, weightPickerBMI.SelectedIndex);
            string selectedHeight = GetSelectedKey(heightOptions, heightPicker.SelectedIndex);

            double weight = double.Parse(selectedWeight);
            double height = double.Parse(selectedHeight) / 100; // Переводим рост в метры

            double bmi = CalculateBMI(weight, height);

            string resultText = string.Format("Индекс массы тела (BMI): {0:F1}\n\n", bmi);

            if (bmi < 18.5)
            {
                resultText += string.Format("Ваш BMI {0:F1}, вы относитесь к категории: {1}", bmi, bmiCategories[UnderweightCategory]);
            }
            else if (bmi >= 18.5 && bmi < 25)
            {
                resultText += string.Format("Ваш BMI {0:F1}, вы относитесь к категории: {1}", bmi, bmiCategories[NormalWeightCategory]);
            }
            else if (bmi >= 25 && bmi < 30)
            {
                resultText += string.Format("Ваш BMI {0:F1}, вы относитесь к категории: {1}", bmi, bmiCategories[OverweightCategory]);
            }
            else if (bmi >= 30 && bmi < 35)
            {
                resultText += string.Format("Ваш BMI {0:F1}, вы относитесь к категории: {1}", bmi, bmiCategories[ObesityCategory1]);
            }
            else if (bmi >= 35 && bmi < 40)
            {
                resultText += string.Format("Ваш BMI {0:F1}, вы относитесь к категории: {1}", bmi, bmiCategories[ObesityCategory2]);
            }
            else
            {
                resultText += string.Format("Ваш BMI {0:F1}, вы относитесь к категории: {1}", bmi, bmiCategories[ObesityCategory3]);
            }

            bmiResult.Text = resultText;
        }

        private double CalculateBMI(double weight, double height)
        {
            // Формула расчета индекса массы тела (BMI)
            return weight / (height * height);
        }


        //Калькулятор идеального веса
        private void CalculateIdealWeight_Clicked(object sender, EventArgs e)
        {
            string selectedWeight = GetSelectedKey(weightOptions, weightPickerIdeal.SelectedIndex);
            string selectedHeight = GetSelectedKey(heightOptions, heightPickerIdeal.SelectedIndex);

            double weight = double.Parse(selectedWeight);
            double height = double.Parse(selectedHeight) / 100; // Переводим рост в метры

            double idealWeight = CalculateIdealWeight(height);

            string resultText = string.Format("Идеальный вес для вашего роста ({0} см): {1:F1} кг", selectedHeight, idealWeight);
            idealWeightResult.Text = resultText;
        }

        private double CalculateIdealWeight(double height)
        {
            // Формула расчета идеального веса (например, ИМТ 22.5)
            double idealBMI = 22.5;
            return idealBMI * (height * height);
        }


    }
}
