using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace SearchPanel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestSearchPanel : PancakeView
    {
        private bool _isOpen;
        private Animation _openAnimation;
        private Animation _closeAnimation;
        private TapGestureRecognizer _searchTapGesture;

        private static Color _circleBoxDefaultColor = Color.White;
        
        #region SearchIconBackgroundColor
        public static BindableProperty SearchIconBackgroundColorProperty = BindableProperty.Create(
            nameof(SearchIconBackgroundColor),
            typeof(Color),
            typeof(TestSearchPanel),
            _circleBoxDefaultColor,
            propertyChanged: OnSearchIconBackgroundColorChanged);

        public Color SearchIconBackgroundColor
        {
            get => (Color)GetValue(SearchIconBackgroundColorProperty);
            set => SetValue(SearchIconBackgroundColorProperty, value);
        }

        private static void OnSearchIconBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is TestSearchPanel testSearchPanel &&
                newValue is Color newColor)
            {
                testSearchPanel.circleBox.Color = newColor;
            }
        }
        #endregion

        #region SearchCommand
        public static BindableProperty SearchCommandProperty = BindableProperty.Create(
            nameof(SearchCommand),
            typeof(ICommand),
            typeof(TestSearchPanel),
            propertyChanged: OnSearchCommandChanged);

        public ICommand SearchCommand
        {
            get => (ICommand)GetValue(SearchCommandProperty);
            set => SetValue(SearchCommandProperty, value);
        }

        private static void OnSearchCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is TestSearchPanel testSearchPanel &&
                newValue is ICommand newCommand)
            {
                testSearchPanel._searchTapGesture.Command = newCommand;
            }
        }
        #endregion

        public TestSearchPanel()
        {
            InitializeComponent();
            this._isOpen = false;

            this._openAnimation = new Animation(v => this.WidthRequest = v, 56, 360);
            this._closeAnimation = new Animation(v => this.WidthRequest = v, 360, 56);
            this.circleBox.Color = this.SearchIconBackgroundColor;
            this.circleBox.IsVisible = false;
            this._searchTapGesture = new TapGestureRecognizer
            {
                Command = this.SearchCommand
            };
            this.circleBox.GestureRecognizers.Add(this._searchTapGesture);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (this._isOpen)
            {
                this._closeAnimation.Commit(this, "CloseTestSearchPanel");
                this.circleBox.IsVisible = false;
                this._isOpen = false;
            }
            else
            {
                this._openAnimation.Commit(this, "OpenTestSearchPanel");
                this.circleBox.IsVisible = true;
                this._isOpen = true;
            }
        }
    }
}