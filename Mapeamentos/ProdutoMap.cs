using FluentNHibernate.Mapping;
using FluentNHibertanteExemplo.Entidades;

namespace FluentNHibertanteExemplo.Mapeamentos
{
    public class ProdutoMap : ClassMap<Produto>
    {
        public ProdutoMap()
        {
            Id(p => p.Id)
                .GeneratedBy
                .Assigned();

            Map(p => p.Nome);

            Map(p => p.Preco);

            HasManyToMany(p => p.LojasEmEstoque)
                .Cascade.All()
                .Inverse()
                .Table("LojaProduto");
        }
    }
}
