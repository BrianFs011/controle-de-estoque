namespace controle_estoque.Models
{
    public class Produto
    {
        public string id { get; set; }
        public string nome { get; set; } = string.Empty;
        public string descricao { get; set; } = string.Empty;
        public double preco { get; set; }
        public int quantidade { get; set; }
        public string img { get; set; } = string.Empty;

    }
}
