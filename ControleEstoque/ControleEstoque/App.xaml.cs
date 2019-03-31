using ControleEstoque.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ControleEstoque
{
    public partial class App : Application
    {
        public App()
        {

            //LiveReload.Init();
            InitializeComponent();

            var master = new FreshMvvm.FreshMasterDetailNavigationContainer();
            master.Init("Menu");
            master.AddPage<HomePageModel>("Home", null);
            master.AddPage<ProdutoPageModel>("Produto", null);
            master.AddPage<FornecedorPageModel>("Fornecedor", null);
            master.AddPage<MovimentoPageModel>("Movimento", null);
            master.AddPage<EstoquePageModel>("Estoque", null);

            MainPage = master;
            // MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
