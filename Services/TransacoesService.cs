using mitienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mitienda.Services
{
    public class TransacoesService
    {
        public static List<Transiction> transictions = new List<Transiction>() 
        {
                new Transiction { Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"), ClientId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"), ClientName = "Rafael Romero", Date = DateTime.Now, Total = 110.00, Products = new List<Item>() { new Item { ProductId= "bola-futebol", Quantity = 1}, new Item { ProductId = "Bola de Basquete", Quantity = 1} } },
                new Transiction { Id = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"), ClientId = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"), ClientName = "Lucas Stark", Date = DateTime.Now.AddDays(-1), Total = 70.00, Products = new List<Item>() { new Item { ProductId = "bola-basquete", Quantity = 1} } } 
        };

        private ProdutoService productService;
        private EstoqueService estoqueService;
        private ClienteService clienteService;

        public TransacoesService()
        {
            productService = new ProdutoService();
            estoqueService = new EstoqueService();
            clienteService = new ClienteService();
        }

        /// <summary>
        /// Responsável por pegar uma lista com todos os produtos do DB
        /// </summary>
        /// <returns>Lista de produtos (List<Products>) </Products></returns>
        public List<Transiction> GetTransictions()
        {
            return transictions;
        }

        /// <summary>
        /// Responsável por trazer uma transição pelo seu Id
        /// </summary>
        /// <param name="transictionId"></param>
        /// <returns></returns>
        public Transiction GetById(Guid transictionId)
        {
            return transictions.Where( x => x.Id == transictionId).First();
        }

        /// <summary>
        /// Retorna todas as transições feitas por um cliente
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public List<Transiction> GetByClientId(Guid clientId)
        {
            return transictions.Where(x => x.ClientId == clientId).ToList();
        }

        /// <summary>
        /// Responsável por cadastrar uma nova transação
        /// </summary>
        /// <param name="transiction"></param>
        /// <returns></returns>
        public bool CreateTransiction(Transiction transiction)
        {
            try
            {
                if(transiction != null)
                {
                    //Verifica se pode acontecer a venda e atualiza o estoque
                    var canSell = estoqueService.CanSell(transiction.Products);

                    if (canSell)
                    {
                        var newTransaction = new Transiction
                        {
                            Id = Guid.NewGuid(),
                            ClientId = transiction.ClientId,
                            Total = transiction.Total,
                            Products = transiction.Products,
                            Date = DateTime.Now, 
                            ClientName = transiction.ClientName
                        };

                        transictions.Add(newTransaction);

                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// Responsável por deletar uma transição
        /// </summary>
        /// <param name="transictionId"></param>
        /// <returns>Retorna True caso seja bem sucedido</returns>
        public bool DeleteTransiction(Guid transictionId)
        {
            try
            {
                if(transictionId != null)
                {
                    var transiction = GetById(transictionId);
                    if(transiction != null)
                    {
                        transictions.Remove(transiction);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
