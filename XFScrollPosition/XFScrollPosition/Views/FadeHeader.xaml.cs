using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFScrollPosition.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class FadeHeader : ContentPage
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

        public FadeHeader()
        {
            InitializeComponent();

            this.BindingContext = this;

            Message = scrollView.ScrollY.ToString();
            frame_Header.Opacity = 0;
        }


        private async void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            Message = e.ScrollY.ToString();

            await ToggleHeaderVisibility(e.ScrollY);
        }

        private readonly double _headerToggleYPosition = 100;
        private readonly uint _animateLength = 200;
        private bool _isHeaderVisible=false;
        private async Task ToggleHeaderVisibility(double scrollYPosition)
        {
            bool prevHeaderVisible = _isHeaderVisible;
            if (scrollYPosition >= _headerToggleYPosition)
            {
                _isHeaderVisible = true;
            }
            else
            {
                _isHeaderVisible = false;
            }

            if (_isHeaderVisible == prevHeaderVisible)
            {
                return;
            }

            if (_isHeaderVisible)
            {
                await frame_Header.FadeTo(1.0, _animateLength);
            }
            else
            {
                await frame_Header.FadeTo(0, _animateLength);
            }
        }
    }
}
