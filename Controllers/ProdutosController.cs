using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using mitienda.Models;
using mitienda.Services;
using Microsoft.AspNetCore.Cors;

namespace mitienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private ProdutoService produtoService;

        public ProdutosController()
        {
            produtoService = new ProdutoService();
        }

        // GET: api/produtos
        /// <summary>
        /// Responsável por pegar uma lista com todos os produtos do DB
        /// </summary>
        /// <returns>Lista de produtos (List<Products>) </Products></returns>
        [HttpGet]
        public List<Products> GetProducts()
        {
            return produtoService.GetProducts();
        }

        //GET: api/produtos/{productId}
        /// <summary>
        /// Responsável por pegar um produto do DB utilizando o id do produto
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>Retorna, caso encontre, um produto (Product) e NULL caso não encontre.</returns>
        [HttpGet("{productId}")]
        public Products GetProductByProductId(string productId)
        {
            try
            {
                if (!string.IsNullOrEmpty(productId))
                    return produtoService.GetProductByProductId(productId);

                return null;

            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        // POST: api/produtos
        /// <summary>
        /// Responsável por criar um produto utilizando um model. E cria automáticamente um item no estoque para ele, com quantidade padrão de 0
        /// </summary>
        /// <param name="model"></param>
        /// <param name="quantity"></param>
        /// <returns>Retorna True caso tenha sido bem sucedido</returns>
        [HttpPost]
        public bool CreateProduct(Products model, int quantity = 0)
        {
            try
            {
                if (model != null)
                {
                    return produtoService.CreateProduct(model, quantity);
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Encontrado um erro: {ex}");
                throw;
            }
        }

        //PUT: api/produtos/
        /// <summary>
        /// Responsável por atualizar os dados de um determinado produto.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Retorna True caso tenha sido bem sucedido</returns>
        [HttpPut]
        public bool UpdateProduct(Products model)
        {

            try
            {
                if (model != null)
                {
                    return produtoService.UpdateProduct(model);
                }

                return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        //Delete: api/produtos
        /// <summary>
        /// Responsável por deletar completamente um produto
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete]
        public bool DeleteProduct(string productId)
        {
            try
            {
                if (!string.IsNullOrEmpty(productId))
                {
                    return produtoService.DeleteProduct(productId);
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

    }
}
