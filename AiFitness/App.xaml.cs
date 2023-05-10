using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AiFitness
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage (new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            if (Device.RuntimePlatform == Device.Android)
            {
                // Остановить работу приложения при переходе в фоновый режим на Android
                Process.GetCurrentProcess().Kill();
            }

        }

        protected override void OnResume()
        {
            
        }
    }
}
