using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Luke
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
			labelAnswer.Text = "";
			Calculate();
		}

		private void Calculate()
		{
			// чтение чисел
			BigInt m = new BigInt(textBoxN.Text);
			List<BigInt> q = new List<BigInt>();

			string temp = "";
			for(int i=0; i<textBoxNMinus1.Text.Length; i++)
			{
				if (Char.IsDigit(textBoxNMinus1.Text[i]))
					temp += textBoxNMinus1.Text[i];
				else if (textBoxNMinus1.Text[i] == ' ')
				{
					q.Add(new BigInt(temp));
					temp = "";
				}
			}
			if (temp != "")
				q.Add(new BigInt(temp));

			// 1)
			int c = 20, k = 0;

			Random random = new Random();

			while (true)
			{
				// 2)
				int max;
				if (m >= int.MaxValue)
					max = int.MaxValue;
				else
					max = int.Parse(m.ToString());
				BigInt a = new BigInt(random.Next(1, max));

				if (BigInt.GCD(a, m) != 1)
				{
					labelAnswer.Text = "Число составное";
					return;
				}

				// 3)
				c--;
				if(c==0)
				{
					labelAnswer.Text = "Не удалось установить простоту";
					return;
				}

				// 4)
				while (k < q.Count)
				{
					// 4.1)
					if (BigInt.Pow(a, m - 1, m) != 1)
					{
						labelAnswer.Text = "Число составное";
						return;
					}
					// 4.2
					if (BigInt.Pow(a, (m - 1) / q.ElementAt(k), m) == 1)
						break;
					// 4.3
					k++;
				}

				// переформулированное условие выхода
				if (k >= q.Count)
				{
					labelAnswer.Text = "Число простое";
					return;
				}
			}
		}


	}
}
