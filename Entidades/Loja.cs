using System;
using System.Collections.Generic;

namespace FluentNHibertanteExemplo.Entidades
{
    public class Loja
    {
        protected internal Loja()
        {
            this.Id = Guid.NewGuid();
            this.Produtos = new List<Produto>();
            this.Funcionarios = new List<Funcionario>();
        }

        public Loja(string nome)
            : this()
        {
            this.Nome = nome;
        }

        public virtual Guid Id { get; protected set; }
        public virtual string Nome { get; set; }
        public virtual IList<Produto> Produtos { get; set; }
        public virtual IList<Funcionario> Funcionarios { get; protected set; }

        public virtual void AdicionarProduto(Produto produto)
        {
            produto.LojasEmEstoque.Add(this);
            this.Produtos.Add(produto);
        }

        public virtual void AdicionarFuncionario(Funcionario funcionario)
        {
            funcionario.Loja = this;
            this.Funcionarios.Add(funcionario);
        }
    }
}
