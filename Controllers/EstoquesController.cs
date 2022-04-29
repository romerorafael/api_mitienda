using Microsoft.AspNetCore.Mvc;
using mitienda.Models;
using mitienda.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mitienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoquesController : ControllerBase
    {
        private EstoqueService estoqueService;

        public EstoquesController()
        {
            estoqueService = new EstoqueService();
        }

        // GET: api/estoque
        /// <summary>
        /// Responsável por retornar todo o estoque do DB
        /// </summary>
        /// <returns>Retorna uma lista de itens do estoque (List<Stock>)</Stok></returns>
        [HttpGet]
        public List<Stock> GetStock()
        {
            try
            {
                return estoqueService.GetStock();
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        //GET: api/estoque/{productId}
        /// <summary>
        /// Responsável por pegar um item especifico do DB pelo productId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>Retorna um item do estoque (Stock)</returns>
        [HttpGet("{productId}")]
        public Stock GetStockByProductId(string productId)
        {
            try
            {
                var stock = estoqueService.GetStockByProductId(productId);
                if (stock != null)
                    return stock;

                return null;

            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        // POST: api/estoque
        /// <summary>
        /// Responsável por criar o item dentro do estoque
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Retorna True caso tenha sido criado</returns>
        [HttpPost]
        public bool CreateStock(Stock model)
        {
            try
            {
                if (model != null)
                {
                    return estoqueService.CreateStock(model);
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Encontrado um erro: {ex}");
                throw;
            }
        }

        //PUT: api/estoque/
        /// <summary>
        /// Responsável por atualizar o item dentro do estoque
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Retorna True caso tenha obtido sucesso</returns>
        [HttpPut]
        public bool UpdateStock(Stock model)
        {

            try
            {
                if (model != null)
                {
                    return estoqueService.UpdateStock(model);
                }

                return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        //Delete: api/estoque
        /// <summary>
        /// Responsável por deletar o item dentro do estoque
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>Retorna True caso seja bem sucedido</returns>
        [HttpDelete]
        public bool DeleteStock(string productId)
        {
            try
            {
                if (!string.IsNullOrEmpty(productId))
                {
                    return estoqueService.DeleteStock(productId);
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
