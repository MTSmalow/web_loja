using System.ComponentModel.DataAnnotations;

namespace WebLoja.Models
{
    public class Carrinho
    {
        public Carrinho() 
        {
            Itens = new List<Item>();
        }
        public decimal Total { get => Itens.Sum(it => it.SubTotal); }
        public List<Item> Itens { get; set; }
        public void AddItem(Item item)
        {
            if (Itens.Exists(it => it.Produto.Id == item.Produto.Id))
            {
                int posicao = Itens.FindIndex(it => it.Produto.Id == it.Produto.Id);
                Itens.ElementAt(posicao).Quantidade += item.Quantidade;

            }
            else
            {
                Itens.Add(item);
            }
        }
    }

    public class Item
    {
        [Key]
        public int Id { get; set; }

        private Produto produto;
        private int quantidade;
        public Decimal SubTotal { get => produto.Preco * quantidade; }

        public Produto Produto { get => produto; set => produto = value; }
        public int Quantidade { get => quantidade; set => quantidade = value; }
    }
}
