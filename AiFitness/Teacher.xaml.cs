using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AiFitness
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Teacher : ContentPage
    {
        public Teacher()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            string url = "https://aifitness.kz/trainer.html"; // Url тренеров
            Device.OpenUri(new Uri(url));
        }
    }
}