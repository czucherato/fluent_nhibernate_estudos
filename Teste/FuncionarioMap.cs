using System;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentNHibertanteExemplo.Entidades;
using FluentNHibertanteExemplo.Auxiliares;

namespace FluentNHibertanteExemplo.Teste
{
    [TestClass]
    public class FuncionarioMap
    {
        [TestMethod]
        public void MapeamentoFuncionarioComReferencias()
        {
            ISessionFactory fabricaDeSessao = CreateSessioFactory();

            using (ISession sessao = fabricaDeSessao.OpenSession())
            {
                new PersistenceSpecification<Funcionario>(sessao)
                .CheckProperty(x => x.Id, Guid.NewGuid())
                .CheckProperty(x => x.Nome, "Carlos Alberto")
                .CheckProperty(x => x.Sobrenome, "Zucherato")
                .CheckReference(x => x.Loja, new Loja("Fnac"))
                .CheckProperty(x => x.Endereco, new Endereco("Rua Rita Maria de Jesus", "Santo Antônio do Jardim", "13995000"));
            }
        }

        [TestMethod]
        public void MapeamentoFuncionarioSemReferencias()
        {
            ISessionFactory fabricaDeSessao = CreateSessioFactory();

            using (ISession sessao = fabricaDeSessao.OpenSession())
            {
                new PersistenceSpecification<Funcionario>(sessao)
                .CheckProperty(x => x.Id, Guid.NewGuid())
                .CheckProperty(x => x.Nome, "Carlos Alberto")
                .CheckProperty(x => x.Sobrenome, "Zucherato")
                .VerifyTheMappings();
            }
        }

        [TestMethod]
        public void MapeamentoFuncionarioChecandoId()
        {
            ISessionFactory fabricaDeSessao = CreateSessioFactory();

            using (ISession sessao = fabricaDeSessao.OpenSession())
            {
                new PersistenceSpecification<Funcionario>(sessao, new CustomEqualityComparer())
                .CheckProperty(x => x.Id, Guid.NewGuid())
                .CheckProperty(x => x.Nome, "Carlos Alberto")
                .CheckProperty(x => x.Sobrenome, "Zucherato")
                .VerifyTheMappings();
            }
        }

        private static ISessionFactory CreateSessioFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                //.ConnectionString(x => x.FromConnectionStringWithKey("FluentNHibernateTeste-Local"))
                .ConnectionString(x =>
                x.Server("(local)")
                .Database("FluentNHibernateTeste")
                .Username("sa")
                .Password("sa"))
                )
                .Mappings(m =>
                    m.FluentMappings.AddFromAssemblyOf<Program>())
                .BuildSessionFactory();
        }
    }
}
