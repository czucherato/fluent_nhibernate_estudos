using System;
using System.Collections.Generic;

namespace FluentNHibertanteExemplo.Entidades
{
    public class Produto
    {
        protected internal Produto()
        {
            this.Id = Guid.NewGuid();
            this.LojasEmEstoque = new List<Loja>();
        }

        public Produto(string nome, double preco)
            : this()
        {
            this.Nome = nome;
            this.Preco = preco;
        }

        public virtual Guid Id { get; protected set; }
        public virtual string Nome { get; set; }
        public virtual double Preco { get; set; }
        public virtual IList<Loja> LojasEmEstoque { get; set; }
    }
}
