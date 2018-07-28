using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Microservices.Entities
{
    public class PessoaJuridica: Pessoa
    {
        [Required]
        public string RazaoSocial { get; set; }
        [Required]
        public string CNPJ { get; set; }
    }
}
