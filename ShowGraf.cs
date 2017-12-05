using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace myGraf
{
    public partial class ShowGraf : Form
    {
        List<mNode> Graf;
        mMatrix2D<sbyte> Matrix;
        private bool mDrawNode;
        private sbyte mNumberOfDrawNode;
        public ShowGraf(int Height,int Width,List<mNode> Graf,mMatrix2D<sbyte> Matrix)
        {            
            InitializeComponent();
            this.Height = Height;
            this.Width = Width;
            this.Graf = Graf;
            this.Matrix = Matrix;
            mDrawNode = false;
            mNumberOfDrawNode = -1;
            mGrafRegion.Refresh();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void mGrafRegion_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < Matrix.Capacity; i++)
                for (int j = 0; j < i; j++)
                    if (Matrix[i, j] == 1)
                        e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Crimson), 2), Graf[i].X + 7, Graf[i].Y + 7, Graf[j].X + 7, Graf[j].Y + 7);
            int chars = 0;
            foreach (mNode mn in Graf)
            {
                mn.Draw(sender, e, chars);
                chars++;
            }
        }
        void mGrafRegion_MouseUp(object sender, MouseEventArgs e)
        {

            sbyte ResultOfFind = RegionOfWhichNode(e.X, e.Y);
            if (mDrawNode)
            {
                mDrawNode = false;
                mGrafRegion.Refresh();
                mNumberOfDrawNode = -1;
            }
        }
        void mGrafRegion_MouseDown(object sender, MouseEventArgs e)
        {
            sbyte ResultOfFind = RegionOfWhichNode(e.X, e.Y);
            if (ResultOfFind >= 0) { mNumberOfDrawNode = ResultOfFind; }
        }
        void mGrafRegion_MouseMove(object sender, MouseEventArgs e)
        {
            if (mNumberOfDrawNode >= 0)
            {
                mDrawNode = true;
                Graf[mNumberOfDrawNode].X = e.X - 5;
                Graf[mNumberOfDrawNode].Y = e.Y - 5;
                mGrafRegion.Refresh();
            }
        }
        private sbyte RegionOfWhichNode(int xCoord, int yCoord)
        {
            sbyte numberOfNode = 0;
            foreach (mNode mn in Graf)
            {
                if ((xCoord < mn.X + 25) && (mn.X - 25 < xCoord) && (yCoord < mn.Y + 25) && (mn.Y - 25 < yCoord)) return numberOfNode;
                numberOfNode++;
            }
            return -1;
        }
    }
}
