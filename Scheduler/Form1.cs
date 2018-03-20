using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Scheduler
{
    public partial class Form1 : Form
    {
        Scheduler _scheduler = new Scheduler();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_scheduler.IsDue(8, 0, 17, 0, 300))
                MessageBox.Show("Due Now!");
        }
    }
}
