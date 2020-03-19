using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFScrollPosition.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SlideHeader3 : ContentPage
    {
        //動作確認用のメッセージ
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

        private readonly double _slideToggleYPosition = 110;
        public SlideHeader3()
        {
            InitializeComponent();
            this.BindingContext = this;

            //Scroll時のHeaderの動作を設定
            SetHeaderBehaviorByScroll();
        }

        #region Scrollに伴うHeader部分の表示動作の定義
        private void SetHeaderBehaviorByScroll()
        {
            //ヘッダーを隠す側の動作
            var hide = Observable.FromEventPattern<ScrolledEventArgs>(scrollView, nameof(scrollView.Scrolled));
            hide.Zip(hide.Skip(1), (prev, current) => CreateScrollInfo(current.EventArgs.ScrollY, prev.EventArgs.ScrollY))
                .Where(x => x.Direction == ScrollDirection.Up && x.CurrentY >= _slideToggleYPosition)
                .Repeat()
                .Subscribe(x =>
                {
                    double nextTransY = frame_Header.TranslationY - x.DeltaY;
                    //if (nextTransY < -(frame_Header.Height + frame_Header.Y))
                    //{
                    //    nextTransY = -(frame_Header.Height + frame_Header.Y);
                    //}
                    nextTransY = Math.Max(-(frame_Header.Height + frame_Header.Y), nextTransY);
                    frame_Header.TranslationY = nextTransY;

                    Message = $"{frame_Header.TranslationY} {x.Direction}";
                });

            //ヘッダーを表示する側の動作
            var show = Observable.FromEventPattern<ScrolledEventArgs>(scrollView, nameof(scrollView.Scrolled));
            show.Zip(show.Skip(1), (prev, current) => CreateScrollInfo(current.EventArgs.ScrollY, prev.EventArgs.ScrollY))
                .Where(x => x.Direction == ScrollDirection.Down)
                .Repeat()
                .Subscribe(x =>
                {
                    double nextTransY = frame_Header.TranslationY - x.DeltaY;
                    //if (nextTransY > 0)
                    //{
                    //    nextTransY = 0;
                    //}
                    nextTransY = Math.Min(0, nextTransY);
                    frame_Header.TranslationY = nextTransY;

                    Message = $"{frame_Header.TranslationY} {x.Direction}";
                });
        }

        private ScrollInfo CreateScrollInfo(double currentScrollY, double prevScrollY) =>
            new ScrollInfo
            {
                CurrentY = currentScrollY,
                DeltaY = currentScrollY - prevScrollY,
                Direction = (currentScrollY - prevScrollY) >= 0 ? ScrollDirection.Up : ScrollDirection.Down
            };


        internal class ScrollInfo
        {
            public double CurrentY { get; set; }
            public double DeltaY { get; set; }
            public ScrollDirection Direction { get; set; }
        }

        internal enum ScrollDirection
        {
            Up,
            Down
        }

        #endregion
    }
}