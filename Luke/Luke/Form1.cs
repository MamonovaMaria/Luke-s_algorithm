using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace Luke
{
	public partial class Form1 : Form
	{
		BigInt m;
		List<BigInt> q;
		string temp;
		Random random;

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
			StreamReader openFileDialog = new StreamReader(openFileDialog1.FileName, Encoding.Default);
			textBoxN.Text = openFileDialog.ReadToEnd();
			openFileDialog.Close();
		}

		private void buttonCalculate_Click(object sender, EventArgs e)
		{
			labelAnswer.Text = "";

			// чтение чисел
			m = new BigInt(textBoxN.Text);
			q = new List<BigInt>();
			random = new Random();

			temp = "";
			for (int i = 0; i < textBoxNMinus1.Text.Length; i++)
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
			List<TimeSpan> resTime1 = new List<TimeSpan>();
			List<TimeSpan> resTime2 = new List<TimeSpan>();
			int ch = 0;
			do
			{
				DateTime time1 = DateTime.Now;
				Stopwatch stopWatch = new Stopwatch();
				stopWatch.Start();

			Calculate();

			DateTime time2 = DateTime.Now;
				stopWatch.Stop();
				resTime1.Add(stopWatch.Elapsed);
				resTime2.Add(time2 - time1);
				ch++;
			} while (ch < 1000);
			long t1 = 0;
			long t2 = 0;
			foreach (var el in resTime1)
				t1 += el.Ticks;
			foreach (var el in resTime2)
				t2 += el.Ticks;
			textBoxForTime1.Text = (t1 / 1000).ToString();
			textBoxForTime2.Text = (t2 / 1000).ToString();
		}

		private void Calculate()
		{
			// 1)
			int c = 20, k = 0;

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
				if (c == 0)
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
