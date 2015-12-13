using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kcipher2Expr.Expression
{
    public class FuncCall : Expr
    {
        private string name;
        private Expr[] exprs;

        private FuncCall() { }
        public FuncCall(string name, params Expr[] exprs)
        {
            this.name = name;
            this.exprs = exprs;
        }

        public override uint Run(ExprManager manager)
        {
            throw new InvalidOperationException();
        }

        public override string ToString()
        {
            return "{" + name + "}(" + string.Join<Expr>(", ", exprs) + ")";
        }
    }
}
