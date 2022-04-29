using mitienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mitienda.Services
{
    public class EstoqueService
    {
        public static List<Stock> estoque = new List<Stock>() {
                new Stock { Id=Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"), ProductId= "bola-futebol", ProductName = "Bola de Futebol", Quantity= 100},
                new Stock { Id = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"), ProductId = "bola-basquete", ProductName = "Bola de Futebol", Quantity= 50},
                new Stock { Id = Guid.Parse("a04d208d-a4f7-4f4d-b635-14bd65147db0"), ProductId = "chuteira-futsal", ProductName = "Chuteira de Futsal", Quantity= 10},
                new Stock { Id = Guid.Parse("61f7b146-8e35-4006-8038-9758371ed956"), ProductId = "chuteira-campo", ProductName = "Chuteira de Campo", Quantity= 0},
                new Stock { Id = Guid.Parse("436f9026-a428-477d-bcbc-0261e265c145"), ProductId = "camisa-barcelona", ProductName = "Camisa do Barcelona", Quantity= 50}
        };

        public EstoqueService()
        { 
        }

        /// <summary>
        /// Responsável por retornar todo o estoque do DB
        /// </summary>
        /// <returns>Retorna uma lista de itens do estoque (List<Stock>)</Stok></returns>
        public List<Stock> GetStock()
        {
            return estoque;
        }

        /// <summary>
        /// Responsável por pegar um item especifico do DB pelo productId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>Retorna um item do estoque (Stock)</returns>
        public Stock GetStockByProductId(string productId)
        {
            try
            {
                var stock = estoque.Where(x => x.ProductId == productId).FirstOrDefault();
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

        /// <summary>
        /// Responsável por criar o item dentro do estoque
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Retorna True caso tenha sido criado</returns>
        public bool CreateStock(Stock model)
        {
            try
            {
                if (model != null)
                {
                    if (!ExistStock(model.ProductId))
                    {
                        var newStock = new Stock
                        {
                            Id = Guid.NewGuid(),
                            ProductName = model.ProductName,
                            ProductId = model.ProductId,
                            Quantity = model.Quantity
                        };

                        estoque.Add(newStock);

                        return true;
                    }

                    return false;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Encontrado um erro: {ex}");
                throw;
            }
        }

        /// <summary>
        /// Responsável por verificar se existem itens suficiente para finalizar a venda
        /// </summary>
        /// <param name="itens"></param>
        /// <returns>Return True se pode ser vendido</returns>
        public bool CanSell(List<Item> itens)
        {
            try
            {
                if (itens != null)
                {
                    var canSell = true;

                    foreach (var item in itens)
                    {
                        var stockProduct = GetStockByProductId(item.ProductId);
                        if(item.Quantity > stockProduct.Quantity)
                        {
                            canSell = false;
                        }
                    }

                    if (canSell)
                    {
                        foreach (var item in itens)
                        {
                            var stockProduct = GetStockByProductId(item.ProductId);
                            stockProduct.Quantity -= item.Quantity;
                        }
                    }

                    return canSell;
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
        /// Responsável por atualizar o item dentro do estoque
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Retorna True caso tenha obtido sucesso</returns>
        public bool UpdateStock(Stock model)
        {

            try
            {
                if (model != null)
                {
                    var stock = GetStockByProductId(model.ProductId);
                    if (stock != null)
                    {
                        stock.ProductName = model.ProductName;
                        stock.ProductId = model.ProductId;
                        stock.Quantity = model.Quantity;

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
        /// Responsável por deletar o item dentro do estoque
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>Retorna True caso seja bem sucedido</returns>
        public bool DeleteStock(string productId)
        {
            try
            {
                if (ExistStock(productId))
                {
                    var stock = estoque.Where(x => x.ProductId == productId).FirstOrDefault();
                    if (stock != null)
                    {
                        estoque.Remove(stock);

                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        private bool ExistStock(string productId)
        {
            try
            {
                var product = GetStockByProductId(productId);

                if (product != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

    }
}
