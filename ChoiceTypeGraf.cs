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
    public partial class ChoiceTypeGraf : Form
    {
        public ChoiceTypeGraf()
        {
            InitializeComponent();
        }

        private void ChoiceTypeGraf_Load(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
