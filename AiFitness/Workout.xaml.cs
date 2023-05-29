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
using AiFitness.Views;
using AiFitness.ViewModels;

namespace AiFitness
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Workout : ContentPage
    {
        public Workout()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WorkoutPage());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WorkoutPage2());
        }

        private void Seven_Day1Button(object sender, EventArgs e)
        {
            Navigation.PushAsync(new _7DaysWorkout1Page());
        }

        private void Seven_Day2Button(object sender, EventArgs e)
        {
            Navigation.PushAsync(new _7DaysWorkout2Page());
        }

        private void Seven_Day3Button(object sender, EventArgs e)
        {
            Navigation.PushAsync(new _7DaysWorkout3Page());
        }

        private void Seven_Day4Button(object sender, EventArgs e)
        {
            Navigation.PushAsync(new _7DaysWorkout4Page());
        }

        private void Seven_Day5Button(object sender, EventArgs e)
        {
            Navigation.PushAsync(new _7DaysWorkout5Page());
        }
        private void Seven_Day6Button(object sender, EventArgs e)
        {
            Navigation.PushAsync(new _7DaysWorkout6Page());
        }

        private void Seven_Day7Button(object sender, EventArgs e)
        {
            Navigation.PushAsync(new _7DaysWorkout7Page());
        }
    }
}