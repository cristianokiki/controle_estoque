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
    public class ProdutoPageModel : BasePageModel
    {
        public ObservableCollection<Produto> Produtos { get; set; }

        private Produto _produtoSelecionado;
        public Produto ProdutoSelecionado
        {
            get { return _produtoSelecionado; }
            set
            {
                _produtoSelecionado = value;
                IsUpdateEnable = _produtoSelecionado != null;
                IsDeleteEnable = _produtoSelecionado != null;
            }
        }

        public bool IsUpdateEnable { get; set; }
        public bool IsDeleteEnable { get; set; }

        public ICommand InsertCommand => new Command(async () =>
        {
            ProdutoSelecionado = null;

            await CoreMethods.PushPageModel<InsertOrUpdateProdutoPageModel>(new { produto = ProdutoSelecionado });

        });

        public ICommand UpdateCommand => new Command(async () =>
        {

            await CoreMethods.PushPageModel<InsertOrUpdateProdutoPageModel>(new { produto = ProdutoSelecionado });


        }, () => IsUpdateEnable);

        public ICommand SearchCommand => new Command(Search);

        public ICommand DeleteCommand => new Command(async () =>
        {

            var op = await CoreMethods.DisplayAlert("Confirmação", "Deseja excluir o produto selecionado?", "Sim", "Não");

            if (op)
                Delete();

        }, () => IsDeleteEnable);


        public override void Init(object initData)
        {
            base.Init(initData);
            Search();

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
            ProdutoSelecionado = null;
            using (UserDialogs.Instance.Loading("Por favor, aguarde..."))
            {
                try
                {
                    var service = RefitServiceImpl.GetService();
                    var response = await service.GetProdutos();
                    if (response.IsSuccessStatusCode)
                        Produtos = new ObservableCollection<Produto>(response.Content);
                }
                catch (Exception)
                {
                    await CoreMethods.DisplayAlert("Falha", "Falha ao enviar requisição", "OK");
                }
            }
        }

        private async void Delete()
        {
            using (UserDialogs.Instance.Loading("Por Favor, aguarde..."))
            {
                try
                {
                    var service = RefitServiceImpl.GetService();
                    var response = await service.DeleteProdutos(new List<Dto.DeleteDto>() { new Dto.DeleteDto { key = "id", value = ProdutoSelecionado.Id.Value } });
                    if (response.IsSuccessStatusCode)
                    {
                        UserDialogs.Instance.Toast("Excluído com sucesso!");
                        Search();
                    }
                    else
                    {
                        UserDialogs.Instance.Toast("Não foi possível excluir!");
                    }

                }
                catch (Exception)
                {
                    await CoreMethods.DisplayAlert("Falha", "Falha ao enviar requisição", "OK");
                }
            }
        }
    }
}
