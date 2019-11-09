using System;

namespace Rede_Neural
{
    class Program
    {
        static void Main(string[] args)
        {
            Matriz m1 = new Matriz(1, 2);
            Matriz m2 = new Matriz(2, 1);

            Matriz.PrintMatriz(m1);
            Matriz.PrintMatriz(m2);

            var m = Matriz.Multiply(m1, m2);

            Matriz.PrintMatriz(m);
        }
    }
}
