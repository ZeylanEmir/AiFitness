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
	public partial class Train : ContentPage
	{
		public Train ()
		{
			InitializeComponent ();

        }

        async void Button_Clicked(object sender, EventArgs e)
        {
			await Navigation.PopModalAsync();

        }

        
    }
}