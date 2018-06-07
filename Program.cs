using NHibernate;
using System.Collections;
using System.Collections.Generic;
using FluentNHibertanteExemplo.Entidades;
using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;
using System.IO;
using System.Text;

namespace FluentNHibertanteExemplo
{
    public class Program
    {
        static void Main(string[] args)
        {
            CriarTabela();

            ISessionFactory fabricaDeSessao = CreateSessioFactory();

            using (ISession sessao = fabricaDeSessao.OpenSession())
            {
                using (ITransaction transacao = sessao.BeginTransaction())
                {
                    Loja americanas = new Loja { Nome = "Americanas" };
                    Loja fnac = new Loja { Nome = " Fnac" };

                    Produto televisao = new Produto { Nome = "Tv Samsung 42", Preco = 2499.99 };
                    Produto panelaEletrica = new Produto { Nome = "Panela Elétrica Arno", Preco = 89.90 };
                    Produto maquinaDeLavar = new Produto { Nome = "Máquina de Lavar Brastemp 10kg", Preco = 1100 };
                    Produto jogoDePanelas = new Produto { Nome = "Jogo de Panelas Tramontina 4pcs", Preco = 100 };
                    Produto consolePs4 = new Produto { Nome = "Console PlayStation 4", Preco = 1200 };
                    Produto geladeiraConsul = new Produto { Nome = "Geladeira Consul 540lts", Preco = 2220.50 };

                    Endereco costaAguiar = new Endereco("Rua Costa Aguiar", "São Paulo", "04020001");
                    Endereco ritaMariaDeJesus = new Endereco("Rua Rita Maria de Jesus", "São Paulo", "13995000");
                    Endereco torresDeOliveira = new Endereco("Torres de Oliveira", "São Paulo", "13995551");

                    Funcionario ricardo = new Funcionario() { Nome = "Ricardo", Sobrenome = "Oliveira", Endereco = costaAguiar };
                    Funcionario fernanda = new Funcionario { Nome = "Fernanda", Sobrenome = "Pimentel", Endereco = ritaMariaDeJesus };
                    Funcionario joao = new Funcionario { Nome = "João", Sobrenome = "Silva", Endereco = torresDeOliveira };
                    Funcionario maria = new Funcionario { Nome = "Maria", Sobrenome = "Fagundes", Endereco = costaAguiar };
                    Funcionario cristina = new Funcionario { Nome = "Cristina", Sobrenome = "Cavalcanti", Endereco = torresDeOliveira };

                    AdicionarProdutos(americanas, televisao, panelaEletrica, jogoDePanelas);
                    AdicionarProdutos(fnac, consolePs4, maquinaDeLavar, geladeiraConsul);

                    AdicionarFuncionario(americanas, ricardo, fernanda, joao);
                    AdicionarFuncionario(fnac, maria, cristina);

                    sessao.SaveOrUpdate(americanas);
                    sessao.SaveOrUpdate(fnac);

                    transacao.Commit();
                }

                using (sessao.BeginTransaction())
                {
                    IList<Loja> lojas = sessao.CreateCriteria(typeof(Loja)).List<Loja>();

                    foreach (Loja loja in lojas)
                        Retornar(loja);
                }

                Console.ReadKey();
            }
        }

        private static void Retornar(Loja loja)
        {
            Console.WriteLine(loja.Nome);
            Console.Write(" Produtos: ");

            foreach (Produto produto in loja.Produtos)
                Console.WriteLine(" " + produto.Nome);

            Console.WriteLine(" Funcionários: ");

            foreach (Funcionario funcionario in loja.Funcionarios)
                Console.WriteLine(" " + funcionario.Nome + " " + funcionario.Sobrenome);

            Console.WriteLine();
        }

        private static void AdicionarProdutos(Loja loja, params Produto[] produtos)
        {
            foreach (Produto produto in produtos)
                loja.AdicionarProduto(produto);
        }

        private static void AdicionarFuncionario(Loja loja, params Funcionario[] funcionarios)
        {
            foreach (Funcionario funcionario in funcionarios)
                loja.AdicionarFuncionario(funcionario);
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

        private static void CriarTabela()
        {
            Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2012
            .ConnectionString(x => x.FromConnectionStringWithKey("FluentNHibernateTeste-Local")))
            .ExposeConfiguration(y => BuildSchema(y))
            .Mappings(m =>
                m.FluentMappings.AddFromAssemblyOf<Program>())
            .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            StreamWriter texto = new StreamWriter(@"C:\Exemplos\schema.txt");
            new SchemaExport(config).Create(texto, true);
        }
    }
}
