using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kcipher2Expr.Expression
{
    public class ConstLiteral : Expr
    {
        private string name;

        private ConstLiteral() { }
        public ConstLiteral(string name)
        {
            this.name = name;
        }

        public override uint Run(ExprManager manager)
        {
            throw new InvalidOperationException();
        }

        public override string ToString()
        {
            return name;
        }

        public static ConstLiteral[] MakeArray(string name, int size)
        {
            var array = new ConstLiteral[size];
            for (int i = 0; i < size; ++i)
            {
                array[i] = new ConstLiteral(name + i);
            }
            return array;
        }
    }
}
