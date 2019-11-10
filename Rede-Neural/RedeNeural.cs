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

        private double TaxaAprendizado;

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

            this.TaxaAprendizado = 0.1;
        }

        public void Train(int[] ArrayEntrada, int[] ArraySaida)
        {
            // Entrada para Oculta
            Matriz matrizEntrada = Matriz.ArrayToMatriz(ArrayEntrada);
            Matriz matrizOculta = Matriz.Multiply(this.Pesos_EntradaOculto, matrizEntrada);
            matrizOculta = Matriz.Add(matrizOculta, this.BIAS_EntradaOculto);
            matrizOculta.MapSigmoid();

            // Oculta para Saida
            Matriz matrizSaida = Matriz.Multiply(this.Pesos_OcultoSaida, matrizOculta);
            matrizSaida = Matriz.Add(matrizSaida, this.BIAS_OcultoSaida);
            matrizSaida.MapSigmoid();

            // BackPropagation ->

            // Saida para Oculta
            Matriz matrizEsperada = Matriz.ArrayToMatriz(ArraySaida);
            Matriz matrizErroSaida = Matriz.Subtract(matrizEsperada, matrizSaida);
            Matriz matrizSaidaDerivada = Matriz.MapDerivedSigmoid(matrizSaida);
            Matriz matrizOcultaTransposta = Matriz.Transpose(matrizOculta);

            Matriz matrizGradiente = Matriz.Hadamard(matrizSaidaDerivada, matrizErroSaida);
            matrizGradiente = Matriz.EscalarMultiply(matrizGradiente, this.TaxaAprendizado);

            // Ajustar a BIAS
            this.BIAS_OcultoSaida = Matriz.Add(this.BIAS_OcultoSaida, matrizGradiente);

            // Ajustar os Pesos
            Matriz matrizDeltaPesoOcultaSaida = Matriz.Multiply(matrizGradiente, matrizOcultaTransposta);
            this.Pesos_OcultoSaida = Matriz.Add(this.Pesos_OcultoSaida, matrizDeltaPesoOcultaSaida);

            // Oculta para Entrada
            Matriz matrizPesoSaidaOcultaTransposta = Matriz.Transpose(this.Pesos_OcultoSaida);
            Matriz matrizErroOculta = Matriz.Multiply(matrizPesoSaidaOcultaTransposta, matrizErroSaida);
            Matriz matrizOcultaDerivada = Matriz.MapDerivedSigmoid(matrizOculta);
            Matriz matrizEntradaTransposta = Matriz.Transpose(matrizEntrada);

            Matriz matrizOcultaGradiente = Matriz.Hadamard(matrizOcultaDerivada, matrizErroOculta);
            matrizOcultaGradiente = Matriz.EscalarMultiply(matrizOcultaGradiente, this.TaxaAprendizado);

            // Ajustar a BIAS
            this.BIAS_EntradaOculto = Matriz.Add(this.BIAS_EntradaOculto, matrizOcultaGradiente);

            // Ajustar os Pesos
            Matriz matrizDeltaPesoEntradaOculto = Matriz.Multiply(matrizOcultaGradiente, matrizEntradaTransposta);
            this.Pesos_EntradaOculto = Matriz.Add(this.Pesos_EntradaOculto, matrizDeltaPesoEntradaOculto);
        }

        public double[] Predict(int[] ArrayEntrada)
        {
            // Entrada para Oculta
            Matriz matrizEntrada = Matriz.ArrayToMatriz(ArrayEntrada);

            Matriz matrizOculta = Matriz.Multiply(this.Pesos_EntradaOculto, matrizEntrada);
            matrizOculta = Matriz.Add(matrizOculta, this.BIAS_EntradaOculto);
            matrizOculta.MapSigmoid();

            // Oculta para Saida
            Matriz matrizSaida = Matriz.Multiply(this.Pesos_OcultoSaida, matrizOculta);
            matrizSaida = Matriz.Add(matrizSaida, this.BIAS_OcultoSaida);
            matrizSaida.MapSigmoid();
            double[] ArraySaida = Matriz.MatrizToArray(matrizSaida);

            return ArraySaida;
        }
    }
}
