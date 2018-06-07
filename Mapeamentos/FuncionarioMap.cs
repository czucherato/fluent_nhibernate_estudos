using FluentNHibernate;
using FluentNHibernate.Mapping;
using FluentNHibertanteExemplo.Entidades;

namespace FluentNHibertanteExemplo.Mapeamentos
{
    public class FuncionarioMap : ClassMap<Funcionario>
    {
        public FuncionarioMap()
        {
            Id(f => f.Id)
                .GeneratedBy
                .Assigned();

            Map(f => f.Nome);

            Map(f => f.Sobrenome);  

            //Map(Reveal.Member<Funcionario>("NomeCompleto"));

            Component(c => c.Endereco, m =>
            {
                m.Map(x => x.Rua);
                m.Map(x => x.Numero);
                m.Map(x => x.Cidade);
                m.Map(x => x.Cep);
            });

            //many-to-one - muitos funcionarios para uma loja
            References(f => f.Loja);
        }
    }
}
