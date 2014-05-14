using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessModel;
using System.Threading;
using System.Diagnostics;

namespace ChessGame
{
    public partial class Form1 : Form
    {
        private bool _isSelectFigure = false;
        private bool _isBlock = false;
        private int sx, sy; //selected x, selected y
        private Game _game;
        private AI _ai;
        private Player _curPlayer;
        private TypeOfGamer tWhite, tBlack; //тип белого/черного игрока
        private Step _lastStep;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = comboBox2.SelectedIndex = 0;
            Functions.InitDesk();
            tWhite = TypeOfGamer.Human;
            tBlack = TypeOfGamer.Human;
            _curPlayer = Player.White;
            _game = new Game();
            _ai = new AI();
            _lastStep = null;
            pictureBox1.Invalidate();
        }

        private void printBoard(Control panel, PaintEventArgs e)
        {
            //создаем буффер и контекст для него
            BufferedGraphics buffer;
            BufferedGraphicsContext context;
            //определяем контекст
            context = BufferedGraphicsManager.Current;
            //определяем размер контекста
            context.MaximumBuffer = new System.Drawing.Size(panel.Width + 1, panel.Height + 1);
            Rectangle rec = new Rectangle(0, 0, panel.Width, panel.Height);
            //на основе контекста создаем буфер
            buffer = context.Allocate(e.Graphics, rec);
            //отрисовка чего то там
            Brush brushWhite = new SolidBrush(Color.White);
            Brush brushBlack = new SolidBrush(Color.Black);
            Pen whiteP = new Pen(Color.White);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((j + i) % 2 == 1)
                    {
                        buffer.Graphics.FillRectangle(brushBlack, 70 * i, 70 * j, 70, 70);
                    }
                    else
                    {
                        buffer.Graphics.FillRectangle(brushWhite, 70 * i, 70 * j, 70,70);
                    }
                    buffer.Graphics.DrawRectangle(whiteP, 70 * i, 70 * j, 70, 70);
                }
            }
            if (_lastStep != null)
            {
                Brush c = new SolidBrush(Color.Yellow);
                buffer.Graphics.FillRectangle(c, 70 * _lastStep.fy, 70 * (7 - _lastStep.fx), 70, 70);
                buffer.Graphics.DrawRectangle(whiteP, 70 * _lastStep.fy, 70 * (7 - _lastStep.fx), 70, 70);
                buffer.Graphics.FillRectangle(c, 70 * _lastStep.ty, 70 * (7 - _lastStep.tx), 70, 70);
                buffer.Graphics.DrawRectangle(whiteP, 70 * _lastStep.ty, 70 * (7 - _lastStep.tx), 70, 70);
                _lastStep = null;
            }
            if (_isSelectFigure)
            {
                Brush green = new SolidBrush(Color.GreenYellow);
                buffer.Graphics.FillRectangle(green, 70 * sy, 70 * (7 - sx), 70, 70);
                buffer.Graphics.DrawRectangle(whiteP,70 * sy, 70 * (7 - sx), 70, 70);
                var steps = Figure._board[(sx << 3) + sy].getRightMove();
                Brush blue = new SolidBrush(Color.BlueViolet);
                Brush red = new SolidBrush(Color.Red);
                foreach (var x in steps)
                {
                    if (Figure._board[(x.tx << 3) + x.ty] == null)
                        buffer.Graphics.FillRectangle(blue, x.ty * 70, (7 - x.tx) * 70, 70, 70);
                    else
                        buffer.Graphics.FillRectangle(red, x.ty * 70, (7 - x.tx) * 70, 70, 70);
                    buffer.Graphics.DrawRectangle(whiteP, x.ty * 70, (7 - x.tx) * 70, 70, 70);
                }
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Figure._board[(i << 3) + j] != null)
                    {
                        string path = "Picture/" + Figure._board[(i << 3) + j].PictureName() + ".png";
                        Image img = Image.FromFile(path);
                        buffer.Graphics.DrawImage(img, 70 * j, 70 * (7-i));
                    }
                }
            }
            //закончили отрисовку
            //Выводим буффер на экран
            buffer.Render(e.Graphics);
            //и очищаем память
            buffer.Dispose();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            printBoard(pictureBox1, e);
            if (_curPlayer == Player.White && tWhite == TypeOfGamer.AI ||
                      _curPlayer == Player.Black && tBlack == TypeOfGamer.AI)
            {
                _isBlock = true;
                stringState.Text = "Вычисление хода";
                Thread thread = new Thread(AIStep);
                thread.Priority = ThreadPriority.Highest;
                thread.Start(_curPlayer);
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (_isBlock) return;
            int nsy = e.X / 70;
            int nsx = 7 - (e.Y / 70);
            if (_isSelectFigure)
            {
                if (nsx == sx && nsy == sy)
                {
                    _isSelectFigure = false;
                    pictureBox1.Invalidate();
                }
                else
                {
                    try
                    {
                        Step s = new Step(sx, sy, nsx, nsy);
                        _game.doMove(s);
                        addStepForHistory(s);
                        _curPlayer = _game.Player;
                    }
                    catch (ErrorStepExveption ex)
                    {
                        MessageBox.Show("Недопустимый ход!");
                    }
                    _isSelectFigure = false;
                    pictureBox1.Invalidate();
                }
            }
            else
            {
                sx = nsx;
                sy = nsy;
                if (Figure._board[(sx << 3) + sy] != null && Figure._board[(sx<<3)+sy].Player == _curPlayer)
                {
                    _isSelectFigure = true;
                    pictureBox1.Invalidate();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0) tBlack = TypeOfGamer.Human;
            else tBlack = TypeOfGamer.AI;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void addStepForHistory(Step s)
        {
            if (listView1.InvokeRequired)
                listView1.BeginInvoke(new Action<string>(str => listView1.Items.Add(str)), s.ToString());
            else listView1.Items.Add(s.ToString());
        }

        private void AIStep(object p)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Step s = _ai.selectMove((Player)p, 6);
            _game.doMove(s);
             addStepForHistory(s);
            _lastStep = s;
            ThreadEnd();
            timer.Stop();
            if (labelTime.InvokeRequired)
                labelTime.BeginInvoke(new Action<string>(x => labelTime.Text = x), timer.ElapsedMilliseconds.ToString());
            else labelTime.Text = timer.ElapsedMilliseconds.ToString();
        }

        private void ThreadEnd()
        {
            pictureBox1.Invalidate();
            if (stringState.InvokeRequired)
                stringState.BeginInvoke(new Action<string> (s => stringState.Text = s), "Ходит игрок");
            else stringState.Text = "Ходит игрок";
            _curPlayer = _game.Player;
            _isBlock = false;
        }
    }
}
