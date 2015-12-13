using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kcipher2Expr
{
    class Program
    {
        static void Main(string[] args)
        {
            KCipher2Tree cipher = new KCipher2Tree();
            cipher.Initialize();

            Console.Error.WriteLine("------");
            Console.Write(cipher);
            Console.ReadKey();
        }
    }
}
