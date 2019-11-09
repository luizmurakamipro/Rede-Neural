using System;
using System.Collections.Generic;
using System.Text;

namespace Rede_Neural
{
    class Matriz
    {
        private int Linhas;
        private int Colunas;
        public double[,] Dados;

        public Matriz(int Linhas, int Colunas)
        {
            this.Linhas = Linhas;
            this.Colunas = Colunas;
            this.Dados = new double[Linhas, Colunas];

            for (int i = 0; i < Linhas; i++)
            {
                for (int j = 0; j < Colunas; j++)
                    this.Dados[i, j] = 0;
            }
        }

        public void Print()
        {
            for (int i = 0; i < this.Linhas; i++)
            {
                for (int j = 0; j < this.Colunas; j++)
                    Console.Write(this.Dados[i, j] + " ");

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public void Randomize()
        {
            Random rand = new Random();

            for (int i = 0; i < this.Linhas; i++)
            {
                for (int j = 0; j < this.Colunas; j++)
                    this.Dados[i, j] = rand.Next(0, 3) * 2 - 1;
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

            double Soma = 0;

            for (int i = 0; i < A.Linhas; i++)
            {
                for (int j = 0; j < A.Colunas; j++)
                {
                    for (int k = 0; k < B.Colunas; k++)
                    {
                        double Elmt1 = A.Dados[i, j];
                        double Elmt2 = B.Dados[j, k];

                        Soma += Elmt1 * Elmt2;
                    }
                }
            }

            matriz.Dados[0, 0] = Soma;

            return matriz;
        }

        public static Matriz ArrayToMatriz(int[] array)
        {
            var matriz = new Matriz(array.Length, 1);

            for (int i = 0; i < matriz.Linhas; i++)
                for (int j = 0; j < matriz.Colunas; j++)
                    matriz.Dados[i, j] = array[i];

            return matriz;
        }

        public static float Sigmoid(double value)
        {
            return 1.0f / (1.0f + (float)Math.Exp(-value));
        }

        public void MapSigmoid()
        {
            for (int i = 0; i < this.Linhas; i++)
                for (int j = 0; j < this.Colunas; j++)
                    this.Dados[i, j] = Sigmoid(this.Dados[i, j]);
        }
    }
}
