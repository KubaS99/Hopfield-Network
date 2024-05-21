using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hopfield3
{
    internal class Neuron
    {
        public int spin;
        public int xPos;
        public int yPos;
        public double[] wages;

        public Neuron(int xPos, int yPos)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            spin = -1;
        }
        public Neuron(int xPos, int yPos,int spin)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.spin = spin;
        }

        
    }
}
