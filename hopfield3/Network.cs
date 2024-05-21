using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace hopfield3
{
    internal class Network
    {
        List<List<Neuron>> patterns;
        public List<Neuron> neurons = new List<Neuron>();
        public Network(int xGrid, int yGrid)
        {
            patterns = new List<List<Neuron>>();
            for (int i = 0; i < yGrid; i++)
            {
                for (int j = 0; j < xGrid; j++)
                {
                    neurons.Add(new Neuron(j, i));
                }
            }
        }
        public int PatternsCount()
        {
            return patterns.Count;
        }

        public void RemovePattern(int index)
        {
            patterns.RemoveAt(index);
        }
        public void SetNeuron(int xPos, int yPos, int spin)
        {
            foreach (Neuron neuron in neurons)
            {
                if (neuron.xPos == xPos && neuron.yPos == yPos)
                {
                    neuron.spin = spin;
                }
            }
        }
        public Neuron GetNeuron(int xPos, int yPos)
        {
            foreach (Neuron neuron in neurons)
            {
                if (neuron.xPos == xPos && neuron.yPos == yPos)
                {
                    return neuron;
                }
            }
            return null;
        }
        public void AddPattern()
        {
            List<Neuron> tmp = new List<Neuron>();
            foreach (Neuron neuron in neurons)
            {
                tmp.Add(new Neuron(neuron.xPos, neuron.yPos, neuron.spin));
            }
            patterns.Add(tmp);
        }
        public void GetPattern(int index)
        {
            for (int i = 0; i < neurons.Count; i++)
            {
                neurons[i].spin = patterns[index][i].spin;
            }
        }

        public void ShowAllPatterns()
        {
            for(int i=0;i<patterns.Count;i++)
            {
                Console.WriteLine(i);
                DisplayNeurons(patterns[i]);
            }
        }
        private void DisplayNeurons(List<Neuron> list)
        {
            Console.WriteLine(list.Count+" neurons");
            for(int i=0; i<list.Count; i++)
            {
                Console.Write(list[i].spin + " ");
            }
            Console.WriteLine("\n\n\n");
        }

        public void Train()
        {

            for (int k = 0; k < neurons.Count; k++)
            {
                double[] tmp = new double[neurons.Count];

                foreach (var pattern in patterns)
                {
                    for (int i = 0; i < pattern.Count; i++)
                    {
                        if (neurons[k].xPos == pattern[i].xPos && neurons[k].yPos == pattern[i].yPos)
                        {
                            continue;
                        }
                        else
                        {
                            tmp[i] += pattern[k].spin * pattern[i].spin;
                        }
                    }
                }
                double x = neurons.Count;
                for (int i = 0; i < tmp.Length; i++)
                {
                    tmp[i] /= x;
                }
                neurons[k].wages = tmp;

            }
        }

        private List<Neuron> CopyNetwork()
        {
            List<Neuron> copy = new List<Neuron>();
            for (int i = 0; i < neurons.Count; i++)
            {
                copy.Add(new Neuron(neurons[i].xPos, neurons[i].yPos, neurons[i].spin));
            }
            return copy;
        }

        private List<int> Perm()
        {
            List<int> values = new List<int>();
            for (int i = 0; i < neurons.Count; i++)
            {
                values.Add(i);
            }

            Random rand = new Random();
            var shuffled = values.OrderBy(_ => rand.Next()).ToList();

            return shuffled;
        }

        private int Field(int index)
        {
            double[] wages = neurons[index].wages;
            double sum = 0;
            for (int i = 0; i < neurons.Count; i++)
            {
                if (i != index)
                {
                    sum += wages[i] * neurons[i].spin;
                }
            }
            return Math.Sign(sum);
        }
        public bool SameNetworks(List<Neuron> copy)
        {
            bool same = true;
            for(int i=0; i < neurons.Count; i++)
            {
                if(neurons[i].spin != copy[i].spin)
                {
                    same = false;
                    break;
                }
            }
            return same;
        }

        public void Test(PictureBox pictureBox, int delay)
        {
            List<Neuron> copy = new List<Neuron>();

            while(true)
            {
                copy = CopyNetwork();
                var perms = Perm();
                for (int i = 0; i < neurons.Count; i++)
                {
                    neurons[perms[i]].spin = Field(perms[i]);
                    pictureBox.Refresh();
                    Thread.Sleep(delay);
                }
                if(SameNetworks(copy))
                    break;
            }

        }

        public void Test2(PictureBox pictureBox, int delay)
        {
            List<Neuron> copy = new List<Neuron>();
            int[] spins = new int[neurons.Count];
            while (true)
            {
                copy = CopyNetwork();
                for (int i= 0;i<neurons.Count;i++)
                {
                    spins[i] = Field(i);
                }
                for (int i = 0; i < neurons.Count; i++)
                {
                    neurons[i].spin = spins[i];
                }
                pictureBox.Refresh();
                Thread.Sleep(delay);
                
                if (SameNetworks(copy))
                    break;
            }

        }

        public void ClearNetwork()
        {
            for(int i=0;i<neurons.Count; i++)
            {
                neurons[i].spin = -1;
            }
        }
    }
}
