using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LukeSimple
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void buttonFromFile_Click(object sender, EventArgs e)
		{
			openFileDialog1.ShowDialog();
		}

		private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
		{
			textBoxN.Text = new System.IO.StreamReader(openFileDialog1.FileName).ReadToEnd();
		}

		private void buttonCalculate_Click(object sender, EventArgs e)
		{
			Calculate();
		}

		private void Calculate()
		{
			
		}
	}
}
