using System;
using System.Collections.Generic;
using System.Text;

namespace ControleEstoque.Model
{
    public class Estoque
    {
        public int ProdutoId { get; set; }
        public string ProdutoCodigo { get; set; }
        public string ProdutoDescricao { get; set; }
        public string FornecedorCnpj { get; set; }
        public string FornecedorNome { get; set; }
        public double QtdConsolidada { get; set; }

    }
}
