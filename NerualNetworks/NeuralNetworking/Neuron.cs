using System;
using System.Collections.Generic;
using NeuralNetworks.NeuralNetworking;

namespace NerualNetworks.NeuralNetworking
{
    class Neuron
    {
        public List<Dendrite> Dendrites { get; set; }

        public Pulse OutputPulse { get; set; }

        public double Weight;

        public Neuron()
        {
            Dendrites = new List<Dendrite>();
            OutputPulse = new Pulse();
        }

        public void Fire()
        {
            OutputPulse.Value = Sum();
            OutputPulse.Value = Activation(OutputPulse.Value);
        }



        private double Sum()
        {
            double computeValue = 0.0f;
            foreach (var terminal in Dendrites)
            {
                computeValue += terminal.InputPulse.Value * terminal.SynapticWeight;
            }

            return computeValue;
        }

        // Activates the function
        private double Activation(double input)
        {
            double threshold = 1;
            return input >= threshold ? 0 : threshold;
        }
    }
}
