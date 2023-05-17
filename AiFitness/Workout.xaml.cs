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

namespace AiFitness
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

   



    public partial class Workout : ContentPage
    {
        public Workout()
        {
            InitializeComponent();
        }

        void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WorkoutPage());
        }

        
    }
}