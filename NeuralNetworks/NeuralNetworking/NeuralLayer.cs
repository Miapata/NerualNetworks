using System;
using System.Collections.Generic;

namespace NeuralNetworks
{
    // We stack a bunch of neurons in the form of layers to build a complex network
    public class NeuralLayer
    {
        public NeuralLayer(int count, double initialWeight, string name = "")
        {
            Neurons = new List<Neuron>();
            for (var i = 0; i < count; i++) Neurons.Add(new Neuron());

            Weight = initialWeight;

            Name = name;
        }

        public List<Neuron> Neurons { get; set; }

        public string Name { get; set; }

        public double Weight { get; set; }

        public void Compute(double learningRate, double delta)
        {
            foreach (var neuron in Neurons) neuron.Compute(learningRate, delta);
        }

        // Take cares of firing all the neuron in the layer and forward the input pulse to the next layer
        public void Forward()
        {
            foreach (var neuron in Neurons)
            {
                neuron.Fire();
            }
        }

        public void Optimize(double learningRate, double delta)
        {
            Weight += learningRate * delta;

            foreach (var neuron in Neurons)
            {
                neuron.UpdateWeights(Weight);
            }
        }

        public void Log()
        {
            Console.WriteLine("{0}, Weight: {1}", Name, Weight);
        }
    }
}