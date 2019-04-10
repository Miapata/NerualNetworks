using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using ConsoleTableExt;

namespace NeuralNetworks
{
    public class NetworkModel
    {
        // Constructor
        public NetworkModel()
        {
            Layers = new List<NeuralLayer>();
        }

        // List of neural layers
        public List<NeuralLayer> Layers { get; set; }


        // Add a new layer
        public void AddLayer(NeuralLayer layer)
        {
            var dendriteCount = 1;

            if (Layers.Count > 0) dendriteCount = Layers.Last().Neurons.Count;

            foreach (var element in layer.Neurons)
                for (var i = 0; i < dendriteCount; i++)

                    element.Dendrites.Add(new Dendrite());
        }
        // Helper function which will execute the Forward method for all the layers and calculate the output
        private void ComputeOutput()
        {
            bool first = true;
            foreach (var layer in Layers)
            {
                // Skip the first layer as it is input
                if (first)
                {
                    first = false;
                    continue;
                }

                layer.Forward();
            }
        }

        // Build our layers or layer
        public void Build()
        {
            var i = 0;

            foreach (var layer in Layers)
            {
                if (i >= Layers.Count - 1) break;

                var nextLayer = Layers[i + 1];
                CreateNetwork(layer, nextLayer);
            }
        }

        private void OptimizeWeights(double accuracy)
        {
            double lr = 0.1;

            // Skip if the accuracy reached 100%
            if (accuracy == 1)
            {
                return;
            }

            if (accuracy > 1)
            {
                lr = -lr;
            }

            // Update the weights for all the layers

            foreach (var layer in Layers)
            {
                layer.Optimize(lr,1);
            }
            
        }
        public void Print()
        {
            // Create a new data table
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Neurons");
            dt.Columns.Add("Weight");

            //

            foreach (var element in Layers)
            {
                var row = dt.NewRow();
                row[0] = element.Name;
                row[1] = element.Neurons.Count;
                row[2] = element.Weight;

                dt.Rows.Add(row);
            }

            var builder = ConsoleTableBuilder.From(dt);
            builder.ExportAndWrite();
        }

        public void Train(NeuralData X, NeuralData Y, int iterations, double learningRate=0.1 )
        {
            int epoch = 1;

            
            while (iterations >= epoch)
            {
                var inputLayer = Layers[0];
                List<double> outputs = new List<double>();
                for (int i = 0; i < X.Data.Length; i++)
                {
                    for (int j = 0; j < X.Data[i].Length; j++)
                    {
                        inputLayer.Neurons[j].OutputPulse.Value = X.Data[i][j];
                    }

                    ComputeOutput();
                    outputs.Add(Layers.Last().Neurons.First().OutputPulse.Value);
                }

                double accuracySum = 0;
                int y_counter = 0;

                outputs.ForEach((x) =>
                {
                    if (x == Y.Data[y_counter].First())
                    {
                        accuracySum++;
                    }

                    y_counter++;
                });

                OptimizeWeights(accuracySum/y_counter);

                Console.WriteLine("Epoch: {0}, Accuracy: {1} %",epoch, accuracySum /y_counter*100);
                epoch++;
            }
        }
        public void CreateNetwork(NeuralLayer connectingFrom, NeuralLayer connectingTo)
        {
            foreach (var to in connectingTo.Neurons)
                foreach (var from in connectingFrom.Neurons)
                    to.Dendrites.Add(new Dendrite { InputPulse = to.OutputPulse, SynapticWeight = connectingTo.Weight });
        }
    }
}