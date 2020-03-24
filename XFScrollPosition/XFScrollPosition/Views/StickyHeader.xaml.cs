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
    public partial class StickyHeader : ContentPage
    {
        public StickyHeader()
        {
            InitializeComponent();

            this.BindingContext = this;
        }

        private void OnRootScrollViewScrolled(object sender, ScrolledEventArgs e)
        {
            //Sticky Header
            var position = EmptyLayout.Height + Math.Max(0, RootScrollView.ScrollY - EmptyLayout.Height);
            AbsoluteLayout.SetLayoutBounds(TabsLayout, new Rectangle(0, position, 1, TabsLayout.Height));
            
            var opacity = Math.Max(0, Math.Min(RootScrollView.ScrollY, EmptyLayout.Height)) / EmptyLayout.Height - 0.1;
            ImageOverlay.Opacity = Math.Min(1, Math.Max(0, opacity));
            EmptyLayout.Opacity = 1 - ImageOverlay.Opacity;
        }

        private StackLayout _prevSelectedStackLayout;
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var currentSelected = sender as StackLayout;
            if (_prevSelectedStackLayout != null)
            {
                VisualStateManager.GoToState(_prevSelectedStackLayout, "Unselected");
            }

            VisualStateManager.GoToState(currentSelected, "Selected");
            _prevSelectedStackLayout = currentSelected;
        }
    }
}