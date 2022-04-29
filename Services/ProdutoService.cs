using mitienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mitienda.Services
{
    public class ProdutoService
    {
        public static List<Products> produtos = new List<Products>() {
                new Products { Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"), ProductId = "bola-futebol", Name = "Bola de Futebol", Price = 50.00, Image = "https://http2.mlstatic.com/D_NQ_NP_870160-MLB46264483457_062021-O.webp", Type = "Bolas" },
                new Products { Id = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"), ProductId = "bola-basquete", Name = "Bola de Basquete", Price = 60.00, Image = "https://img.elo7.com.br/product/zoom/329A3EE/painel-de-festa-em-tecido-bola-de-basquete-1-60-x-1-60-kit-festa.jpg", Type = "Bolas" },
                new Products { Id = Guid.Parse("a04d208d-a4f7-4f4d-b635-14bd65147db0"), ProductId = "chuteira-futsal", Name = "Chuteira de Futsal", Price= 360.00, Image = "https://static.netshoes.com.br/produtos/chuteira-futsal-nike-mercurial-superfly-8-club/72/HZM-5384-172/HZM-5384-172_zoom1.jpg?ts=1626301089&ims=544x", Type = "Calçados" },
                new Products { Id = Guid.Parse("61f7b146-8e35-4006-8038-9758371ed956"), ProductId = "chuteira-campo", Name = "Chuteira de Campo", Price= 260.00, Image = "https://static.netshoes.com.br/produtos/chuteira-campo-nike-phantom-gt-academy-df/08/HZM-3949-108/HZM-3949-108_zoom1.jpg?ts=1615299275", Type = "Calçados" },
                new Products { Id = Guid.Parse("436f9026-a428-477d-bcbc-0261e265c145"), ProductId = "camisa-barcelona", Name = "Camisa do Barcelona", Price= 160.00, Image = "https://cdn.dooca.store/17236/products/camisa-nike-barcelona-ii-202021-torcedor-pro-infantil-cd4499-011-1-11626441478-removebg-preview_640x640+fill_ffffff.png?v=1637123553&webp=0", Type = "Camisas" },
        };

        private EstoqueService estoqueService;

        public ProdutoService()
        {
            estoqueService = new EstoqueService();
        }

        /// <summary>
        /// Responsável por pegar uma lista com todos os produtos do DB
        /// </summary>
        /// <returns>Lista de produtos (List<Products>) </Products></returns>
        public List<Products> GetProducts()
        {
            return produtos;
        }

        /// <summary>
        /// Responsável por pegar um produto do DB utilizando o id do produto
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>Retorna, caso encontre, um produto (Product) e NULL caso não encontre.</returns>
        public Products GetProductByProductId(string productId)
        {
            try
            {
                var product = produtos.Where(x => x.ProductId == productId).FirstOrDefault();
                if (product != null)
                    return product;

                return null;

            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        /// <summary>
        /// Responsável por criar um produto utilizando um model. E cria automáticamente um item no estoque para ele, com quantidade padrão de 0
        /// </summary>
        /// <param name="model"></param>
        /// <param name="quantity"></param>
        /// <returns>Retorna True caso tenha sido bem sucedido</returns>
        public bool CreateProduct(Products model, int quantity = 0)
        {
            try
            {
                if (model != null && !ExistProduct(model.ProductId))
                {
                    var newStock = new Stock
                    {
                        Id = Guid.NewGuid(),
                        ProductName = model.Name,
                        ProductId = model.ProductId,
                        Quantity = quantity
                    };

                    //Tenta colocar no estoque, caso não consiga ele não cria o produto
                    if (estoqueService.CreateStock(newStock))
                    {
                        var newProduct = new Products
                        {
                            Id = Guid.NewGuid(),
                            Name = model.Name,
                            ProductId = model.ProductId,
                            Price = model.Price,
                            Image = model.Image,
                            Type = model.Type
                        };

                        produtos.Add(newProduct);

                        return true;
                    }
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
        /// Responsável por atualizar os dados de um determinado produto.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Retorna True caso tenha sido bem sucedido</returns>
        public bool UpdateProduct(Products model)
        {

            try
            {
                if (model != null)
                {
                    var product = GetProductByProductId(model.ProductId);
                    if (product != null)
                    {
                        product.Name = model.Name != null ? model.Name : product.Name;
                        product.ProductId = model.ProductId != null ? model.ProductId : product.ProductId;
                        product.Image = model.Image != null ? model.Image : product.Image;
                        product.Type = model.Type != null ? model.Type : product.Type;

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
        /// Responsável por deletar completamente um produto
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool DeleteProduct(string productId)
        {
            try
            {
                if (ExistProduct(productId))
                {
                    var product = produtos.Where(x => x.ProductId == productId).FirstOrDefault();
                    if (product != null)
                    {
                        produtos.Remove(product);

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

        private bool ExistProduct(string productId)
        {
            try
            {
                var product = GetProductByProductId(productId);

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
