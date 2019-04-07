using ControleEstoque.Dto;
using ControleEstoque.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstoque.Service
{
    [Headers("Content-Type: application/json")]
    public interface IRefitService
    {
        #region estoque
        [Get("/Estoque/Consolidar")]
        Task<ApiResponse<List<Estoque>>> GetEstoque();
        #endregion

        #region produto
        [Get("/Produto/Produtos")]
        Task<ApiResponse<List<Produto>>> GetProdutos();

        [Post("/Produto/Insert")]
        Task<ApiResponse<Produto>> InsertProduto([Body] Produto produto);

        [Put("/Produto/Update")]
        Task<ApiResponse<Produto>> UpdateProduto([Body] Produto produto);

        [Delete("/Produto/Delete")]
        Task<ApiResponse<object>> DeleteProdutos([Body] List<DeleteDto> delete);
        #endregion

        #region fornecedor
        [Get("/Fornecedor/Fornecedores")]
        Task<ApiResponse<List<Fornecedor>>> GetFornecedores();

        [Post("/Fornecedor/Insert")]
        Task<ApiResponse<Fornecedor>> InsertFornecedor([Body] Fornecedor fornecedor);

        [Put("/Fornecedor/Update")]
        Task<ApiResponse<Fornecedor>> UpdateFornecedores([Body] Fornecedor fornecedor);

        [Delete("/Fornecedor/Delete")]
        Task<ApiResponse<object>> DeleteFornecedores([Body] List<DeleteDto> delete);
        #endregion

        #region movimento
        [Get("/Movimentacao/Movimentos")]
        Task<ApiResponse<List<Movimento>>> GetMovimentos();

        [Get("/Movimentacao/Tipos")]
        Task<ApiResponse<List<TipoMovimento>>> GetTipoMovimentacao();

        [Post("/Movimentacao/Insert")]
        Task<ApiResponse<Movimento>> PostMovimentacao([Body] Movimento movimentacao);
        #endregion
    }
}
