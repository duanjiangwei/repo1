using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Russia
{
    public partial class MainForm : Form
    {

        #region 变量
        private Block block;  //方块类对象
        private Block nextBlock;  //下一个方块
        private int nextShapeNO;  //下一个形状编号
        private bool paused;  //是否暂停
        private DateTime atStart;  //开始的时间
        private DateTime atPause; //暂停的时刻
        private TimeSpan pauseTime;  //暂停时间间隔
        private ControlForm controlform;//控制窗口对象
        private Keys[] keys;  //按键数组
        private int level;  //等级
        private int startLevel; //初始等级
        private bool trans;  //是否是透明模式
        private int rowDelNum;  //消去的行数
        private bool failed;  //是否失败

        #endregion
        public MainForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 读取文件初始化类成员
        /// </summary>
        private void Initiate()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                string path = Application.StartupPath;
                doc.Load(path + "\\setting.ini"); //加载配置文件
                XmlNodeList nodes = doc.DocumentElement.ChildNodes;  //获取所有节点
                this.startLevel = Convert.ToInt32(nodes[0].InnerText);  //设置初始等级
                this.level = this.startLevel;//设置等级
                this.trans = Convert.ToBoolean(nodes[1].InnerText); //设置???

                keys = new Keys[5];  //设置按键
                for (int i = 0; i < nodes[2].ChildNodes.Count; i++)  //遍历节点，设置按键
                {
                    KeysConverter kc = new KeysConverter();  //按键转换类
                    this.keys[i] = (Keys)(kc.ConvertFromString(nodes[2].ChildNodes[i].InnerText));
                }
            }
            catch  //捕获异常,设置默认值
            {
                this.trans = false;  
                keys = new Keys[5];
                keys[0] = Keys.Left;  //左移按键
                keys[1] = Keys.Right; //右移按键
                keys[2] = Keys.Down;  //下键
                keys[3] = Keys.NumPad8;//旋转
                keys[4] = Keys.NumPad9;//直接落下
                this.level = 1;
                this.startLevel = 1;
            }
            this.timer1.Interval = 500 - 50 * (level - 1); //根据等级设置定时器间隔
            this.label4.Text = "级别：  " + this.startLevel;

            if (trans)
            {
                this.TransparencyKey = Color.Black; 
            }
        }


        /// <summary>
        /// 开始游戏
        /// </summary>
        private void Start()
        {
            this.block = null;
            this.nextBlock = null;
            this.label1.Text = "手速： 0";
            this.label2.Text = "块数： 0";
            this.label3.Text = "行数： 0";
            this.label4.Text = "级别： "+this.startLevel;
            this.level = this.startLevel;
            this.timer1.Interval = 500 - 50 * (level - 1);
            this.paused = false;
            this.failed = false;
            this.panel1.Invalidate(); //游戏区重绘
            this.panel2.Invalidate();  //下个方块显示区重绘
            this.nextShapeNO = 0; //下一个方块形状编号
            CreateBlock();//生成当前方块
            CreateNextBlock();//生成下个方块
            this.timer1.Enabled = true; //启动定时器
            this.atStart = DateTime.Now;  //设置游戏开始时刻
            this.pauseTime = new TimeSpan(0); //初始化暂停时间间隔
        }


        /// <summary>
        /// 游戏结束处理
        /// </summary>
        private void Fail()
        {
            this.failed = true;
            this.panel1.Invalidate(new Rectangle(0, 0, panel1.Width, 100)); //重绘区域
            this.timer1.Enabled = false; //停止定时器
            this.paused = true; 
        }


        /// <summary>
        /// 在游戏区绘制新方块
        /// </summary>
        /// <returns></returns>
        private bool CreateBlock()
        {
            Point firstPos;
            Color color;
            if (this.nextShapeNO == 0)  //如果是首次产生方块
            {
                Random rand = new Random(); //随机数对象
                this.nextShapeNO = rand.Next(1, 8); //1--8的随机数
            }
            switch (this.nextShapeNO)   //根据随机数初始化方块属性
            { 
                case 1:
                    firstPos = new Point(4, 0);
                    color = Color.Turquoise;
                    break;
                case 2:
                    firstPos = new Point(3, 0);
                    color = Color.Red;
                    break;
                case 3:
                    firstPos = new Point(4, 0);
                    color = Color.Silver;
                    break;
                case 4:
                    firstPos = new Point(4, 0);
                    color = Color.LawnGreen;
                    break;
                case 5:
                    firstPos = new Point(4, 1);
                    color = Color.DodgerBlue;
                    break;
                case 6:
                    firstPos = new Point(4, 0);
                    color = Color.Yellow;
                    break;
                default:
                    firstPos = new Point(4, 0);
                    color = Color.Salmon;
                    break;
            }
            if (this.block == null)  //对象没有初始化
            {
                block = new Block(this.panel1, 9, 19, 25, this.nextShapeNO, firstPos, color);  //初始化方块对象
            }
            else
            {
                if (!block.GeneBlock(this.nextShapeNO, firstPos, color))  //如果在指定位置不能产生方块
                {
                    return false;  //返回
                }
            }
            block.EraseLast(); //擦除上一个方块
            block.Move(2); //下移
            return true; //成功

        }


        /// <summary>
        /// 产生预览区方块
        /// </summary>
        private void CreateNextBlock()
        {
            Random rand = new Random();
            this.nextShapeNO = rand.Next(1, 8);
            Point firstPos;
            Color color;
            switch (this.nextShapeNO)
            { 
                case 1:
                    firstPos = new Point(1, 0);
                    color = Color.Turquoise;
                    break;
                case 2:
                    firstPos = new Point(0, 1);
                    color = Color.Red;
                    break;
                case 3:
                    firstPos = new Point(0, 0);
                    color = Color.Silver;
                    break;
                case 4:
                    firstPos = new Point(0, 0);
                    color = Color.LawnGreen;
                    break;
                case 5:
                    firstPos = new Point(0, 1);
                    color = Color.DodgerBlue;
                    break;
                case 6:
                    firstPos = new Point(0, 0);
                    color = Color.Yellow;
                    break;
                default:
                    firstPos = new Point(0, 0);
                    color = Color.Salmon;
                    break;
            }
            if (nextBlock == null)
            {
                nextBlock = new Block(this.panel2, 3, 1, 20, this.nextShapeNO, firstPos, color);
            }
            else
            {
                nextBlock.GeneBlock(this.nextShapeNO, firstPos, color);
                nextBlock.EraseLast();
            }
        }


        /// <summary>
        /// 固定方块，产生新方块，判断是否结束游戏
        /// </summary>
        private void FixAndCreate()
        {
            block.FixBlock();  //固定方块到方块区

            //计算手速算法: 手速=已落下的方块总数/(游戏开始后经过的时间-暂停的时间)
            label1.Text = "手速： " + Math.Round((double)block.BlockNum / 
                ((TimeSpan)(DateTime.Now - this.atStart)).Subtract(this.pauseTime).TotalSeconds, 3) + "块/秒";

            label2.Text = "块数： " + block.BlockNum;
            label3.Text = "行数： " + block.RowDelNum;

            if (this.level < 10 && block.RowDelNum - this.rowDelNum >= 30)  //每次消去30行后升级(10级以下)
            {
                this.rowDelNum += 30;
                this.level++;  //等级提高
                this.timer1.Interval = 500 - 50 * (level - 1); //更新计时器
                this.label4.Text = "级别： " + this.level;//更新显示
            }

            bool createOK = this.CreateBlock();  //产生新方块，返回结果
            this.CreateNextBlock();  //产生新方块，并显示
            if (!createOK)  //如果不能产生新方块，那么游戏结束
            {
                this.Fail();
            }
        }


        /// <summary>
        /// 保存设置到文件
        /// </summary>
        private void SaveSetting()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlDeclaration xmlDec = doc.CreateXmlDeclaration("1.0", "gb2312", null);  //

                XmlElement setting = doc.CreateElement("SETTING"); //创建节点
                doc.AppendChild(setting);

                XmlElement level = doc.CreateElement("LEVEL");  //等级子节点
                level.InnerText = this.startLevel.ToString();
                setting.AppendChild(level);

                XmlElement trans = doc.CreateElement("TRANSPARENT"); //trans子节点
                trans.InnerText = this.trans.ToString();
                setting.AppendChild(trans);

                XmlElement keys = doc.CreateElement("KEYS");  //保存按键
                setting.AppendChild(keys);
                foreach (Keys k in this.keys)
                {
                    KeysConverter kc = new KeysConverter();
                    XmlElement x = doc.CreateElement("SUBKEYS");
                    x.InnerText = kc.ConvertToString(k);
                    keys.AppendChild(x);
                }

                XmlElement root = doc.DocumentElement;
                doc.InsertBefore(xmlDec, root);
                string path = Application.StartupPath;
                doc.Save(path+"\\setting.ini");

            }
            catch (Exception xe)
            {
                MessageBox.Show(xe.Message);
            }

        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            Initiate();
        }

        private void button1_Click(object sender, EventArgs e)  //开始游戏
        {
            this.Start(); //开始游戏
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {

            //MessageBox.Show("test");
            if (block != null && this.paused == false && !this.failed)
            {
                if (e.KeyCode == this.keys[0])  //按下的是坐键
                {
                    if (block.Move(0))
                    {
                        block.EraseLast();
                    }
                }
                else if (e.KeyCode == this.keys[1])
                {
                    if (block.Move(1))
                    {
                        block.EraseLast();
                    }
                }
                else if (e.KeyCode == this.keys[2])
                {
                    if (!block.Move(2))
                    {
                        this.FixAndCreate();
                    }
                    else
                    {
                        block.EraseLast();
                    }
                }
                else if (e.KeyCode == this.keys[3])  //旋转
                {
                    if (block.Rotate())
                    {
                        block.EraseLast();
                    }
                }
                else if (e.KeyCode == this.keys[4])  //直接下落
                {
                    block.Drop();  //落到最下面
                    block.EraseLast();
                    this.FixAndCreate(); 
                }
                if (e.KeyCode == Keys.F2)  //重新开始
                {
                    this.Start();  
                }
                else if (e.KeyCode == Keys.F3)
                { 
                   //执行暂停命令
                    button3_Click(null, null);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!this.failed)
            {
                if (paused)
                {
                    this.pauseTime += DateTime.Now - this.atPause;
                    paused = false;
                    this.timer1.Start();
                }
                else
                {
                    this.atPause = DateTime.Now;
                    paused = true;
                    this.timer1.Stop();
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)  //重绘事件响应
        {
            if (block != null)
            {
                block.DrawBlocks(e.ClipRectangle);  //绘制方块
            }
            if (this.failed)
            {
                Graphics gra = e.Graphics; //获得绘图对象
                gra.DrawString("Game Over", new Font("Arial Black", 25f), new SolidBrush(Color.Gray), 30, 30); //绘制游戏失败
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (block != null && !this.failed)  //游戏没有结束
            {
                if (!block.Move(2))  //如果可以下移
                {
                    this.FixAndCreate();
                }
               else
                {
                    block.EraseLast(); //消除原来的
                }
            }
           
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            if (nextBlock != null)
            {
                nextBlock.DrawBlocks(e.ClipRectangle);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SaveSetting();
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            label5.Text = "开源";
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.Text = "版本：1.0";
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        /// <summary>
        /// 使方向键有效
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)
            {
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!paused)   //如果正在游戏中
            {
                this.atPause = DateTime.Now; //设置暂停时刻
                this.paused = true; //设置暂停
                this.timer1.Stop(); //停止计时器
            }
            controlform = new ControlForm();  //新建控制窗体
            controlform.SetOptions(this.keys, this.level, this.trans); //根据现有参数初始化设置窗体
            controlform.DialogResult = DialogResult.Cancel;
            if (controlform.ShowDialog() == DialogResult.OK) //显示窗体，如果点了确定按钮后
            {
                controlform.GetOptions(ref this.keys, ref this.startLevel, ref this.trans); //得到用户选择
                this.level = this.startLevel; //当前等级等于初始等级
                this.label4.Text = "级别： " + this.level; //更新显示
                this.timer1.Interval = 500 - 50 * (this.level - 1); //更新游戏速度
                if (this.trans == true)
                    this.TransparencyKey = Color.Black;
                else
                    this.TransparencyKey = Color.Transparent;

            }
            this.paused = false; //恢复游戏
            this.pauseTime = DateTime.Now - atPause;  //设置暂停时间
            timer1.Start(); //打开定时器
        }

        private void 操作控制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlForm frm = new ControlForm();
            frm.Show();
        }  

    }
}
