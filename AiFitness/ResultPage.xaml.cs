using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AiFitness
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    {
        public ResultPage(string result)
        {
            InitializeComponent();
            resultLabel.Text = result;
        }
    }
}
