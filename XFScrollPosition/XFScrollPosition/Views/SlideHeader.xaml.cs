using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFScrollPosition.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SlideHeader : ContentPage
    {
        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }
        public SlideHeader()
        {
            InitializeComponent();

            this.BindingContext = this;

            Message = scrollView.ScrollY.ToString();
        }


        private async void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            Message = $"Y:{e.ScrollY},TY:{frame_Header.TranslationY},SY:{e.ScrollY},S-T:{e.ScrollY - _slideToggleYPosition}";

            await SlideHeaderProcess(e.ScrollY);
        }

        private readonly double _slideToggleYPosition = 110;
        private async Task SlideHeaderProcess(double scrollYPosition)
        {
            if (scrollYPosition >= _slideToggleYPosition)
            {
                frame_Header.TranslationY = frame_Header.Y - (scrollYPosition - _slideToggleYPosition);
            }
            else
            {
                frame_Header.TranslationY = 0;
            }
        }
    }
}