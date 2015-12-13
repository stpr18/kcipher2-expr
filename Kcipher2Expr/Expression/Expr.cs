using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kcipher2Expr.Expression
{
    public abstract class Expr
    {
        private string toStringCache;

        protected Expr() { }

        public abstract uint Run(ExprManager manager);

        public static implicit operator Expr(uint i)
        {
            return new ConstValue(i);
        }

        public static Expr operator +(Expr x, Expr y)
        {
            return new AddOp(x, y);
        }

        public static Expr operator ^(Expr x, Expr y)
        {
            return new XorOp(x, y);
        }

        public string ToStringCache()
        {
            if (toStringCache == null)
            {
                toStringCache = ToString();
            }
            return toStringCache;
        }
    }
}
