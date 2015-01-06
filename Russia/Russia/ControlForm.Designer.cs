namespace Russia
{
    partial class ControlForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDrop = new System.Windows.Forms.TextBox();
            this.txtRotate = new System.Windows.Forms.TextBox();
            this.txtDown = new System.Windows.Forms.TextBox();
            this.txtRight = new System.Windows.Forms.TextBox();
            this.txtLeft = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.music = new System.Windows.Forms.CheckBox();
            this.comboStartLevel = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnok = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDrop);
            this.groupBox1.Controls.Add(this.txtRotate);
            this.groupBox1.Controls.Add(this.txtDown);
            this.groupBox1.Controls.Add(this.txtRight);
            this.groupBox1.Controls.Add(this.txtLeft);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(172, 169);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "键盘设置";
            // 
            // txtDrop
            // 
            this.txtDrop.Location = new System.Drawing.Point(57, 133);
            this.txtDrop.Name = "txtDrop";
            this.txtDrop.ReadOnly = true;
            this.txtDrop.Size = new System.Drawing.Size(72, 21);
            this.txtDrop.TabIndex = 9;
            this.txtDrop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDrop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDrop_KeyDown);
            // 
            // txtRotate
            // 
            this.txtRotate.Location = new System.Drawing.Point(57, 105);
            this.txtRotate.Name = "txtRotate";
            this.txtRotate.ReadOnly = true;
            this.txtRotate.Size = new System.Drawing.Size(72, 21);
            this.txtRotate.TabIndex = 8;
            this.txtRotate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRotate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRotate_KeyDown);
            // 
            // txtDown
            // 
            this.txtDown.Location = new System.Drawing.Point(57, 76);
            this.txtDown.Name = "txtDown";
            this.txtDown.ReadOnly = true;
            this.txtDown.Size = new System.Drawing.Size(72, 21);
            this.txtDown.TabIndex = 7;
            this.txtDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDown_KeyDown);
            // 
            // txtRight
            // 
            this.txtRight.Location = new System.Drawing.Point(57, 49);
            this.txtRight.Name = "txtRight";
            this.txtRight.ReadOnly = true;
            this.txtRight.Size = new System.Drawing.Size(72, 21);
            this.txtRight.TabIndex = 6;
            this.txtRight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRight_KeyDown);
            // 
            // txtLeft
            // 
            this.txtLeft.Location = new System.Drawing.Point(57, 18);
            this.txtLeft.Name = "txtLeft";
            this.txtLeft.ReadOnly = true;
            this.txtLeft.Size = new System.Drawing.Size(72, 21);
            this.txtLeft.TabIndex = 5;
            this.txtLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtLeft.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLeft_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "丢下:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "旋转:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "下移:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "右移:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "左移:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.music);
            this.groupBox2.Controls.Add(this.comboStartLevel);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(13, 180);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(171, 77);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "环境设置";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(96, 55);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "透明模式";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // music
            // 
            this.music.AutoSize = true;
            this.music.Enabled = false;
            this.music.Location = new System.Drawing.Point(12, 55);
            this.music.Name = "music";
            this.music.Size = new System.Drawing.Size(48, 16);
            this.music.TabIndex = 2;
            this.music.Text = "音效";
            this.music.UseVisualStyleBackColor = true;
            // 
            // comboStartLevel
            // 
            this.comboStartLevel.FormattingEnabled = true;
            this.comboStartLevel.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.comboStartLevel.Location = new System.Drawing.Point(67, 24);
            this.comboStartLevel.Name = "comboStartLevel";
            this.comboStartLevel.Size = new System.Drawing.Size(61, 20);
            this.comboStartLevel.TabIndex = 1;
            this.comboStartLevel.SelectedIndexChanged += new System.EventHandler(this.comboStartLevel_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "开始级别：";
            // 
            // btnok
            // 
            this.btnok.Location = new System.Drawing.Point(13, 268);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(75, 23);
            this.btnok.TabIndex = 2;
            this.btnok.Text = "确定";
            this.btnok.UseVisualStyleBackColor = true;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // btncancel
            // 
            this.btncancel.Location = new System.Drawing.Point(109, 268);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(75, 23);
            this.btncancel.TabIndex = 3;
            this.btncancel.Text = "取消";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(196, 303);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.btnok);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ControlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ControlForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDrop;
        private System.Windows.Forms.TextBox txtRotate;
        private System.Windows.Forms.TextBox txtDown;
        private System.Windows.Forms.TextBox txtRight;
        private System.Windows.Forms.TextBox txtLeft;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox music;
        private System.Windows.Forms.ComboBox comboStartLevel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnok;
        private System.Windows.Forms.Button btncancel;
    }
}