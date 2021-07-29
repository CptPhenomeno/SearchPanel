using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace SearchPanel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestSearchPanel : PancakeView
    {
        public TestSearchPanel()
        {
            InitializeComponent();
        }
    }
}