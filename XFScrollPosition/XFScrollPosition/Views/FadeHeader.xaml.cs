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

        //表示,非表示が切り替わる位置
        private readonly double _headerToggleYPosition = 100;
        //アニメーションにかける時間.
        private readonly uint _animateLength = 200;
        //ナビゲーションバーの表示状態を保持
        private bool _isHeaderVisible = false;
        private async Task ToggleHeaderVisibility(double scrollYPosition)
        {
            bool prevIsHeaderVisible = _isHeaderVisible;
            //if (scrollYPosition >= _headerToggleYPosition)
            //{
            //    //指定位置までスクロールアップしたら,ナビゲーションバーを表示するフラグOn
            //    _isHeaderVisible = true;
            //}
            //else
            //{
            //    //スクロールダウンでフラグOff
            //    _isHeaderVisible = false;
            //}

            _isHeaderVisible = scrollYPosition >= _headerToggleYPosition;

            //指定位置をまたがない場合,アニメーションの必要は無いのでここまで.
            if (_isHeaderVisible == prevIsHeaderVisible)
            {
                return;
            }

            ////ナビゲーションバーの表示･非表示のアニメーション
            //if (_isHeaderVisible)
            //{
            //    await frame_Header.FadeTo(1.0, _animateLength);
            //}
            //else
            //{
            //    await frame_Header.FadeTo(0, _animateLength);
            //}

            var result=_isHeaderVisible ? (await frame_Header.FadeTo(1.0, _animateLength)) : (await frame_Header.FadeTo(0, _animateLength));
        }
    }
}
