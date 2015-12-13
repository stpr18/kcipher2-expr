using Kcipher2Expr.Expression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kcipher2Expr
{
    class KCipher2Tree
    {
        private State currentState;

        public void Initialize()
        {
            Console.Error.WriteLine("--- Initialize ---");

            var key = ConstLiteral.MakeArray("K", 12);
            var iv = ConstLiteral.MakeArray("IV", 4);

            currentState = new State(key, iv);

            for (int i = 0; i < 24; ++i)
            {
                Console.Error.WriteLine(i + "Round");
                Next(true);
                ToString();
            }
        }

        public void Next(bool init)
        {
            currentState = new State(currentState, init);
        }

        public override string ToString()
        {
            return currentState.ToString();
        }

        private sealed class State
        {
            public readonly Expr[] RegA;
            public readonly Expr[] RegB;
            public readonly Expr[] RegR;
            public readonly Expr[] RegL;

            private State()
            {
                throw new InvalidOperationException();
            }

            public State(Expr[] k, Expr[] iv)
            {
                RegA = new Expr[5] { k[4], k[3], k[2], k[1], k[0] };
                RegB = new Expr[11] { k[10], k[11], iv[0], iv[1], k[8], k[9], iv[2], iv[3], k[7], k[5], k[6] };
                RegR = new Expr[2] { 0, 0 };
                RegL = new Expr[2] { 0, 0 };
            }

            public State(State old, bool init)
            {
                RegA = new Expr[5] {
                    old.RegA[1],
                    old.RegA[2],
                    old.RegA[3],
                    old.RegA[4],
                    old.RegA[3] ^ (new FuncCall("a0", old.RegA[0]))
                };
                if (init)
                {
                    RegA[4] ^= old.KeyStreamLow();
                }
                RegB = new Expr[11] {
                    old.RegB[1],
                    old.RegB[2],
                    old.RegB[3],
                    old.RegB[4],
                    old.RegB[5],
                    old.RegB[6],
                    old.RegB[7],
                    old.RegB[8],
                    old.RegB[9],
                    old.RegB[10],
                    new XorOp(old.RegB[1], old.RegB[6], new FuncCall("a1 or a2", old.RegB[0], old.RegA[2]), new FuncCall("1 or a3", old.RegB[8], old.RegA[2]))
                };
                if (init)
                {
                    RegB[10] ^= old.KeyStreamHigh();
                }
                RegR = new Expr[2] { old.RegL[1] + old.RegB[9], old.RegR[0] };
                RegL = new Expr[2] { old.RegR[1] + old.RegB[4], old.RegL[0] };
            }

            public Expr KeyStreamLow()
            {
                return new XorOp(RegB[0] + RegR[1], RegR[0], RegA[4]);
            }

            public Expr KeyStreamHigh()
            {
                return new XorOp(RegB[10] + RegL[1], RegL[0], RegA[0]);
            }

            public override string ToString()
            {
                StringBuilder builder = new StringBuilder();
                
                for (int i = 0; i < RegA.Length; ++i)
                {
                    builder.Append("A[");
                    builder.Append(i);
                    builder.Append("]:");
                    builder.Append(RegA[i].ToStringCache());
                    builder.AppendLine();
                }

                for (int i = 0; i < RegB.Length; ++i)
                {
                    builder.Append("B[");
                    builder.Append(i);
                    builder.Append("]:");
                    builder.Append(RegB[i].ToStringCache());
                    builder.AppendLine();
                }

                for (int i = 0; i < RegR.Length; ++i)
                {
                    builder.Append("R[");
                    builder.Append(i);
                    builder.Append("]:");
                    builder.Append(RegR[i].ToStringCache());
                    builder.AppendLine();
                }

                for (int i = 0; i < RegL.Length; ++i)
                {
                    builder.Append("L[");
                    builder.Append(i);
                    builder.Append("]:");
                    builder.Append(RegL[i].ToStringCache());
                    builder.AppendLine();
                }

                return builder.ToString();
            }
        }
    }
}
