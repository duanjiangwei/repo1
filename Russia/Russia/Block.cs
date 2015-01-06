using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Russia
{
    public class Block
    {
        #region 变量成员
        private int shapeNO;  //形状号
        private Control con;  //绘图控件
        private Point[] pos;  //当前位置
        private Point[] lastPos; //上一次位置
        private int leftBorder; //左边界
        private int bottomBorder; //下边界
        private int unitPix;  //每块像素数
        private int blockNum = 0; //固定的方块数，为了计算手速
        private int rowDelNum = 0; //消除的行数
        private bool[,] huji;  //存放当前游戏区内各点是否存在方块
        private Color[,] iori; //存放每一个方块对应的颜色
        private Color color; //当前块颜色
        
        #endregion

        #region 属性
        public int BlockNum
        {
            get
            {
                return this.blockNum;
            }
        }

        public int RowDelNum
        {
            get
            {
                return this.rowDelNum;
            }
        }

        #endregion


        #region 方法
        ///<summary>
        ///构造方法
        ///</summary>
        public Block(Control con, int leftBorder, int bottomBorder, int unitPix, 
            int shapeNO, Point firstPos, Color color)
        {
            this.con = con;
            this.leftBorder = leftBorder;
            this.bottomBorder = bottomBorder;
            this.unitPix = unitPix;
            this.SetPos(shapeNO, firstPos); //设置方块位置
            this.color = color;
            this.huji = new bool[leftBorder + 1, bottomBorder + 1];
            this.iori=new Color[leftBorder+1,bottomBorder+1];
            this.lastPos = new Point[4];
        }


        /// <summary>
        /// 根据方块号和起点位置填充方块数组
        /// </summary>
        /// <param name="shapeNO">方块编号</param>
        /// <param name="firstPos">第一个点</param>
        private void SetPos(int shapeNO, Point firstPos)
        {
            this.shapeNO = shapeNO;

            this.pos = new Point[4];
            pos[0] = firstPos;
            switch (shapeNO)
            { 
                case 1:    //石头
                    pos[1] = new Point(firstPos.X + 1, firstPos.Y);
                    pos[2] = new Point(firstPos.X, firstPos.Y + 1);
                    pos[3] = new Point(firstPos.X+1,firstPos.Y+1);
                    break;
                case 2:  //横着的棍子
                    pos[1] = new Point(firstPos.X+1,firstPos.Y);
                    pos[2] = new Point(firstPos.X+2,firstPos.Y);
                    pos[3] = new Point(firstPos.X + 3, firstPos.Y);
                    break;
                case 3: //倒土
                    pos[1] = new Point(firstPos.X + 1, firstPos.Y);
                    pos[2] = new Point(firstPos.X+1,firstPos.Y+1);
                    pos[3] = new Point(firstPos.X + 2, firstPos.Y);
                    break;
                case 4:  //正2
                    pos[1] = new Point(firstPos.X+1,firstPos.Y);
                    pos[2] = new Point(firstPos.X+1,firstPos.Y+1);
                    pos[3] = new Point(firstPos.X+2,firstPos.Y+1);
                    break;
                case 5:  //反2
                    pos[1] = new Point(firstPos.X + 1, firstPos.Y);
                    pos[2] = new Point(firstPos.X+1,firstPos.Y-1);
                    pos[3] = new Point(firstPos.X+2,firstPos.Y-1);
                    break;
                case 6:
                    pos[1] = new Point(firstPos.X,firstPos.Y+1);
                    pos[2] = new Point(firstPos.X+1,firstPos.Y);
                    pos[3] = new Point(firstPos.X+2,firstPos.Y);
                    break;
                default:
                    pos[1] = new Point(firstPos.X+1,firstPos.Y);
                    pos[2] = new Point(firstPos.X+2,firstPos.Y);
                    pos[3] = new Point(firstPos.X+2,firstPos.Y+1);
                    break;
            }
        }




        /// <summary>
        /// 擦除显示空间中方块上一个位置的显示
        /// </summary>
        public void EraseLast()
        {
            foreach (Point p in this.lastPos)
            { 
               this.con.Invalidate(new Rectangle(p.X*unitPix,p.Y*unitPix,unitPix+1,unitPix+1));
            }
        }

        private void SetLastPos()  //将当前位置数组填充到上一个位置数组
        {
            for (int i = 0; i < this.pos.Length; i++)
            {
                this.lastPos[i] = this.pos[i];
            }
        }



        /// <summary>
        /// 判断方块能否移动
        /// </summary>
        /// <param name="direction">移动方向，0代表左，1代表右，其他代表下</param>
        /// <returns></returns>
        private bool CanMove(int direction)
        {
            bool canMove = true;
            if (direction == 0)  //向左移动
            {
                foreach (Point p in this.pos)  //遍历当前位置数组
                {
                    if (p.X - 1 < 0 || this.huji[p.X - 1, p.Y]) //如果超出边界或者 被已存在的方块阻挡，则不能移动
                    {
                        canMove = false;
                        break;  //退出判断
                    }
                }
            }
            else if (direction == 1)  //向右移动
            {
                foreach (Point p in this.pos)
                {
                    if (p.X + 1 > this.leftBorder || this.huji[p.X + 1, p.Y])  //被方块阻挡或者到达右边界
                    {
                        canMove = false;
                        break;
                    }
                }
            }
            else  //向下移动
            {
                foreach (Point p in this.pos)
                {
                    if (p.Y + 1 > this.bottomBorder || this.huji[p.X, p.Y + 1])  //超过下边界或者被阻挡
                    {
                        canMove = false;
                        break;
                    }
                }
            }

            return canMove;  //返回结果
        }


        /// <summary>
        /// 判断是否能旋转方块
        /// </summary>
        /// <param name="pos">旋转后的方块</param>
        /// <returns></returns>
        private bool CanRotate(Point[] pos)
        {
            bool canRotate = true;
            foreach (Point p in pos)  //遍历方块位置，判断是否能旋转
            {
                if (p.X < 0 || p.X > this.leftBorder || p.Y < 0 || p.Y > this.bottomBorder || this.huji[p.X, p.Y])
                {
                    canRotate = false;
                    break;
                }
            }

            if (canRotate == true)   //如果可以旋转，更新位置坐标
                this.SetLastPos();
            return canRotate;
        }


        private void DrawOne(int x, int y, Color color, Graphics gra)  //绘制当前方块（小方块）
        {
            gra.FillRectangle(new SolidBrush(color), x * unitPix + 1, unitPix * y + 1, unitPix - 1, unitPix - 1);
            gra.DrawRectangle(new Pen(Color.Black, 1), x * unitPix, y * unitPix, unitPix, unitPix);
        }


        /// <summary>
        /// 消行函数（算法不太明白，到后面在研究）
        /// </summary>
        private void DelRows()
        {
            int count = 0; //可以消去的行数
            int highRow = 20;//？？
            int lowRow = -1;//??
            int[] delRow = { -1,-1,-1,-1};//记录可以消去的行号
            foreach (Point p in this.pos) //遍历方块数组
            {
                if (p.Y == highRow || p.Y == lowRow) //如果是已经确定的行的话，进行下次循环
                    continue;
                int i;
                for (i = 0; i < this.huji.GetLength(0); i++) //遍历方块行
                    if (huji[i, p.Y] == false)//如果有空格
                        break; //调出循环
                if (i == this.huji.GetLength(0))//如果空格是行的末尾,记录行号
                {
                    delRow[count] = p.Y;
                    if (p.Y < highRow)
                        highRow = p.Y;
                    if (p.Y > lowRow)
                        lowRow = p.Y;
                    count++;
                }
            }

            if (count > 0)  //如果可消除的行数大于0
            {
                Graphics gra = con.CreateGraphics(); //创建容器控件的画布
                foreach (Point p in this.lastPos) //在方块原先位置绘图
                {
                    gra.FillRectangle(new SolidBrush(con.BackColor), p.X * this.unitPix,
                       p.Y * unitPix, 25, 25); //用背景色填充方块
                }

                foreach (Point p in this.pos)  //遍历当前方块位置，绘制当前方块
                {
                    this.DrawOne(p.X, p.Y, this.color, gra);
                }

                foreach (int i in delRow)    //遍历要被消除的行，将方块背景色变成灰色
                {
                    if (i > 0)
                    {
                        for (int j = 0; j < this.huji.GetLength(0); j++)
                        {
                            gra.FillRectangle(new SolidBrush(Color.FromArgb(60, Color.Black)), j * this.unitPix, i * unitPix, 25, 25);
                        }
                    }
                }

                System.Threading.Thread.CurrentThread.Join(180); //线程延迟180毫秒

                if (count == 2 && lowRow - highRow > 1)
                {
                    for (int i = lowRow; i > highRow + 1; i--)
                    {
                        for (int j = 0; j < this.huji.GetLength(0); j++)  //上面的行落到下面的行
                        {
                            this.huji[j, i] = this.huji[j, i - 1];
                            this.iori[j, i] = this.iori[j, i - 1];
                        }
                    }

                    for (int i = highRow; i >= count; i--)   //??
                    {
                        for (int j = 0; j < this.huji.GetLength(0); j++)
                        {
                            this.huji[j, i + 1] = this.huji[j, i - 1];
                            this.iori[j, i + 1] = this.iori[j, i - 1];
                        }
                    }
                }

                else if (count == 3 && lowRow - highRow > 2)
                {
                    int midRow = -1;
                    foreach (int row in delRow)
                    {
                        if (row != highRow && row != lowRow)
                        {
                            midRow = row;
                            break;
                        }
                        for (int j = 0; j < this.huji.GetLength(0); j++)
                        {
                            this.huji[j, lowRow] = this.huji[j, lowRow + highRow - midRow];
                        }
                        for (int i = highRow; i >= count; i--)
                        {
                            for (int j = 0; j < this.huji.GetLength(0); j++)
                            {
                                this.huji[j, i + 2] = this.huji[j, i - 1];
                                this.iori[j, i + 2] = this.iori[j, i - 1];
                            }
                        }
                    }
                }

                else
                {
                    for (int i = lowRow; i >= count; i--)
                    {
                        for (int j = 0; j < this.huji.GetLength(0); j++)
                        {
                            this.huji[j, i] = this.huji[j, i - count];
                            this.iori[j,i]=this.iori[j,i-count];
                        }
                    }

                }


                for (int i = 0; i < count; i++)
                {
                    for (int j = 0; j < this.huji.GetLength(0); j++)
                    {
                        this.huji[j, i] = false;
                    }

                    //播放音乐
                    
                }

                con.Invalidate(new Rectangle(0, 0, con.Width, (lowRow + 1) * this.unitPix));  //重绘指定区域
                this.rowDelNum += count; //消除的行数增加
            }
       }



        /// <summary>
        /// 固定方块，更新方块区
        /// </summary>
        public void FixBlock()
        {
            this.blockNum++; //方块数增加
            foreach (Point p in this.pos)  //遍历当前位置坐标，更新到方块区
            {
                this.huji[p.X, p.Y] = true;
                this.iori[p.X, p.Y] = this.color;
            }
            this.DelRows();  //检查消行
        }



        /// <summary>
        /// 根据参数判断是否能产生新方块
        /// </summary>
        /// <param name="shapeNO">方块编号</param>
        /// <param name="firstPos">产生位置</param>
        /// <param name="color">方块颜色</param>
        /// <returns>是否能</returns>
        public bool GeneBlock(int shapeNO, Point firstPos, Color color)
        {
            this.SetLastPos(); //当前位置更新到上一个位置
            this.EraseLast(); //擦除上一个方块
            this.SetPos(shapeNO, firstPos); //在新位置产生方块
            if (!this.CanRotate(this.pos)) //如果新位置不合适
            {
                this.pos = null;//放弃新位置
                return false;
            }
            else  //合适
            {
                this.color = color;
                return true;
            }
        }



        /// <summary>
        /// 旋转方块
        /// </summary>
        /// <returns>返回是否能旋转</returns>
        public bool Rotate()
        {
            bool rotated = true;  //是否旋转
            Point[] temp = { pos[0],pos[1],pos[2],pos[3]};
            switch (this.shapeNO)
            { 
                case 1:    //田，不能旋转
                    rotated = false;
                    break;
                case 2:
                    temp[0].Offset(2, 2);   //更改成旋转后的位置
                    temp[1].Offset(1, 1);
                    temp[3].Offset(-1, -1);
                    if (this.CanRotate(temp)) //判断旋转后的位置是否合法,合法则更新
                    {
                        this.pos[0].Offset(2, 2);
                        this.pos[1].Offset(1, 1);
                        this.pos[3].Offset(-1, -1);
                        this.shapeNO = 8;//设置当前编号
                    }
                    else
                        rotated = false;   //否则不能旋转
                    break;
                case 3:
                    temp[0].Offset(1,-1);   //更改成旋转后的位置
                    if (this.CanRotate(temp)) //判断旋转后的位置是否合法,合法则更新
                    {
                        this.pos[0].Offset(1,-1);
                      
                        this.shapeNO = 9;//设置当前编号
                    }
                    else
                        rotated = false;   //否则不能旋转
                    break;
                case 4:
                    temp[0].Offset(2, 0);   //更改成旋转后的位置
                    temp[1].Offset(0, 2);
                    if (this.CanRotate(temp)) //判断旋转后的位置是否合法,合法则更新
                    {
                        this.pos[0].Offset(2, 0);
                        this.pos[1].Offset(0, 2);
                        this.shapeNO = 12;//设置当前编号
                    }
                    else
                        rotated = false;   //否则不能旋转
                    break;
                case 5:
                    temp[2].Offset(-1,0);   //更改成旋转后的位置
                    temp[3].Offset(-1,2);
                    if (this.CanRotate(temp)) //判断旋转后的位置是否合法,合法则更新
                    {
                        this.pos[2].Offset(-1,0);
                        this.pos[3].Offset(-1,2);
                        this.shapeNO = 13;//设置当前编号
                    }
                    else
                        rotated = false;   //否则不能旋转
                    break;
                case 6:
                    temp[0].Offset(1, 1);   //更改成旋转后的位置
                    temp[1].Offset(2, 0);
                    temp[3].Offset(-1, -1);
                    if (this.CanRotate(temp)) //判断旋转后的位置是否合法,合法则更新
                    {
                        this.pos[0].Offset(1, 1);
                        this.pos[1].Offset(2, 0);
                        this.pos[3].Offset(-1, -1);
                        this.shapeNO = 14;//设置当前编号
                    }
                    else
                        rotated = false;   //否则不能旋转
                    break;

                case 7:
                    temp[0].Offset(1, -1);   //更改成旋转后的位置
                    //temp[1].Offset(1, 0);
                    temp[2].Offset(-1, 1);
                    temp[3].Offset(-2, 0);
                    if (this.CanRotate(temp)) //判断旋转后的位置是否合法,合法则更新
                    {
                        this.pos[0].Offset(1, -1);
                        //this.pos[1].Offset(1, 0);
                        this.pos[2].Offset(-1, 1);
                        this.pos[3].Offset(-2,0);
                        this.shapeNO = 17;//设置当前编号
                    }
                    else
                        rotated = false;   //否则不能旋转
                    break;
                case 8:
                    temp[0].Offset(-2, -2);   //更改成旋转后的位置
                    temp[1].Offset(-1, -1);
                    temp[3].Offset(1, 1);
                    if (this.CanRotate(temp)) //判断旋转后的位置是否合法,合法则更新
                    {
                        this.pos[0].Offset(-2, -2);
                        this.pos[1].Offset(-1, -1);
                        this.pos[3].Offset(1, 1);
                        this.shapeNO = 2;//设置当前编号
                    }
                    else
                        rotated = false;   //否则不能旋转
                    break;

                case 9:
                    temp[2].Offset(-1, -1);
                    if (this.CanRotate(temp)) //判断旋转后的位置是否合法,合法则更新
                    {
                        this.pos[2].Offset(-1, -1);
                        this.shapeNO = 10;//设置当前编号
                    }
                    else
                        rotated = false;   //否则不能旋转
                    break;

                case 10:
                    temp[3].Offset(-1, 1);
                    if (this.CanRotate(temp)) //判断旋转后的位置是否合法,合法则更新
                    {
                        this.pos[3].Offset(-1, 1);
                        this.shapeNO = 11;//设置当前编号
                    }
                    else
                        rotated = false;   //否则不能旋转
                    break;
                case 11:
                    temp[0].Offset(-1, 1);   //更改成旋转后的位置
                    temp[2].Offset(1, 1);
                    temp[3].Offset(1, -1);
                    if (this.CanRotate(temp)) //判断旋转后的位置是否合法,合法则更新
                    {
                        this.pos[0].Offset(-1, 1);
                        this.pos[2].Offset(1, 1);
                        this.pos[3].Offset(1, -1);
                        this.shapeNO = 3;//设置当前编号
                    }
                    else
                        rotated = false;   //否则不能旋转
                    break;
                case 12:
                    temp[0].Offset(-2, 0);   //更改成旋转后的位置
                    temp[1].Offset(0, -2);
                    if (this.CanRotate(temp)) //判断旋转后的位置是否合法,合法则更新
                    {
                        this.pos[0].Offset(-2, 0);
                        this.pos[1].Offset(0, -2);
                        this.shapeNO = 4;//设置当前编号
                    }
                    else
                        rotated = false;   //否则不能旋转
                    break;
                case 13:
                    temp[2].Offset(1, 0);
                    temp[3].Offset(1, -2);
                    if (this.CanRotate(temp)) //判断旋转后的位置是否合法,合法则更新
                    {
                        this.pos[2].Offset(1, 0);
                        this.pos[3].Offset(1, -2);
                        this.shapeNO = 5;//设置当前编号
                    }
                    else
                        rotated = false;   //否则不能旋转
                    break;

                case 14:
                    temp[2].Offset(1, 0);
                    temp[3].Offset(-1, 2);
                    if (this.CanRotate(temp)) //判断旋转后的位置是否合法,合法则更新
                    {
                        this.pos[2].Offset(1, 0);
                        this.pos[3].Offset(-1, 2);
                        this.shapeNO = 15;//设置当前编号
                    }
                    else
                        rotated = false;   //否则不能旋转
                    break;
                case 15:
                    temp[2].Offset(-1, -1);   //更改成旋转后的位置
                    temp[1].Offset(-1, -1);
                    temp[3].Offset(0, -2);
                    if (this.CanRotate(temp)) //判断旋转后的位置是否合法,合法则更新
                    {
                        this.pos[2].Offset(-1, -1);
                        this.pos[1].Offset(-1, -1);
                        this.pos[3].Offset(0, -2);
                        this.shapeNO = 16;//设置当前编号
                    }
                    else
                        rotated = false;   //否则不能旋转
                    break;
                case 16:
                    temp[0].Offset(-1, -1);   //更改成旋转后的位置
                    temp[1].Offset(-1, 1);
                    temp[2].Offset(0, 1);
                    temp[3].Offset(2, 1);
                    if (this.CanRotate(temp)) //判断旋转后的位置是否合法,合法则更新
                    {
                        this.pos[0].Offset(-1,-1);
                        this.pos[1].Offset(-1, 1);
                        this.pos[2].Offset(0, 1);
                        this.pos[3].Offset(2, 1);
                        this.shapeNO = 6;//设置当前编号
                    }
                    else
                        rotated = false;   //否则不能旋转
                    break;
                case 17:
                    temp[0].Offset(1,1);   //更改成旋转后的位置
                    temp[2].Offset(-1, -1);
                   // temp[2].Offset(-1, 1);
                    temp[3].Offset(0, -2);
                    if (this.CanRotate(temp)) //判断旋转后的位置是否合法,合法则更新
                    {
                        this.pos[0].Offset(1,1);
                        this.pos[2].Offset(-1, -1);
                        this.pos[3].Offset(0,-2);
                        this.shapeNO = 18;//设置当前编号
                    }
                    else
                        rotated = false;   //否则不能旋转
                    break;
                case 18:
                    temp[0].Offset(-1,1);
                    //temp[1].Offset(-1,0);   //更改成旋转后的位置
                    temp[2].Offset(1, -1);
                    temp[3].Offset(2, 0);
                    if (this.CanRotate(temp)) //判断旋转后的位置是否合法,合法则更新
                    {
                        pos[0].Offset(-1, 1);
                       // this.pos[1].Offset(-1,0);
                        this.pos[2].Offset(1, -1);
                        this.pos[3].Offset(2,0);
                        this.shapeNO = 19;//设置当前编号
                    }
                    else
                        rotated = false;   //否则不能旋转
                    break;
                case 19:
                    temp[0].Offset(-1,-1);
                    //temp[1].Offset(-1, -1);   //更改成旋转后的位置
                    temp[2].Offset(1,1);
                    temp[3].Offset(0,2);
                    if (this.CanRotate(temp)) //判断旋转后的位置是否合法,合法则更新
                    {
                        this.pos[0].Offset(-1,-1);
                       // this.pos[1].Offset(-1, -1);
                        this.pos[2].Offset(1,1);
                        this.pos[3].Offset(0,2);
                        this.shapeNO = 7;//设置当前编号
                    }
                    else
                        rotated = false;   //否则不能旋转
                    break;
            }
            return rotated;
        }


        /// <summary>
        /// 根据方向移动方块
        /// </summary>
        /// <param name="direction">方向编号</param>
        /// <returns></returns>
        public bool Move(int direction)
        {
            int offx = 0, offy = 0;
            if (direction == 0 && this.CanMove(0))  //左
            {
                offx = -1;
                offy = 0;
            }
            else if (direction == 1 && this.CanMove(1))//右
            {
                offx = 1;
                offy = 0;
            }
            else if (direction == 2 && this.CanMove(2)) //下
            {
                offx = 0;
                offy = 1;
            }
            else
            {
                return false;
            }
            this.SetLastPos();
            for (int i = 0; i < this.pos.Length; i++)
            {
                pos[i].Offset(offx, offy);
            }
            return true;

        }



        /// <summary>
        /// 极速下降方块
        /// </summary>
        public void Drop()
        {
            if (this.CanMove(2))
                this.SetLastPos();  //设置上一个位置
            while (this.CanMove(2))  //当前位置偏移
            {
                for (int i = 0; i < this.pos.Length; i++)
                {
                    pos[i].Offset(0, 1);
                }
            }

        }


        /// <summary>
        /// 绘制所有方块
        /// </summary>
        /// <param name="rec">不明</param>
        public void DrawBlocks(Rectangle rec)
        {
            Graphics gra = this.con.CreateGraphics();  //创建控件的画图对象

            if (this.pos != null)  //如果当前位置不为空，则在当前位置画方块
            {
                foreach (Point p in this.pos)
                {
                    this.DrawOne(p.X, p.Y, this.color, gra);
                }
            }


            //下面内容是遍历数组，绘制所有的方块，算法有待研究
            int x = rec.Height - 1;
            int y = rec.Width - 1;
            if ((x == this.unitPix && y == 4 * this.unitPix) || (x == 2 * unitPix && y == 2 * unitPix) ||
                (x == 2 * unitPix && y == 3 * unitPix) || (x == 3 * unitPix && y == 2 * unitPix) || (x == 4 * unitPix && y == unitPix))
                return;
            else
            {
                for (int i = this.huji.GetLength(0) - 1; i >= 0; i--)
                {
                    for (int j = this.huji.GetLength(1) - 1; j >= 0; j--)
                    {
                        if (huji[i, j] == true && i * unitPix - rec.Left > -unitPix && i * unitPix < rec.Right && j * unitPix - rec.Top > -unitPix && j * unitPix < rec.Bottom)
                        {
                            this.DrawOne(i, j, this.iori[i, j], gra);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
