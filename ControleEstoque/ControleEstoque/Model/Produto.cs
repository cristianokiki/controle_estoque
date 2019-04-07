using System;
using System.Collections.Generic;
using System.Text;

namespace ControleEstoque.Model
{
    public class Produto
    {
        public int? Id { get; set; }
        public int FornecedorId { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Unidade { get; set; }
        public bool Desativado { get; set; }
        public Fornecedor TbFornecedor { get; set; }
    }
}
