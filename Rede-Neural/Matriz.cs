using System;
using System.Collections.Generic;
using System.Text;

namespace Rede_Neural
{
    class Matriz
    {
        public int Linhas;
        public int Colunas;
        public double[][] Dados;

        public Matriz(int Linhas, int Colunas)
        {
            this.Linhas = Linhas;
            this.Colunas = Colunas;
            this.Dados = new double[Linhas][];
            for (int i = 0; i < this.Dados.Length; i++)
                this.Dados[i] = new double[Colunas];


            for (int i = 0; i < Linhas; i++)
            {
                for (int j = 0; j < Colunas; j++)
                    this.Dados[i][j] = 0;
            }
        }

        public void Print()
        {
            for (int i = 0; i < this.Linhas; i++)
            {
                for (int j = 0; j < this.Colunas; j++)
                    Console.Write(this.Dados[i][j] + " ");

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        // Aplica valores randomicos na matriz
        public void Randomize()
        {
            Random rand = new Random();

            for (int i = 0; i < this.Linhas; i++)
            {
                for (int j = 0; j < this.Colunas; j++)
                    this.Dados[i][j] = rand.NextDouble() * 2 - 1;
            }
        }

        // Faz a transposição da matriz de horizontal para vertical
        public static Matriz Transpose(Matriz A)
        {
            var matriz = new Matriz(A.Colunas, A.Linhas);

            for (int i = 0; i < A.Linhas; i++)
                for (int j = 0; j < A.Colunas; j++)
                    matriz.Dados[j][i] = A.Dados[i][j];

            return matriz;
        }

        public static Matriz EscalarMultiply(Matriz A, double escalar)
        {
            var matriz = new Matriz(A.Linhas, A.Colunas);

            for (int i = 0; i < A.Linhas; i++)
            {
                for (int j = 0; j < A.Colunas; j++)
                    matriz.Dados[i][j] = A.Dados[i][j] * escalar;
            }

            return matriz;
        }

        public static Matriz Hadamard(Matriz A, Matriz B)
        {
            var matriz = new Matriz(A.Linhas, A.Colunas);

            for (int i = 0; i < A.Linhas; i++)
            {
                for (int j = 0; j < A.Colunas; j++)
                    matriz.Dados[i][j] = A.Dados[i][j] * B.Dados[i][j];
            }

            return matriz;
        }

        public static Matriz Add(Matriz A, Matriz B)
        {
            var matriz = new Matriz(A.Linhas, A.Colunas);

            for (int i = 0; i < A.Linhas; i++)
            {
                for (int j = 0; j < A.Colunas; j++)
                    matriz.Dados[i][j] = A.Dados[i][j] + B.Dados[i][j];
            }

            return matriz;
        }

        public static Matriz Subtract(Matriz A, Matriz B)
        {
            var matriz = new Matriz(A.Linhas, A.Colunas);

            for (int i = 0; i < A.Linhas; i++)
            {
                for (int j = 0; j < A.Colunas; j++)
                    matriz.Dados[i][j] = A.Dados[i][j] - B.Dados[i][j];
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
                        double Elmt1 = A.Dados[i][j];
                        double Elmt2 = B.Dados[j][k];

                        Soma += Elmt1 * Elmt2;

                        matriz.Dados[i][k] = Soma;
                    }
                }
            }

            return matriz;
        }

        public static Matriz ArrayToMatriz(int[] array)
        {
            var matriz = new Matriz(array.Length, 1);

            for (int i = 0; i < matriz.Linhas; i++)
                for (int j = 0; j < matriz.Colunas; j++)
                    matriz.Dados[i][j] = array[i];

            return matriz;
        }

        public static Matriz ArrayToMatriz(double[] arr, int rows)
        {
            var cols = arr.Length / rows;
            var matriz = new Matriz(rows, cols);

            for (int i = 0; i < arr.Length; i++)
                matriz.Dados[i / cols][i % cols] = arr[i];

            return matriz;
        }

        public static double[] MatrizToArray(Matriz matriz)
        {
            double[] array = new double[matriz.Linhas * matriz.Colunas];

            int max = matriz.Linhas * matriz.Colunas;
            int k = 0;

            for (int i = 0; i < matriz.Linhas; i++)
                for (int j = 0; j < matriz.Colunas; j++)
                {
                    if (k < max)
                        array[k++] = matriz.Dados[i][j];
                    else
                        break;
                }

            return array;
        }

        public static float Sigmoid(double value)
        {
            return 1.0f / (1.0f + (float)Math.Exp(-value));
        }

        public static float DerivedSigmoid(double value)
        {
            return (float)value * (1.0f - (float)value);
        }

        public void MapSigmoid()
        {
            for (int i = 0; i < this.Linhas; i++)
                for (int j = 0; j < this.Colunas; j++)
                    this.Dados[i][j] = Sigmoid(this.Dados[i][j]);
        }

        public static Matriz MapDerivedSigmoid(Matriz m)
        {
            for (int i = 0; i < m.Linhas; i++)
                for (int j = 0; j < m.Colunas; j++)
                    m.Dados[i][j] = DerivedSigmoid(m.Dados[i][j]);

            return m;
        }
    }
}
