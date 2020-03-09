using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFScrollPosition
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private string _message;
        public string Message { get=>_message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }
        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = this;

            Message = scrollView.ScrollY.ToString();
        }


        private readonly double _headerVisibleYPosition = 100;
        private bool _isHeaderVisible = true;
        private async void scrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            Message = e.ScrollY.ToString();

            bool prevHeaderVisible=_isHeaderVisible;
            if (e.ScrollY>_headerVisibleYPosition)
            {
                _isHeaderVisible = false;
            }
            else
            {
                _isHeaderVisible = true;
            }

            if(_isHeaderVisible==prevHeaderVisible)
            {
                return;
            }

            if(_isHeaderVisible)
            {
                await boxView_Header.FadeTo(1.0, 200);
            }
            else
            {
                await boxView_Header.FadeTo(0, 200);
            }
        }
    }
}
