using Acr.UserDialogs;
using ControleEstoque.Model;
using ControleEstoque.Service;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ControleEstoque.Pages
{
    public class InsertOrUpdateFornecedorPageModel : BasePageModel
    {

        public override void Init(object initData)
        {
            base.Init(initData);
            var data = initData as dynamic;
            Fornecedor = data.fornecedor;
            LoadUpdate();
        }

        #region binding
        public string Nome { get; set; }
        public string Cnpj { get; set; }

        private Fornecedor Fornecedor { get; set; }
        #endregion

        #region command
        public ICommand SalvarCommand => new Command(Salvar);
        #endregion


        #region function
        private async void Salvar()
        {
            if (Validation())
            {
                Fornecedor = Fornecedor ?? new Fornecedor();
                Fornecedor.Cnpj = Cnpj;
                Fornecedor.Nome = Nome;

                try
                {
                    using (UserDialogs.Instance.Loading("Por favor, aguarde..."))
                    {
                        var service = RefitServiceImpl.GetService();
                        ApiResponse<Fornecedor> response = null;

                        if (Fornecedor.Id == null)
                        {
                            response = await service.InsertFornecedor(Fornecedor);
                        }
                        else
                        {
                            response = await service.UpdateFornecedores(Fornecedor);
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
                catch
                {
                    await CoreMethods.DisplayAlert("Falha", "Falha ao enviar requisição", "OK");
                }
            }
        }

        private void LoadUpdate()
        {
            if (Fornecedor != null)
            {
                Nome = Fornecedor.Nome;
                Cnpj = Fornecedor.Cnpj;
            }
        }

        private bool Validation()
        {
            if (string.IsNullOrEmpty(Nome))
            {
                CoreMethods.DisplayAlert("Validação", "Nome é Obrigatório!", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(Cnpj))
            {
                CoreMethods.DisplayAlert("Validação", "Cnpj é Obrigatório!", "OK");
                return false;
            }

            return true;
        }
        #endregion
    }
}
