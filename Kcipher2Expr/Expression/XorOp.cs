using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kcipher2Expr.Expression
{
    public class XorOp : Expr
    {
        private Expr[] exprs;

        public XorOp(params Expr[] exprs)
        {
            this.exprs = exprs;
        }

        public override uint Run(ExprManager manager)
        {
            return exprs.Aggregate<Expr, uint>(0, (sum, next) => sum ^ next.Run(manager));
        }

        public override string ToString()
        {
            return "(" + string.Join<Expr>(" ^ ", exprs) + ")";
        }
    }
}
