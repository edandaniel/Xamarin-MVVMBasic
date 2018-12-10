using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XF.MVVMBasic.Model;

namespace XF.MVVMBasic.ViewModel
{
    public class AlunoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnAlterarPropriedade([CallerMemberName] string propriedade = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propriedade));
        }
        #region Propriedades
        public string RM { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public ICommand OnAddAluno { get; set; }
        public ICommand OnNovoAluno { get; set; }
        public Command<Aluno> OnDelete
        {
            get
            {
                return new Command<Aluno>(aluno =>
                {
                    Alunos.Remove(aluno);
                });
            }
        }
        private ObservableCollection<Aluno> items;
        public ObservableCollection<Aluno> Alunos
        {
            get { return items; }
            set
            {
                items = value;
            }
        }
        #endregion
        public AlunoViewModel()
        {
            Alunos = new ObservableCollection<Aluno>() {};
            OnAddAluno = new Command(IrNovoAluno);
            OnNovoAluno = new Command(NovoAluno);
        }

        
        public void NovoAluno()
        {
            if (String.IsNullOrEmpty(App.AlunoVM.Nome))
            {
                App.Current.MainPage.DisplayAlert("Erro", "Nome não pode ser vazio", "OK");
            }
            else
            {
                //correção visual já que só o nome é obrigatório para não ficar feio para o usuário
                if (App.AlunoVM.Email == null)
                    App.AlunoVM.Email = "sem email";
                if (App.AlunoVM.RM == null)
                    App.AlunoVM.RM = "000000000";
                //adiciona aluno na lista da VM
                Aluno novoAluno = new Aluno() { Nome = App.AlunoVM.Nome, Email = App.AlunoVM.Email, Id = Guid.NewGuid(), RM = App.AlunoVM.RM };
                App.AlunoVM.Alunos.Add(novoAluno);
                App.Current.MainPage.DisplayAlert("Novo aluno cadastrado!",
                    string.Format(@"nome: {0} RM: {1} e-mail: {2}", Nome, RM, Email), "OK");

                //limpa para na tela de novo aluno não reaparecer o nome
                App.AlunoVM.Nome = null;
                App.AlunoVM.Email= null;
                App.AlunoVM.RM = null;
                
                App.Current.MainPage.Navigation.PopAsync();
            }
        }
        
        public void IrNovoAluno()
        {
            App.Current.MainPage.Navigation
                .PushAsync(new View.NovoAlunoView() { BindingContext = App.AlunoVM });
        }
    }
}
