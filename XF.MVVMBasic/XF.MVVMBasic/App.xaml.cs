using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.MVVMBasic.ViewModel;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XF.MVVMBasic
{
    public partial class App : Application
    {
        public static AlunoViewModel AlunoVM { get; set; }

        public App()
        {
            InitializeComponent();
            if (AlunoVM == null) AlunoVM = new AlunoViewModel();
            MainPage = new NavigationPage(new View.AlunoView() { BindingContext = App.AlunoVM });
        }
    }
}
