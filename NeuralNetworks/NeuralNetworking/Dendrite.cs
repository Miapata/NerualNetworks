using System;
using System.Collections.Generic;
using NeuralNetworks.NeuralNetworking;

namespace NeuralNetworks.NeuralNetworking
{
    class Dendrite
    {
        public Pulse InputPulse { get; set; }
        public double SynapticWeight { get; set; }

        public bool Learnable { get; set; } = true;
    }
}
