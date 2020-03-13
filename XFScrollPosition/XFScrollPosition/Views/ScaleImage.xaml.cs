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
    public partial class ScaleImage : ContentPage
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

        private readonly double _headerStartPanYPosition = 40;
        private readonly double _headerStartZoomYPosition = 70;
        private readonly uint _animateLength = 150;

        public ScaleImage()
        {
            InitializeComponent();
            this.BindingContext = this;

            var up = Observable.FromEventPattern<ScrolledEventArgs>(scrollView, nameof(scrollView.Scrolled));
            up.Zip(up.Skip(1),
                (prev, current) => new
                {
                    ScaleDelta = (current.EventArgs.ScrollY - prev.EventArgs.ScrollY) / 15,
                    CurrentY = current.EventArgs.ScrollY,
                    Direction = (current.EventArgs.ScrollY - prev.EventArgs.ScrollY) >= 0 ? ScrollDirection.Up : ScrollDirection.Down
                })
                .Where(x => x.CurrentY >= _headerStartPanYPosition && x.Direction == ScrollDirection.Up)
               .Subscribe(async x =>
                  {
                      double scale = imageFrame.Scale - x.ScaleDelta;

                      if (scale < 0.5)
                      {
                          scale = 0.5;
                      }
                      await imageFrame.ScaleTo(scale, _animateLength);

                      Message = $"up {imageFrame.Width.ToString()}";
                  });

            var down = Observable.FromEventPattern<ScrolledEventArgs>(scrollView, nameof(scrollView.Scrolled));
            down.Zip(down.Skip(1),
                (prev, current) => new
                {
                    ScaleDelta = (current.EventArgs.ScrollY - prev.EventArgs.ScrollY) /15,
                    CurrentY = current.EventArgs.ScrollY,
                    Direction = (current.EventArgs.ScrollY - prev.EventArgs.ScrollY) >= 0 ? ScrollDirection.Up : ScrollDirection.Down
                })
                .Where(x => x.CurrentY < _headerStartZoomYPosition && x.Direction == ScrollDirection.Down)
                .Subscribe(async x =>
                {
                    double scale = imageFrame.Scale - x.ScaleDelta;
                    if (scale > 1.0)
                    {
                        scale = 1.0;
                    }
                    if (x.CurrentY == 0)
                    {
                        scale = 1.0;
                    }
                    await imageFrame.ScaleTo(scale, _animateLength,Easing.CubicOut);
                    Message = $"down {scale.ToString()}";
                });
        }

        enum ScrollDirection
        {
            Up,
            Down,
        }
    }
}