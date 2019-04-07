using Acr.UserDialogs;
using ControleEstoque.Model;
using ControleEstoque.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;

namespace ControleEstoque.Pages
{
    public class EstoquePageModel : BasePageModel
    {
        public ObservableCollection<Estoque> EstoqueConsolidado { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);
            Search();
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
            Search();
        }

        private async void Search()
        {
            using (UserDialogs.Instance.Loading("Por favor, aguarde..."))
            {
                try
                {
                    var service = RefitServiceImpl.GetService();
                    var response = await service.GetEstoque();
                    if (response.IsSuccessStatusCode)
                        EstoqueConsolidado = new ObservableCollection<Estoque>(response.Content);
                }
                catch (Exception)
                {
                    await CoreMethods.DisplayAlert("Falha", "Falha ao enviar requisição", "OK");
                }
            }
        }
    }
}
