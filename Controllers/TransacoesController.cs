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
    public class TransacoesController : ControllerBase
    {
        private TransacoesService transacoesService;
       
        public TransacoesController()
        {
            transacoesService = new TransacoesService();
        }

        // GET: api/transacoes
        [HttpGet]
        public List<Transiction> GetTransictions()
        {
            return transacoesService.GetTransictions() ;
        }

        // GET: api/transacoes/transictionId
        [HttpGet("getById/{transictionId}")]
        public Transiction GetById(Guid transictionId)
        {
            try
            {
                if(transictionId != null)
                {
                    return transacoesService.GetById(transictionId);
                }

                return null;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        [HttpGet("getByClietId/{clientId}")]
        public List<Transiction> GetByClientId(Guid clientId)
        {
            try
            {
                if(clientId != null)
                {
                    return transacoesService.GetByClientId(clientId);
                }

                return null;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        [HttpPost]
        public bool CreateTransiction(Transiction transiction)
        {
            try
            {
                if(transiction != null)
                {
                    return transacoesService.CreateTransiction(transiction);
                }

                return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        [HttpDelete]
        public bool DeleteTransiction(Guid transictionId)
        {
            try
            {
                if (transictionId != null)
                {
                    return transacoesService.DeleteTransiction(transictionId);
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
