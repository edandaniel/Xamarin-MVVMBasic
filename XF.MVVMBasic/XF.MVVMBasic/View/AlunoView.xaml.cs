using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.MVVMBasic.Model;
using XF.MVVMBasic.ViewModel;

namespace XF.MVVMBasic.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AlunoView : ContentPage
	{
        public AlunoView()
        {
            InitializeComponent();
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            var AlunoVM = BindingContext as AlunoViewModel;
            var button = ((MenuItem)sender);
            var aluno = button?.BindingContext as Aluno;
            AlunoVM?.OnDelete.Execute(aluno);
        }

    }
}