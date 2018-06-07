namespace FluentNHibertanteExemplo.Entidades
{
    public class Endereco
    {
        public Endereco()
        {

        }

        public Endereco(string rua, string cidade, string cep)
        {
            this.Rua = rua;
            this.Cidade = cidade;
            this.Cep = cep;
        }

        public string Rua { get; set; }

        public int Numero { get; set; }

        public string Cidade { get; set; }

        public string Cep { get; set; }
    }
}
