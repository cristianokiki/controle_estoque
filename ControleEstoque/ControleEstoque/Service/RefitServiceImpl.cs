using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControleEstoque.Service
{
    public class RefitServiceImpl
    {
        public static IRefitService GetService()
        {
            //definir url
            string url = "http://controleestoque.somee.com/Api";
            return RestService.For<IRefitService>(url);
        }
    }
}
