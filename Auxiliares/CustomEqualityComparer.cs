using FluentNHibertanteExemplo.Entidades;
using System.Collections;

namespace FluentNHibertanteExemplo.Auxiliares
{
    public class CustomEqualityComparer : IEqualityComparer
    {
        public bool Equals(object x, object y)
        {
            if (x == null || y == null)
                return false;

            if (x is Funcionario && y is Funcionario)
                return ((Funcionario)x).Id == ((Funcionario)y).Id;

            return x.Equals(y);
        }

        public int GetHashCode(object obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
