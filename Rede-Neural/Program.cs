using System;
using System.IO;
using System.Threading;

namespace Rede_Neural
{
    class Program
    {
        static void Main(string[] args)
        {
            bool Train = true;
            RedeNeural rn = new RedeNeural(2, 3, 1);
            Random rand = new Random();

            if (File.Exists("rede.txt"))
            {
                StreamReader sr = new StreamReader("rede.txt");

                string[] ArrayString = new string[2];
                int Count = 0;

                while (sr.EndOfStream != true)
                {
                    ArrayString[Count++] = sr.ReadLine();
                }

                var temp = ArrayString[0].Split(';');
                var Array1 = Array.ConvertAll<string, double>(temp, double.Parse);

                temp = ArrayString[1].Split(';');
                var Array2 = Array.ConvertAll<string, double>(temp, double.Parse);

                Matriz m1 = Matriz.ArrayToMatriz(Array1, 3);
                m1.Print();
                Matriz m2 = Matriz.ArrayToMatriz(Array2, 1);
                m2.Print();

                rn.SetPesoEntradaOculto(m1);
                rn.SetPesoOcultoSaida(m2);

                sr.Close();
             }

            int[] ArrayEntrada = new int[2] { 0, 0 };
            int[] ArrayEsperado = new int[2] { 1, 0 };

            // XOR Problem
            int[,] datasetEntrada = new int[4, 2]
            {
                {1, 1},
                {1, 0},
                {0, 1},
                {0, 0}
            };

            int[,] datasetEsperado = new int[4, 1]
            {
                {0 },
                {1 },
                {1 },
                {0 }
            };

            while (Train == true)
            {
                for (int i = 0; i < 10000; i++)
                {
                    int Linha = rand.Next(4);
                    int[] entradaArray = new int[2];
                    entradaArray[0] = datasetEntrada[Linha, 0];
                    entradaArray[1] = datasetEntrada[Linha, 1];

                    int[] esperadoArray = new int[1];
                    esperadoArray[0] = datasetEsperado[Linha, 0];

                    rn.Train(entradaArray, esperadoArray);
                }

                double[] predict1 = rn.Predict(ArrayEntrada);
                double[] predict2 = rn.Predict(ArrayEsperado);

                Console.WriteLine("Predict 1: " + predict1[0] + " - Predict 2: " + predict2[0]);

                if (predict1[0] < 0.04 && predict2[0] > 0.98)
                {
                    Train = false;
                    Console.WriteLine("Terminou!");

                    rn.GetPesoEntradaOculto().Print();
                    rn.GetPesoOcultoSaida().Print();

                    double[] PesoEntradaOculto = Matriz.MatrizToArray(rn.GetPesoEntradaOculto());
                    double[] PesoOcultoSaida = Matriz.MatrizToArray(rn.GetPesoOcultoSaida());

                    StreamWriter sm = new StreamWriter("rede.txt");

                    for (int i = 0; i < PesoEntradaOculto.Length; i++)
                    {
                        if (i == PesoEntradaOculto.Length - 1)
                            sm.Write(PesoEntradaOculto[i]);
                        else
                            sm.Write(PesoEntradaOculto[i] + ";");
                    }

                    sm.WriteLine();

                    for (int i = 0; i < PesoOcultoSaida.Length; i++)
                    {
                        if (i == PesoOcultoSaida.Length - 1)
                            sm.Write(PesoOcultoSaida[i]);
                        else
                            sm.Write(PesoOcultoSaida[i] + ";");
                    }

                    sm.Close();
                }
            }
        }
    }
}
