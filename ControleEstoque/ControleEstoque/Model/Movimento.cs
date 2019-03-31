using System;
using System.Collections.Generic;
using System.Text;

namespace ControleEstoque.Model
{
    public class Movimento
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int TipoMoviId { get; set; }
        public int Qtd { get; set; }
        public DateTime Data { get; set; }
        public TipoMovimento TbTipoMovi { get; set; }
    }
}
