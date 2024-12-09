

using System.ComponentModel.DataAnnotations.Schema;

namespace WebLoja.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Autenticacao { get; set; }

        public string Endereco { get; set; }

    }
}
