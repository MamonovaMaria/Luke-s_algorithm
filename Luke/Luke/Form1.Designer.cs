namespace Luke
{
	partial class Form1
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxN = new System.Windows.Forms.TextBox();
			this.buttonFromFile = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxNMinus1 = new System.Windows.Forms.TextBox();
			this.buttonCalculate = new System.Windows.Forms.Button();
			this.labelAnswer = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Введите N";
			// 
			// textBoxN
			// 
			this.textBoxN.Location = new System.Drawing.Point(12, 25);
			this.textBoxN.Name = "textBoxN";
			this.textBoxN.Size = new System.Drawing.Size(350, 20);
			this.textBoxN.TabIndex = 1;
			// 
			// buttonFromFile
			// 
			this.buttonFromFile.Location = new System.Drawing.Point(287, 111);
			this.buttonFromFile.Name = "buttonFromFile";
			this.buttonFromFile.Size = new System.Drawing.Size(75, 23);
			this.buttonFromFile.TabIndex = 2;
			this.buttonFromFile.Text = "Из файла";
			this.buttonFromFile.UseVisualStyleBackColor = true;
			this.buttonFromFile.Click += new System.EventHandler(this.buttonFromFile_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(269, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Введите простые сомножители, составляющие N-1";
			// 
			// textBoxNMinus1
			// 
			this.textBoxNMinus1.Location = new System.Drawing.Point(15, 64);
			this.textBoxNMinus1.Multiline = true;
			this.textBoxNMinus1.Name = "textBoxNMinus1";
			this.textBoxNMinus1.Size = new System.Drawing.Size(350, 41);
			this.textBoxNMinus1.TabIndex = 1;
			// 
			// buttonCalculate
			// 
			this.buttonCalculate.Location = new System.Drawing.Point(137, 130);
			this.buttonCalculate.Name = "buttonCalculate";
			this.buttonCalculate.Size = new System.Drawing.Size(105, 40);
			this.buttonCalculate.TabIndex = 2;
			this.buttonCalculate.Text = "Проверить";
			this.buttonCalculate.UseVisualStyleBackColor = true;
			this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);
			// 
			// labelAnswer
			// 
			this.labelAnswer.AutoSize = true;
			this.labelAnswer.Location = new System.Drawing.Point(174, 186);
			this.labelAnswer.Name = "labelAnswer";
			this.labelAnswer.Size = new System.Drawing.Size(0, 13);
			this.labelAnswer.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(388, 272);
			this.Controls.Add(this.buttonCalculate);
			this.Controls.Add(this.buttonFromFile);
			this.Controls.Add(this.textBoxNMinus1);
			this.Controls.Add(this.textBoxN);
			this.Controls.Add(this.labelAnswer);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxN;
		private System.Windows.Forms.Button buttonFromFile;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxNMinus1;
		private System.Windows.Forms.Button buttonCalculate;
		private System.Windows.Forms.Label labelAnswer;
	}
}

