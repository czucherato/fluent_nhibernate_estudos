using FluentNHibernate.Mapping;
using FluentNHibertanteExemplo.Entidades;

namespace FluentNHibertanteExemplo.Mapeamentos
{
    public class LojaMap : ClassMap<Loja>
    {
        public LojaMap()
        {
            Id(l => l.Id)
                .GeneratedBy
                .Assigned();

            Map(l => l.Nome);

            //one-to-many - uma loja para muitos funcionários
            HasMany(l => l.Funcionarios)
                .Inverse()
                .Cascade.All();

            //many-to-many 
            HasManyToMany(l => l.Produtos)
                .Cascade.All()
                .Table("LojaProduto")
                .LazyLoad();
        }
    }
}
