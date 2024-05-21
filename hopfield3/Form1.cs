using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hopfield3
{
    public partial class Form1 : Form
    {
        Network network;
        int grid = 8;
        int size = 60;
        bool isPattern = false;
        int patternCounter = 0;
        
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Refresh();
            network = new Network(8,8);
            
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
            for (int i=0;i<network.neurons.Count;i++)
            {
                if (network.neurons[i].spin==1)
                {
                    e.Graphics.FillRectangle(Brushes.Gray, network.neurons[i].xPos * size, network.neurons[i].yPos * size, size, size);
                }
            }
            
            for(int i=size;i<=480;i+=size)
            {
                e.Graphics.DrawLine(Pens.Black, i, 0, i, 480);
                e.Graphics.DrawLine(Pens.Black, 0, i, 480, i);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            int xIndex = coordinates.X / size;
            int yIndex = coordinates.Y / size;
            Neuron neuron = network.GetNeuron(xIndex, yIndex);
            network.SetNeuron(xIndex, yIndex, neuron.spin * -1);
            isPattern = false;
            pictureBox1.Refresh();
        }

        private void randomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            for (int i = 0; i < network.neurons.Count; i++)
            {
                if(r.Next()%2==1)
                {
                    network.neurons[i].spin = 1;
                }
                else
                {
                    network.neurons[i].spin = -1;
                }
                
            }
            pictureBox1.Refresh();
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            testButton.Enabled = false;
            network.Test(pictureBox1,(int)delayNumeric.Value);
            testButton.Enabled = true;
        }


        private void storePatternToolStripMenuItem_Click(object sender, EventArgs e)
        {
            network.AddPattern();
            network.ShowAllPatterns();
            patternsLabel.Text = "Stored Patterns: " + network.PatternsCount();
            network.ClearNetwork();
            isPattern = false;
            patternCounter = 0;
            pictureBox1.Refresh();
            testButton.Enabled = false;
        }


        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            network.ClearNetwork();
            isPattern = false;
            patternCounter = 0;
            pictureBox1.Refresh();
        }


        private void trainButton_Click(object sender, EventArgs e)
        {
            network.Train();
            testButton.Enabled = true;
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            pictureBox1.Refresh();
        }

        private void ChangeGrid()
        {
            network = new Network(grid, grid);
            patternsLabel.Text = "Stored Patterns: 0";
            pictureBox1.Refresh();
            patternCounter = 0;
            isPattern = false;
            testButton.Enabled = false;
        }
        private void x6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grid = 6;
            size = 80;
            capacityLabel.Text = "Patterns capacity: ~5";
            ChangeGrid();
        }

        private void x8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grid = 8;
            size = 60;
            capacityLabel.Text = "Patterns capacity: ~9";
            ChangeGrid();
        }

        private void x10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grid = 10;
            size = 48;
            capacityLabel.Text = "Patterns capacity: ~14";
            ChangeGrid();
        }

        private void DrawPattern(int pattern)
        {
            List<int> indexes=new List<int>();
            switch (grid)
            {
                case 6:
                    switch(pattern)
                    {
                        case 0:
                            indexes = new List<int> { 2, 3, 7, 10, 13, 16, 19, 22, 25, 28, 32, 33 };
                            break;
                        case 1:
                            indexes = new List<int> { 3, 8, 9, 13, 15, 21, 27, 33 };
                            break;
                        case 2:
                            indexes = new List<int> { 2, 7, 9, 15, 20, 25, 31, 32, 33 };
                            break;
                        case 3:
                            indexes = new List<int> { 2, 3, 4, 5, 11, 15, 16, 17, 23, 29, 32, 33, 34, 35 };
                            break;
                        case 4:
                            indexes = new List<int> { 1, 4, 7, 10, 13, 16, 19, 20, 21, 22, 28, 34 };
                            break;
                        case 5:
                            indexes = new List<int> { 1, 2, 3, 4, 7, 13, 14, 15, 16, 22, 28, 31, 32, 33, 34 };
                            break;
                        case 6:
                            indexes = new List<int> { 1, 2, 3, 4, 7, 13, 19, 20, 21, 22, 25, 28, 31, 32, 33, 34 };
                            break;
                        case 7:
                            indexes = new List<int> { 1, 2, 3, 4, 10, 16, 22, 28, 34 };
                            break;
                        case 8:
                            indexes = new List<int> { 2, 3, 7, 10, 14, 15, 19, 22, 25, 28, 32, 33 };
                            break;
                        case 9:
                            indexes = new List<int> { 0, 1, 2, 3, 6, 9, 12, 13, 14, 15, 21, 27, 33 };
                            break;
                    }
                    break;
                case 8:
                    switch (pattern)
                    {
                        case 0:
                            indexes = new List<int> { 2, 3, 4, 5, 9, 14, 17, 22, 25, 30, 33, 38, 41, 46, 49, 54, 58, 59, 60, 61 };
                            break;
                        case 1:
                            indexes = new List<int> { 3, 4, 10, 11, 12, 17, 18, 19, 20, 27, 28, 35, 36, 43, 44, 51, 52, 59, 60 };
                            break;
                        case 2:
                            indexes = new List<int> { 1, 2, 3, 4, 5, 8, 9, 10, 11, 12, 13, 14, 21, 22, 24, 25, 26, 27, 28, 29, 30, 32, 33, 34, 35, 36, 37, 40, 41, 48, 49, 50, 51, 52, 53, 54, 56, 57, 58, 59, 60, 61, 62 };
                            break;
                        case 3:
                            indexes = new List<int> { 1, 2, 3, 4, 5, 6, 9, 10, 11, 12, 13, 14, 15, 22, 23, 27, 28, 29, 30, 31, 35, 36, 37, 38, 39, 46, 47, 49, 50, 51, 52, 53, 54, 55, 57, 58, 59, 60, 61, 62 };
                            break;
                        case 4:
                            indexes = new List<int> { 1, 5, 6, 9, 13, 14, 17, 21, 22, 25, 26, 27, 28, 29, 30, 33, 34, 35, 36, 37, 38, 45, 46, 53, 54, 61, 62 };
                            break;
                        case 5:
                            indexes = new List<int> { 1, 2, 3, 4, 5, 6, 9, 10, 11, 12, 13, 14, 17, 25, 26, 27, 28, 29, 30, 33, 34, 35, 36, 37, 38, 46, 49, 50, 51, 52, 53, 54, 57, 58, 59, 60, 61 };
                            break;
                        case 6:
                            indexes = new List<int> { 2, 3, 4, 5, 6, 10, 14, 18, 26, 34, 35, 36, 37, 38, 42, 46, 50, 54, 58, 59, 60, 61, 62 };
                            break;
                        case 7:
                            indexes = new List<int> { 2, 3, 4, 5, 9, 10, 11, 12, 13, 14, 21, 22, 29, 30, 37, 38, 45, 46, 53, 54, 61, 62 };
                            break;
                        case 8:
                            indexes = new List<int> { 2, 3, 4, 5, 9, 14, 17, 22, 26, 27, 28, 29, 33, 38, 41, 46, 49, 54, 58, 59, 60, 61 };
                            break;
                        case 9:
                            indexes = new List<int> { 1, 2, 3, 4, 5, 9, 13, 17, 21, 25, 26, 27, 28, 29, 37, 45, 53, 58, 59, 60, 61 };
                            break;
                    }
                    break;
                case 10:
                    switch (pattern)
                    {
                        case 0:
                            indexes = new List<int> { 2, 3, 4, 5, 6, 7, 11, 18, 21, 28, 31, 38, 41, 48, 51, 58, 61, 68, 71, 78, 81, 88, 92, 93, 94, 95, 96, 97 };
                            break;
                        case 1:
                            indexes = new List<int> { 4, 5, 13, 14, 15, 22, 23, 24, 25, 31, 32, 34, 35, 44, 45, 54, 55, 64, 65, 74, 75, 84, 85, 94, 95 };
                            break;
                        case 2:
                            indexes = new List<int> { 3, 4, 5, 6, 12, 13, 16, 17, 22, 27, 36, 37, 45, 46, 54, 55, 63, 64, 72, 73, 82, 92, 93, 94, 95, 96, 97 };
                            break;
                        case 3:
                            indexes = new List<int> { 3, 4, 5, 6, 12, 13, 16, 17, 22, 27, 28, 37, 38, 45, 46, 47, 55, 56, 57, 67, 68, 72, 77, 78, 82, 83, 86, 87, 93, 94, 95, 96 };
                            break;
                        case 4:
                            indexes = new List<int> { 1, 2, 5, 6, 11, 12, 15, 16, 21, 22, 25, 26, 31, 32, 35, 36, 41, 42, 45, 46, 51, 52, 53, 54, 55, 56, 57, 62, 63, 64, 65, 66, 67, 75, 76, 85, 86, 95, 96 };
                            break;
                        case 5:
                            indexes = new List<int> { 2, 3, 4, 5, 6, 7, 12, 22, 32, 42, 43, 44, 45, 46, 57, 67, 77, 87, 92, 93, 94, 95, 96 };
                            break;
                        case 6:
                            indexes = new List<int> { 2, 3, 4, 5, 6, 11, 17, 21, 31, 41, 51, 52, 53, 54, 55, 56, 61, 67, 71, 77, 81, 87, 92, 93, 94, 95, 96 };
                            break;
                        case 7:
                            indexes = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 18, 27, 28, 37, 46, 47, 56, 65, 66, 75, 84, 85, 94 };
                            break;
                        case 8:
                            indexes = new List<int> { 3, 4, 5, 6, 12, 17, 22, 27, 32, 37, 43, 44, 45, 46, 52, 57, 62, 67, 72, 77, 82, 87, 93, 94, 95, 96 };
                            break;
                        case 9:
                            indexes = new List<int> { 2, 3, 4, 5, 6, 7, 11, 18, 21, 28, 31, 38, 41, 48, 52, 53, 54, 55, 56, 57, 58, 68, 78, 88, 92, 93, 94, 95, 96, 97 };
                            break;
                    }
                    break;
            }
            network.ClearNetwork();
            foreach(var i in indexes)
            {
                network.neurons[i].spin = 1;
            }
            pictureBox1.Refresh();
            
            
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DrawPattern(0);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DrawPattern(1);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            DrawPattern(2);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            DrawPattern(3);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            DrawPattern(4);
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            DrawPattern(5);
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            DrawPattern(6);
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            DrawPattern(7);
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            DrawPattern(8);
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            DrawPattern(9);
        }

        private void nextPatternToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(isPattern)
            {
                patternCounter++;
                network.GetPattern(patternCounter % network.PatternsCount());
            }
            else
            {
                isPattern = true;
                patternCounter = 0;
                network.GetPattern(0);
            }
            pictureBox1.Refresh();
        }

        private void deletePatternToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(isPattern)
            {
                network.RemovePattern(patternCounter % network.PatternsCount());
                testButton.Enabled = false;
                patternsLabel.Text = "Stored Patterns: " + network.PatternsCount();
            }
            else
            {
                MessageBox.Show("Pattern not found in stored patterns!");
            }
        }



        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    AllocConsole();
        //}
        //[DllImport("kernel32.dll", SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool AllocConsole();

        //private void TextPattern(object sender, EventArgs e)
        //{
        //    for(int i=0;i<network.neurons.Count;i++)
        //    {
        //        if (network.neurons[i].spin==1)
        //        {
        //            Console.Write(i + ", ");
        //        }
        //    }
        //    Console.WriteLine();
        //}
    }
}
