using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFScrollPosition.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FadeHeader2 : ContentPage
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

        private readonly double _headerToggleYPosition = 100;
        private readonly uint _animateLength = 200;

        public FadeHeader2()
        {
            InitializeComponent();
            this.BindingContext = this;

            Message = scrollView.ScrollY.ToString();
            frame_Header.Opacity = 0;

            var fadeIn = Observable.FromEventPattern<ScrolledEventArgs>(scrollView, nameof(scrollView.Scrolled));
            fadeIn.Where(x => x.EventArgs.ScrollY >= _headerToggleYPosition)
                .Subscribe(x =>
                {
                    frame_Header.FadeTo(1.0, _animateLength);
                    Message = x.EventArgs.ScrollY.ToString();
                });

            var fadeOut = Observable.FromEventPattern<ScrolledEventArgs>(scrollView, nameof(scrollView.Scrolled));
            fadeOut.Where(x => x.EventArgs.ScrollY < _headerToggleYPosition)
                .Subscribe(x =>
                {
                    frame_Header.FadeTo(0.0, _animateLength);
                    Message = x.EventArgs.ScrollY.ToString();
                });
        }
    }
}