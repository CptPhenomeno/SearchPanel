using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

[assembly:ExportFont("MaterialDesignIcons.ttf", Alias = "Icons")]
namespace SearchPanel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchBar : PancakeView
    {
        private bool _isOpen;
        private Animation _openAnimation;
        private Animation _closeAnimation;
        private TapGestureRecognizer _searchTapGesture;
        private Command _internalCommand;

        private static Color _searchIconBackgroundDefaultColor = Color.White;
        private static Color _searchIconDefaultColor = Color.DarkGray;
        private static Color _closePanelIconDefaultColor = Color.LightGray;
        private static Color _textDefaultColor = Color.LightGray;

        #region SearchIconBackgroundColor
        public static BindableProperty SearchIconBackgroundColorProperty = BindableProperty.Create(
            nameof(SearchIconBackgroundColor),
            typeof(Color),
            typeof(SearchBar),
            _searchIconBackgroundDefaultColor,
            propertyChanged: OnSearchIconBackgroundColorChanged);

        public Color SearchIconBackgroundColor
        {
            get => (Color)GetValue(SearchIconBackgroundColorProperty);
            set => SetValue(SearchIconBackgroundColorProperty, value);
        }

        private static void OnSearchIconBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SearchBar searchBar &&
                newValue is Color newColor)
            {
                searchBar.CircleBox.Color = newColor;
            }
        }
        #endregion

        #region SearchIconColor
        public static BindableProperty SearchIconColorProperty = BindableProperty.Create(
            nameof(SearchIconColor),
            typeof(Color),
            typeof(SearchBar),
            _searchIconDefaultColor,
            propertyChanged: OnSearchIconColorChanged);

        public Color SearchIconColor
        {
            get => (Color)GetValue(SearchIconColorProperty);
            set => SetValue(SearchIconColorProperty, value);
        }

        private static void OnSearchIconColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SearchBar searchBar &&
                newValue is Color newColor)
            {
                searchBar.SearchIcon.TextColor = newColor;
            }
        }
        #endregion

        #region SearchCommand
        public static BindableProperty SearchCommandProperty = BindableProperty.Create(
            nameof(SearchCommand),
            typeof(ICommand),
            typeof(SearchBar));

        public ICommand SearchCommand
        {
            get => (ICommand)GetValue(SearchCommandProperty);
            set => SetValue(SearchCommandProperty, value);
        }
        #endregion

        #region ClosePanelIconColor
        public static BindableProperty ClosePanelIconColorProperty = BindableProperty.Create(
            nameof(ClosePanelIconColor),
            typeof(Color),
            typeof(SearchBar),
            _closePanelIconDefaultColor,
            propertyChanged: OnClosePanelIconColorChanged);

        public Color ClosePanelIconColor
        {
            get => (Color)GetValue(ClosePanelIconColorProperty);
            set => SetValue(ClosePanelIconColorProperty, value);
        }

        private static void OnClosePanelIconColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SearchBar searchBar &&
                newValue is Color newColor)
            {
                searchBar.ClosePanelIcon.TextColor = newColor;
            }
        }
        #endregion

        #region Text
        public static BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(SearchBar),
            default(string),
            BindingMode.TwoWay);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        #endregion

        #region TextColor
        public static BindableProperty TextColorProperty = BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(SearchBar),
            _textDefaultColor);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }
        #endregion

        public SearchBar()
        {
            InitializeComponent();

            var openPanelTapGesture = new TapGestureRecognizer();
            var closePanelTapGesture = new TapGestureRecognizer();
            openPanelTapGesture.Tapped += OnMainPanelTapped;
            closePanelTapGesture.Tapped += OnClosePanelIconTapped;

            this._isOpen = false;
            this._openAnimation = new Animation(v => this.WidthRequest = v, 56, 360);
            this._closeAnimation = new Animation(v => this.WidthRequest = v, 360, 56);
            this._internalCommand = new Command(() =>
            {
                if (this.SearchCommand?.CanExecute(null) ?? false)
                {
                    this.SearchCommand.Execute(null);
                    CloseAction();
                }
            });
            this._searchTapGesture = new TapGestureRecognizer
            {
                Command = this._internalCommand
            };

            this.CircleBox.Color = this.SearchIconBackgroundColor;
            this.CircleBox.IsVisible = false;
            this.CircleBox.GestureRecognizers.Add(this._searchTapGesture);
            this.MainPanel.GestureRecognizers.Add(openPanelTapGesture);
            this.ClosePanelIcon.GestureRecognizers.Add(closePanelTapGesture);
            this.SearchText.BindingContext = this;
            this.SearchText.SetBinding(Entry.TextProperty, SearchBar.TextProperty.PropertyName, BindingMode.TwoWay);
            this.SearchText.SetBinding(Entry.TextColorProperty, SearchBar.TextColorProperty.PropertyName, BindingMode.TwoWay);
        }

        private void OnMainPanelTapped(object sender, EventArgs e)
        {
            if (this._isOpen is false)
                OpenAction();
        }

        private void OnClosePanelIconTapped(object sender, EventArgs e)
        {
            if (this._isOpen is true)
                CloseAction();
        }

        private void CloseAction()
        {
            this.SearchText.Unfocus();
            this.Text = null;
            this._closeAnimation.Commit(this, "CloseSearchBar");
            this.CircleBox.IsVisible = false;
            this._isOpen = false;
        }

        private void OpenAction()
        {
            this._openAnimation.Commit(this, "OpenSearchBar");
            this.CircleBox.IsVisible = true;
            this._isOpen = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(50);
                this.SearchText.Focus();
            });
        }
    }
}