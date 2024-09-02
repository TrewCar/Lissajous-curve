namespace WinForm
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            RadioOff = new RadioButton();
            RadioWave = new RadioButton();
            RadioPC = new RadioButton();
            RadioMicro = new RadioButton();
            button1 = new Button();
            groupBox2 = new GroupBox();
            RadioWaves = new RadioButton();
            RadioLisajue = new RadioButton();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(RadioOff);
            groupBox1.Controls.Add(RadioWave);
            groupBox1.Controls.Add(RadioPC);
            groupBox1.Controls.Add(RadioMicro);
            groupBox1.Location = new Point(9, 7);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(142, 123);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Формат";
            // 
            // RadioOff
            // 
            RadioOff.AutoSize = true;
            RadioOff.Checked = true;
            RadioOff.Location = new Point(10, 98);
            RadioOff.Name = "RadioOff";
            RadioOff.Size = new Size(42, 19);
            RadioOff.TabIndex = 3;
            RadioOff.TabStop = true;
            RadioOff.Text = "Off";
            RadioOff.UseVisualStyleBackColor = true;
            // 
            // RadioWave
            // 
            RadioWave.AutoSize = true;
            RadioWave.Location = new Point(10, 73);
            RadioWave.Name = "RadioWave";
            RadioWave.Size = new Size(92, 19);
            RadioWave.TabIndex = 2;
            RadioWave.Text = "Свои волны";
            RadioWave.UseVisualStyleBackColor = true;
            RadioWave.CheckedChanged += SetType_CheckedChanged;
            // 
            // RadioPC
            // 
            RadioPC.AutoSize = true;
            RadioPC.Location = new Point(10, 48);
            RadioPC.Name = "RadioPC";
            RadioPC.Size = new Size(41, 19);
            RadioPC.TabIndex = 1;
            RadioPC.Text = "ПК";
            RadioPC.UseVisualStyleBackColor = true;
            RadioPC.CheckedChanged += SetType_CheckedChanged;
            // 
            // RadioMicro
            // 
            RadioMicro.AutoSize = true;
            RadioMicro.Location = new Point(10, 23);
            RadioMicro.Name = "RadioMicro";
            RadioMicro.Size = new Size(86, 19);
            RadioMicro.TabIndex = 0;
            RadioMicro.Text = "Микрофон";
            RadioMicro.UseVisualStyleBackColor = true;
            RadioMicro.CheckedChanged += SetType_CheckedChanged;
            // 
            // button1
            // 
            button1.Location = new Point(9, 136);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "On";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(RadioWaves);
            groupBox2.Controls.Add(RadioLisajue);
            groupBox2.Location = new Point(165, 14);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 100);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Рендер";
            // 
            // RadioWaves
            // 
            RadioWaves.AutoSize = true;
            RadioWaves.Location = new Point(10, 45);
            RadioWaves.Name = "RadioWaves";
            RadioWaves.Size = new Size(62, 19);
            RadioWaves.TabIndex = 1;
            RadioWaves.Text = "Волны";
            RadioWaves.UseVisualStyleBackColor = true;
            RadioWaves.CheckedChanged += RadioWaves_CheckedChanged;
            // 
            // RadioLisajue
            // 
            RadioLisajue.AutoSize = true;
            RadioLisajue.Checked = true;
            RadioLisajue.Location = new Point(10, 20);
            RadioLisajue.Name = "RadioLisajue";
            RadioLisajue.Size = new Size(116, 19);
            RadioLisajue.TabIndex = 0;
            RadioLisajue.TabStop = true;
            RadioLisajue.Text = "Формы лиссажу";
            RadioLisajue.UseVisualStyleBackColor = true;
            RadioLisajue.CheckedChanged += RadioWaves_CheckedChanged;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox2);
            Controls.Add(button1);
            Controls.Add(groupBox1);
            Name = "Main";
            Text = "Form1";
            FormClosed += Main_FormClosed;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private RadioButton RadioWave;
        private RadioButton RadioPC;
        private RadioButton RadioMicro;
        private Button button1;
        private RadioButton RadioOff;
        private GroupBox groupBox2;
        private RadioButton RadioWaves;
        private RadioButton RadioLisajue;
    }
}
