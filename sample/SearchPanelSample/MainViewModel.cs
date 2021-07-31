using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace SearchPanelSample
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _query;
        public string Query
        {
            get => this._query;
            set
            {
                if (string.Equals(this._query, value) is false)
                {
                    this._query = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ICommand TestCommand { get; private set; }

        public MainViewModel()
        {
            TestCommand = new Command(() => Application.Current.MainPage.DisplayAlert("Test command", $"I wrote: {this.Query}", "Cancel"));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
