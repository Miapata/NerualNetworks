using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks.NeuralNetworking
{
    // We stack a bunch of neurons in the form of layers to build a complex network
    class NeuralLayer
{
  public List<Neuron> Neurons { get; set; }

  public string Name { get; set;}

  public double Weight { get; set;}

  public NeuralLayer(int count, double initialWeight, string name = "")
  {
    Neurons = new List<Neuron>();
    for(int i=0;i<count;i++)
    {
      Neurons.Add(new Neuron());
    }
    
    Weight = initialWeight;
    
    Name = name;
  }

  public void Compute(double learningRate, double delta)
  {
    foreach (var neuron in Neurons)
    {
      neuron.Compute(learningRate, delta);
    }
  }

  public void Log()
  {
    Console.WriteLine("{0}, Weight: {1}", Name, Weight);
  }
}
    }
