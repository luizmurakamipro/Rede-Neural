using System;
using System.Collections.Generic;
using System.Text;

namespace Rede_Neural
{
    class RedeNeural
    {
        private int Entrada, Oculto, Saida;

        private Matriz BIAS_EntradaOculto;
        private Matriz BIAS_OcultoSaida;

        private Matriz Pesos_EntradaOculto;
        private Matriz Pesos_OcultoSaida;

        public RedeNeural(int Entrada, int Oculto, int Saida)
        {
            this.Entrada = Entrada;
            this.Oculto = Oculto;
            this.Saida = Saida;

            this.BIAS_EntradaOculto = new Matriz(this.Oculto, 1);
            this.BIAS_EntradaOculto.Randomize();
            this.BIAS_OcultoSaida = new Matriz(this.Saida, 1);
            this.BIAS_OcultoSaida.Randomize();

            this.Pesos_EntradaOculto = new Matriz(this.Oculto, this.Entrada);
            this.Pesos_EntradaOculto.Randomize();

            this.Pesos_OcultoSaida = new Matriz(this.Saida, this.Oculto);
            this.Pesos_OcultoSaida.Randomize();
        }

        public void FeedFoward(int[] Entrada)
        {
            // Entrada para Oculta
            Matriz matrizEntrada = Matriz.ArrayToMatriz(Entrada);
            Matriz matrizOculta = Matriz.Multiply(this.Pesos_EntradaOculto, matrizEntrada);
            matrizOculta = Matriz.Add(matrizOculta, this.BIAS_EntradaOculto);
            matrizOculta.MapSigmoid();

            // Oculta para Saida
            Matriz matrizSaida = Matriz.Multiply(this.Pesos_OcultoSaida, matrizOculta);
            matrizSaida = Matriz.Add(matrizSaida, this.BIAS_OcultoSaida);
            matrizSaida.MapSigmoid();

            matrizSaida.Print();
        }
    }
}
