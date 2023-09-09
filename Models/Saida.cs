namespace newmarket.Models
{
    public class Saida
    {
        public int Id {get; set;}

        public Produto Produto {get; set;}

		public float Quantidade {get; set;}

        public float ValorDaVenda {get; set;}

        public DateTime Data {get; set;}

		public Venda venda {get; set;}
    }
}