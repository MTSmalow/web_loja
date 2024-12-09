using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebLoja.Models
{

    [Table("Produtos")]
    public class Produto
    {
        int id;
        string nome;
        string descricao;
        decimal preco;
        string categoria;
        string foto;



        [Key]
        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public decimal Preco { get => preco; set => preco = value; }
        public string Categoria { get => categoria; set => categoria = value; }
        public string Foto { get => foto; set => foto = value; }
    }
}
