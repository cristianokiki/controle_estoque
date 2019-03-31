using System;
using System.Collections.Generic;
using System.Text;

namespace ControleEstoque.Model
{
    public class Fornecedor
    {
        public int? Id { get; set; }
        public string Cnpj { get; set; }
        public string Nome { get; set; }
    }
}
