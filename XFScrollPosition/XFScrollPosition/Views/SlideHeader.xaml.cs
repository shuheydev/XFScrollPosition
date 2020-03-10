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


        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            Message = e.ScrollY.ToString();

            SlideHeaderProcess(e.ScrollY);
        }

        private readonly double _slideToggleYPosition = 110;
        private void SlideHeaderProcess(double scrollYPosition)
        {
            if (scrollYPosition >= _slideToggleYPosition)
            {
                //指定した位置より多くスクロールした分だけ,配置位置から引く
                frame_Header.TranslationY = frame_Header.Y - (scrollYPosition - _slideToggleYPosition);
            }
            else
            {
                //素早く動かすと値が飛び飛びになって,もとの位置に戻らないのでここで調整
                frame_Header.TranslationY =frame_Header.Y;
            }
        }
    }
}