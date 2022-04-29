using mitienda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mitienda.Services
{
    public class ClienteService
    {
        public static List<Clientes> clientes = new List<Clientes>() {
                    new Clientes { Id=Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"), Name= "Rafael Romero", Cpf= "06675747940", Cep = "80240140" },
                    new Clientes { Id = Guid.Parse("7c9e6679-7425-40de-944b-e07fc1f90ae7"), Name = "Lucas Stark", Cpf = "06675747923", Cep = "80240280" }
            };


        /// <summary>
        /// Responsável por pegar todos os clientes do DB
        /// </summary>
        /// <returns>Retorna todos </returns>
        public List<Clientes> GetClients()
        {
            return clientes;
        }

        /// <summary>
        /// Responsável por buscar o cliente com determinado CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns>Retorna o cliente com o CPF determinado ou NULL</returns>
        public Clientes GetClienteByCpf(string cpf)
        {
            try
            {
                var client = clientes.Where(x => x.Cpf == cpf).FirstOrDefault();
                if (client != null)
                    return client;

                return null;

            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }


        // <summary>
        /// Responsável por buscar o cliente com determinado CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns>Retorna o cliente com o CPF determinado ou NULL</returns>
        public Clientes GetClienteById(Guid id)
        {
            try
            {
                var client = clientes.Where(x => x.Id == id).FirstOrDefault();
                if (client != null)
                    return client;

                return null;

            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        /// <summary>
        /// Responsável por criar um novo cliente
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Retorna True caso seja bem sucedido</returns>
        public bool CreateClient(Clientes model)
        {
            try
            {
                if (model != null)
                {
                    if (!ExistClient(model.Cpf))
                    {
                        var newClient = new Clientes
                        {
                            Id = Guid.NewGuid(),
                            Name = model.Name,
                            Cep = model.Cep,
                            Cpf = model.Cpf
                        };

                        clientes.Add(newClient);

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
        /// Responsável por atualizar o cliente, não pode atualizar o CPF que é a chave de busca
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Retorna True caso seja bem sucedido</returns>
        public bool UpdateClient(Clientes model)
        {

            try
            {
                if (model != null)
                {
                    var client = GetClienteByCpf(model.Cpf);
                    if (client != null)
                    {
                        client.Name = model.Name;
                        client.Cpf = model.Cpf;
                        client.Cep = model.Cep;

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
        /// Responsável por deletar o cliente do DB
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns>Retorna True caso seja bem sucedido</returns>
        public bool DeleteClient(string cpf)
        {
            try
            {
                if (!string.IsNullOrEmpty(cpf))
                {
                    var client = clientes.Where(x => x.Cpf == cpf).FirstOrDefault();
                    if (client != null)
                    {
                        clientes.Remove(client);

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

        private bool ExistClient(string cpf)
        {
            try
            {
                var client = clientes.Where(x => x.Cpf == cpf).FirstOrDefault();
                if (client != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }
    }
}
