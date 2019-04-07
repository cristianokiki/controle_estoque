using Acr.UserDialogs;
using ControleEstoque.Model;
using ControleEstoque.Pages;
using ControleEstoque.Service;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ControleEstoque.Pages
{
    public class InsertOrUpdateProdutoPageModel : BasePageModel
    {
        public Produto Produto { get; set; }

        public ObservableCollection<Fornecedor> Fornecedores { get; set; }

        public ICommand SalvarCommand => new Command(Salvar);

        public override void Init(object initData)
        {
            base.Init(initData);
            var data = initData as dynamic;
            Produto = new Produto();
            if (data.produto != null)
                Produto = data.produto;

            Produto.Desativado = !Produto.Desativado;

            PopulaPicker();
        }

        private async void PopulaPicker()
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

        private async void Salvar()
        {
            if (await Validar())
            {
                Produto = Produto ?? new Produto();
                Produto.FornecedorId = Produto.TbFornecedor.Id.Value;
                Produto.Desativado = !Produto.Desativado;
                try
                {
                    using (UserDialogs.Instance.Loading("Por favor, aguarde..."))
                    {
                        var service = RefitServiceImpl.GetService();
                        ApiResponse<Produto> response = null;
                        if (Produto.Id == null)
                        {
                            response = await service.InsertProduto(Produto);
                        }
                        else
                        {
                            response = await service.UpdateProduto(Produto);
                        }

                        if (response.IsSuccessStatusCode)
                        {
                            UserDialogs.Instance.Toast("Salvo com sucesso!");
                            await CoreMethods.PopPageModel(new { isRefresh = true });
                        }
                        else
                        {
                            UserDialogs.Instance.Toast("Não foi possível salvar!");
                        }
                    }
                }
                catch (Exception)
                {
                    await CoreMethods.DisplayAlert("Falha", "Falha ao enviar requisição", "OK");
                }

            }
        }

        private async Task<bool> Validar()
        {
            if (string.IsNullOrEmpty(Produto.Codigo) || string.IsNullOrEmpty(Produto.Descricao) || string.IsNullOrEmpty(Produto.Unidade) || Produto.TbFornecedor == null)
            {
                await CoreMethods.DisplayAlert("Erro", "Preencha todos os campos!", "Ok");
                return false;
            }
            if (!Produto.Desativado)
            {
                var op = await CoreMethods.DisplayAlert("Atenção", "Deseja salvar o produto desativado?", "Sim", "Não");
                if (op)
                    return true;

                return false;
            }

            return true;
        }
    }
}
