using System.Collections.Generic;

namespace NeuralNetworks
{
    public class Neuron
    {
        private double Weight;

        public Neuron()
        {
            Dendrites = new List<Dendrite>();
            OutputPulse = new Pulse();
        }

        public List<Dendrite> Dendrites { get; set; }

        public Pulse OutputPulse { get; set; }

        public void Fire()
        {
            OutputPulse.Value = Sum();
            OutputPulse.Value = Activation(OutputPulse.Value);
        }


        public void Compute(double learningRate, double delta)
        {
            Weight += learningRate * delta;
            foreach (var terminal in Dendrites) terminal.SynapticWeight = Weight;
        }

        private double Sum()
        {
            double computeValue = 0.0f;
            foreach (var terminal in Dendrites) computeValue += terminal.InputPulse.Value * terminal.SynapticWeight;

            return computeValue;
        }


        // Activates the function
        private double Activation(double input)
        {
            double threshold = 1;
            return input >= threshold ? 0 : threshold;
        }

        public void UpdateWeights(double new_weights)
        {
            foreach (var dendrite in Dendrites)
            {
                dendrite.SynapticWeight = new_weights;
            }
        }
    }
}