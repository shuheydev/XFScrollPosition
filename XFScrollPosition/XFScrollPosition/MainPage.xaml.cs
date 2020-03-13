using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFScrollPosition.Views;

namespace XFScrollPosition
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FadeHeader());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SlideHeader());
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SlideHeader2());
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SlideHeader3());
        }

        private void Button_Clicked_4(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FadeHeader2());
        }

        private void Button_Clicked_5(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScaleImage());
        }
    }
}