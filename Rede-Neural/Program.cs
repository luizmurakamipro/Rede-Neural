using System;

namespace Rede_Neural
{
    class Program
    {
        static void Main(string[] args)
        {
            RedeNeural rn = new RedeNeural(1, 3, 5);
            int[] arr = new int[2] { 1, 2 };

            rn.FeedFoward(arr);
        }
    }
}
