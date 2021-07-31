using System.Windows.Input;
using Xamarin.Forms;

namespace SearchPanelSample
{
    public class MainViewModel
    {
        public ICommand TestCommand { get; private set; }

        public MainViewModel()
        {
            TestCommand = new Command(() => Application.Current.MainPage.DisplayAlert("Title", "Test command", "Cancel"));
        }
    }
}
