using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Russia
{
    public partial class ControlForm : Form
    {

        private Keys[] keys;  //按键数组，用来存放用户设置的按键
        private int level; //起始等级
        private bool trans; //是否是透明模式
        public ControlForm()
        {
            InitializeComponent();
            keys = new Keys[5];  //初始化数组
        }

        public void SetOptions(Keys[] keys, int level, bool trans) //根据参数设置对话框的状态
        {
            for (int i = 0; i < this.keys.Length; i++)
            {
                this.keys[i] = keys[i];
            }
            this.txtLeft.Text = keys[0].ToString();
            this.txtRight.Text = keys[1].ToString();
            this.txtDown.Text = keys[2].ToString();
            this.txtRotate.Text = keys[3].ToString();
            this.txtDrop.Text = keys[4].ToString();

            this.level = level;
            this.comboStartLevel.SelectedIndex = level - 1;
            this.trans = trans;
            if (trans)
            {
                this.checkBox1.Checked = true;
            }
        }

        public void GetOptions(ref Keys[] key, ref int level, ref bool tran) //得到对话框设置的参数
        {
            key = this.keys;
            level = this.level;
            tran = this.trans;
        }

        private void ChangeKey(TextBox txt, KeyEventArgs e, int i)
        {

            if ((e.KeyValue >= 32 && e.KeyValue <= 40) || (e.KeyValue >= 45 && e.KeyValue <= 46) || (e.KeyValue >= 48 && e.KeyValue <= 57) || (e.KeyValue >= 65 && e.KeyValue <= 90) ||
                 (e.KeyValue >= 96 && e.KeyValue <= 107) || (e.KeyValue >= 109 && e.KeyValue <= 111) || (e.KeyValue >= 186 && e.KeyValue <= 192) ||
                 (e.KeyValue >= 219 && e.KeyValue <= 222))
            {
                txt.Text = e.KeyCode.ToString();
                this.keys[i] = e.KeyCode;
            }
        }
        private void ControlForm_Load(object sender, EventArgs e)
        {

        }

        private void txtLeft_KeyDown(object sender, KeyEventArgs e)
        {
            ChangeKey(txtLeft, e, 0);
        }

        private void txtRight_KeyDown(object sender, KeyEventArgs e)
        {
            ChangeKey(txtRight, e, 1);
        }

        private void txtDown_KeyDown(object sender, KeyEventArgs e)
        {
            ChangeKey(txtDown, e, 2);
        }

        private void txtRotate_KeyDown(object sender, KeyEventArgs e)
        {
            ChangeKey(txtRotate, e, 3);
        }

        private void txtDrop_KeyDown(object sender, KeyEventArgs e)
        {
            ChangeKey(txtDrop, e, 4);
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboStartLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.level = comboStartLevel.SelectedIndex + 1;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                this.trans = true;
            else
                this.trans = false;
        }
    }
}
