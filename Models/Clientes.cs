using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mitienda.Models
{
    public class Clientes
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(100, ErrorMessage = "O campo nome suporta até {1} caracteres")]
        public string Name { get; set; }
        [MaxLength(12, ErrorMessage = "O campo cpf suporta até {1} caracteres")]
        public string Cpf { get; set; }
        [MaxLength(8, ErrorMessage = "O campo cep suporta até {1} caracteres")]
        public string Cep { get; set; }
    }
}
