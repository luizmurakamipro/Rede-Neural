using System;
using System.Collections.Generic;
using System.Text;

namespace Rede_Neural
{
    class Matriz
    {
        private int Linhas;
        private int Colunas;
        public int[,] Dados;

        public Matriz(int Linhas, int Colunas)
        {
            this.Linhas = Linhas;
            this.Colunas = Colunas;
            this.Dados = new int[Linhas, Colunas];

            Random rand = new Random();

            for (int i = 0; i < Linhas; i++)
            {
                for (int j = 0; j < Colunas; j++)
                    this.Dados[i, j] = rand.Next(1, 9);
            }
        }

        public static Matriz Add(Matriz A, Matriz B)
        {
            var matriz = new Matriz(A.Linhas, A.Colunas);

            for (int i = 0; i < A.Linhas; i++)
            {
                for (int j = 0; j < A.Colunas; j++)
                    matriz.Dados[i, j] = A.Dados[i, j] + B.Dados[i, j];
            }

            return matriz;
        }

        public static Matriz Multiply(Matriz A, Matriz B)
        {
            var matriz = new Matriz(A.Linhas, B.Colunas);

            for (int i = 0; i < A.Linhas; i++)
            {
                for (int j = 0; j < A.Colunas; j++)
                {
                    for (int k = 0; k < B.Colunas; k++)
                    {
                        int Elm1 = A.Dados[i, j];
                        int Elm2 = B.Dados[k, j];

                        matriz.Dados[i, k] = Elm1 * Elm2;
                    }
                }
            }

            return matriz;
        }

        public static void PrintMatriz(Matriz m)
        {
            for (int i = 0; i < m.Linhas; i++)
            {
                for (int j = 0; j < m.Colunas; j++)
                    Console.Write(m.Dados[i, j] + " ");

                Console.WriteLine();
            }
        }
    }
}
