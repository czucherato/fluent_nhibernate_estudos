using System;

namespace FluentNHibertanteExemplo.Entidades
{
    public class Funcionario
    {
        protected internal Funcionario()
        {
            this.Id = Guid.NewGuid();
            this.Endereco = new Endereco();
        }

        public Funcionario(string nome, string sobrenome, Loja loja, Endereco endereco)
            : this()
        {
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.Loja = loja;
            this.Endereco = endereco;
        }

        public virtual Guid Id { get; protected set; }
        public virtual string Nome { get; set; }

        public virtual Endereco Endereco { get; set; }

        //private string NomeCompleto 
        //{
        //    get { return this.ObterNomeCompleto(); }

        //    set { this.NomeCompleto = this.ObterNomeCompleto(); }
        //}

        public virtual string Sobrenome { get; set; }
        public virtual Loja Loja { get; set; }

        //public virtual string ObterNomeCompleto()
        //{
        //    return string.Format("{0} {1}", this.Nome, this.Sobrenome);
        //}
    }
}
