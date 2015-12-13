using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kcipher2Expr.Expression
{
    public class ConstValue : Expr
    {
        private uint value;

        private ConstValue() { }
        public ConstValue(uint value)
        {
            this.value = value;
        }

        public override uint Run(ExprManager manager)
        {
            return value;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
