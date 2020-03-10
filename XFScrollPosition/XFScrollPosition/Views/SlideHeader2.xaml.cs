using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFScrollPosition.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SlideHeader2 : ContentPage
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
        public SlideHeader2()
        {
            InitializeComponent();

            this.BindingContext = this;
            Message = scrollView.ScrollY.ToString();
        }


        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            SlideHeaderProcess(e.ScrollY);
        }

        private readonly double _slideToggleYPosition_Up = 110;
        private double _slideToggleYPosition_Down = 0;

        private double _prevScrollYPosition = 0;
        private ScrollDirection _prevDirection = ScrollDirection.Down;

        private void SlideHeaderProcess(double scrollYPosition)
        {
            ScrollDirection scrlDirection = (scrollYPosition - _prevScrollYPosition) >= 0 ? ScrollDirection.Up : ScrollDirection.Down;

            //スクロールの方向が下方向に変わった
            if (scrlDirection != _prevDirection && scrlDirection == ScrollDirection.Down)
            {
                _slideToggleYPosition_Down = scrollYPosition;
            }

            //上方向にスクロールしている時
            if (scrlDirection == ScrollDirection.Up)
            {
                //上方向にスクロールするときは決まった位置から
                //ナビゲーションバーを隠す処理を開始
                if (scrollYPosition >= _slideToggleYPosition_Up)
                {
                    //前回のスクロール位置との差を計算する
                    double deltaY = Math.Abs(scrollYPosition - _prevScrollYPosition);
                    //次のTranslationYの値を計算する
                    double nextTransY = frame_Header.TranslationY - deltaY;

                    //次のTranslationYの値とナビゲーションバーのコントロールの高さを比較する.
                    //TranslationYの変化はナビゲーションバーの高さと開始位置を足したものちょうど
                    if (nextTransY < -(frame_Header.Height + frame_Header.Y))
                    {
                        nextTransY = -(frame_Header.Height + frame_Header.Y);
                    }

                    frame_Header.TranslationY = nextTransY;
                    Message = frame_Header.TranslationY.ToString();
                }
            }

            //スクロールの方向が引き続き下方向
            if (scrlDirection == ScrollDirection.Down)
            {
                double deltaY = Math.Abs(scrollYPosition - _prevScrollYPosition);
                double nextTransY = frame_Header.TranslationY + deltaY;

                if (nextTransY > 0)
                {
                    nextTransY = 0;
                }

                frame_Header.TranslationY = nextTransY;

                Message = frame_Header.TranslationY.ToString();
            }

            //スクロール方向を記録
            _prevDirection = scrlDirection;
            //スクロール位置を記録
            _prevScrollYPosition = scrollYPosition;
        }
    }

    enum ScrollDirection
    {
        Up,
        Down,
    }
}