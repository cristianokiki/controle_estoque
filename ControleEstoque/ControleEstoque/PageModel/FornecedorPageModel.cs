using Acr.UserDialogs;
using ControleEstoque.Model;
using ControleEstoque.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ControleEstoque.Pages
{
    public class FornecedorPageModel : BasePageModel
    {

        #region binding
        public ObservableCollection<Fornecedor> Fornecedores { get; set; }

        private Fornecedor _FornecedorSelecionado;
        public Fornecedor FornecedorSelecionado
        {
            get => _FornecedorSelecionado;
            set
            {
                _FornecedorSelecionado = value;
                IsDeleteEnable = _FornecedorSelecionado != null;
                IsUpdateEnable = _FornecedorSelecionado != null;
            }
        }

        public bool IsUpdateEnable { get; set; }
        public bool IsDeleteEnable { get; set; }

        #endregion

        #region command
        public ICommand InsertCommand => new Command(async () =>
        {
            FornecedorSelecionado = null;
            await CoreMethods.PushPageModel<InsertOrUpdateFornecedorPageModel>(new { fornecedor = FornecedorSelecionado });
            //await CoreMethods.PushPageModel<InsertOrUpdateFornecedorPageModel>();
        });
        public ICommand UpdateCommand => new Command(async () =>
        {
            await CoreMethods.PushPageModel<InsertOrUpdateFornecedorPageModel>(new { fornecedor = FornecedorSelecionado });
        }, () => IsUpdateEnable);
        public ICommand DeleteCommand => new Command(async () =>
        {
            var op = await CoreMethods.DisplayAlert("Confirmação", "Deseja excluir o fornecedor selecionado?", "Sim", "Não");
            if (op)
            {
                Delete();
            }

        }, () => IsDeleteEnable);
        public ICommand SearchCommand => new Command(Search);
        #endregion


        public override void Init(object initData)
        {
            base.Init(initData);
            // Search();

            IsDeleteEnable = false;
            IsUpdateEnable = false;
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
            var data = returnedData as dynamic;
            if (data.isRefresh)
            {
                Search();
            }
        }

        private async void Search()
        {
            FornecedorSelecionado = null;
            using (UserDialogs.Instance.Loading("Por favor, aguarde..."))
            {
                try
                {
                    var service = RefitServiceImpl.GetService();
                    var response = await service.GetFornecedores();
                    if (response.IsSuccessStatusCode)
                    {
                        Fornecedores = new ObservableCollection<Fornecedor>(response.Content);
                    }
                }
                catch
                {
                    await CoreMethods.DisplayAlert("Falha", "Falha ao enviar requisição", "OK");
                }
            }
        }

        private async void Delete()
        {
            using (UserDialogs.Instance.Loading("Por favor, aguarde..."))
            {
                try
                {
                    var service = RefitServiceImpl.GetService();
                    var response = await service.DeleteFornecedores(new List<Dto.DeleteDto>() { new Dto.DeleteDto() { key = "id", value = FornecedorSelecionado.Id.Value } });
                    if (response.IsSuccessStatusCode)
                    {
                        //  Fornecedores = new ObservableCollection<Fornecedor>(response.Content);
                        UserDialogs.Instance.Toast("Excluído com sucesso!");
                        Search();
                    } else
                    {
                        UserDialogs.Instance.Toast("Não foi possível excluir!");
                    }
                }
                catch
                {
                    await CoreMethods.DisplayAlert("Falha", "Falha ao enviar requisição", "OK");
                }
            }
        }

    }
}
