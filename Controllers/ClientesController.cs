using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mitienda.Data;
using mitienda.Models;
using mitienda.Services;

namespace mitienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private ClienteService clienteService;

        public ClientesController()
        {
            clienteService = new ClienteService();
        }
       
        // GET: api/clientes
        [HttpGet]
        public List<Clientes> GetClients()
        {
            return clienteService.GetClients();
        }

        //GET: api/clientes/{cpf}
        [HttpGet("{cpf}")]
        public Clientes GetClienteByCpf(string cpf)
        {
            try
            {
                if (!string.IsNullOrEmpty(cpf))
                    return clienteService.GetClienteByCpf(cpf);

                return null;

            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        //GET: api/clientes/{id}
        [HttpGet("{id}")]
        public Clientes GetClienteById(Guid id)
        {
            try
            {
                if (Guid.Empty != id)
                    return clienteService.GetClienteById(id);

                return null;

            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        // POST: api/clientes
        [HttpPost]
        public bool CreateClient(Clientes model)
        {
            try
            {
                if (model != null)
                {
                    return clienteService.CreateClient(model);
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Encontrado um erro: {ex}");
                throw;
            }
        }

        //PUT: api/clientes/
        [HttpPut]
        public bool UpdateClient(Clientes model)
        {

            try
            {
                if (model != null)
                {
                    return clienteService.UpdateClient(model);
                }

                return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        //Delete: api/clientes
        [HttpDelete]
        public bool DeleteClient(string cpf)
        {
            try
            {
                if (!string.IsNullOrEmpty(cpf))
                {
                    return clienteService.DeleteClient(cpf);
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
