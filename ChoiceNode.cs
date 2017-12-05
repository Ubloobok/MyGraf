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
    public partial class ChoiceNode : Form
    {
        public ChoiceNode(sbyte p_maximum)
        {          
            InitializeComponent();
            numericUpDown1.Maximum = p_maximum;
        }
        public ChoiceNode(string p_text,sbyte p_maximum)
        {
            InitializeComponent();
            label1.Text = p_text;
            label2.Visible = true;
            numericUpDown2.Visible = true;
            numericUpDown1.Maximum = p_maximum;
            numericUpDown2.Maximum = p_maximum;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) button1.PerformClick();
        }
        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) button1.PerformClick();
        }

        private void numericUpDown2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) button1.PerformClick();
        }
    }
}
