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
        private TypeOfGamer _tWhite, _tBlack; //тип белого/черного игрока
        private Step _lastStep;
		private Board _mainBoard = new Board();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = comboBox2.SelectedIndex = 0;
            Functions.Precalc();
            _tWhite = TypeOfGamer.Human;
            _tBlack = TypeOfGamer.Human;
            _curPlayer = Player.White;
            _game = new Game(_mainBoard);
            _ai = new AI();
            _lastStep = null;
            pictureBox1.Invalidate();
        }

        private void PrintBoard(Control panel, PaintEventArgs e)
        {
            //создаем буффер и контекст для него
            //определяем контекст
            BufferedGraphicsContext context = BufferedGraphicsManager.Current;
            //определяем размер контекста
            context.MaximumBuffer = new Size(panel.Width + 1, panel.Height + 1);
            var rec = new Rectangle(0, 0, panel.Width, panel.Height);
            //на основе контекста создаем буфер
            BufferedGraphics buffer = context.Allocate(e.Graphics, rec);
            //отрисовка чего то там
            Brush brushWhite = new SolidBrush(Color.White);
            Brush brushBlack = new SolidBrush(Color.Black);
            var whiteP = new Pen(Color.White);
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
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
                buffer.Graphics.FillRectangle(c, 70 * _lastStep.FromY, 70 * (7 - _lastStep.FromX), 70, 70);
                buffer.Graphics.DrawRectangle(whiteP, 70 * _lastStep.FromY, 70 * (7 - _lastStep.FromX), 70, 70);
                buffer.Graphics.FillRectangle(c, 70 * _lastStep.ToY, 70 * (7 - _lastStep.ToX), 70, 70);
                buffer.Graphics.DrawRectangle(whiteP, 70 * _lastStep.ToY, 70 * (7 - _lastStep.ToX), 70, 70);
                _lastStep = null;
            }
            if (_isSelectFigure)
            {
                Brush green = new SolidBrush(Color.GreenYellow);
                buffer.Graphics.FillRectangle(green, 70 * sy, 70 * (7 - sx), 70, 70);
                buffer.Graphics.DrawRectangle(whiteP,70 * sy, 70 * (7 - sx), 70, 70);
                var steps = _mainBoard[(sx << 3) + sy].GetRightMove();
                var allSteps = _game.getAllLegalMoves(_curPlayer);
                var rSteps = steps.Where(x =>
                {
                    var s = allSteps.FirstOrDefault(y => x.FromX == y.FromX &&
                                                         x.FromY == y.FromY &&
                                                         x.ToX == y.ToX &&
                                                         x.ToY == y.ToY);
                    return s != null;
                }
                    );
                Brush blue = new SolidBrush(Color.BlueViolet);
                Brush red = new SolidBrush(Color.Red);
                foreach (var x in rSteps)
                {
	                buffer.Graphics.FillRectangle(_mainBoard[(x.ToX << 3) + x.ToY] == null ? blue : red, x.ToY*70,
		                (7 - x.ToX)*70, 70, 70);
	                buffer.Graphics.DrawRectangle(whiteP, x.ToY * 70, (7 - x.ToX) * 70, 70, 70);
                }
            }
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    if (_mainBoard[(i << 3) + j] != null)
                    {
                        var path = "Picture/" + _mainBoard[(i << 3) + j].PictureName() + ".png";
                        var img = Image.FromFile(path);
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
            PrintBoard(pictureBox1, e);
            if (_curPlayer == Player.White && _tWhite == TypeOfGamer.AI ||
                      _curPlayer == Player.Black && _tBlack == TypeOfGamer.AI)
            {
                _isBlock = true;
                stringState.Text = "Вычисление хода";
                var thread = new Thread(AIStep) {Priority = ThreadPriority.Highest};
                thread.Start(_curPlayer);
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (_isBlock) return;
            var nsy = e.X / 70;
            var nsx = 7 - (e.Y / 70);
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
                        var s = new Step(sx, sy, nsx, nsy);
                        _game.doMove(s);
                        AddStepForHistory(s);
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
	            if (_mainBoard[(sx << 3) + sy] == null || _mainBoard[(sx << 3) + sy].Player != _curPlayer) return;
	            _isSelectFigure = true;
	            pictureBox1.Invalidate();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) _tWhite = TypeOfGamer.Human;
            else _tWhite = TypeOfGamer.AI;
            pictureBox1.Invalidate();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0) _tBlack = TypeOfGamer.Human;
            else _tBlack = TypeOfGamer.AI;
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void AddStepForHistory(Step s)
        {
            if (listView1.InvokeRequired)
                listView1.BeginInvoke(new Action<string>(str => listView1.Items.Add(str)), s.ToString());
            else listView1.Items.Add(s.ToString());
        }

        private void AIStep(object p)
        {
            var timer = new Stopwatch();
            timer.Start();
            _ai.Count = 0;
            var state = _game.calcState();
            if (state == State.Draw)
            {
                MessageBox.Show("Пат");
                return;
            }
            if (state == State.Checkmate)
            {
                MessageBox.Show("Компьютерный игрок проиграл");
                return;
            }
            var s = _ai.SelectMove((Player)p, 6, _mainBoard);
            _game.doMove(s);
             AddStepForHistory(s);
            _lastStep = s;
            ThreadEnd();
            timer.Stop();
            if (labelTime.InvokeRequired)
                labelTime.BeginInvoke(new Action<string>(x => labelTime.Text = x), timer.ElapsedMilliseconds.ToString()+
                    " Просмотренно узлов: " + _ai.Count.ToString());
            else labelTime.Text = timer.ElapsedMilliseconds.ToString();
        }

        private void ThreadEnd()
        {
            pictureBox1.Invalidate();
            if (stringState.InvokeRequired)
                stringState.BeginInvoke(new Action<string> (s => stringState.Text = s), "Ходит игрок");
            else stringState.Text = "Ходит игрок";
            _curPlayer = _game.Player;
            var state = _game.calcState();
            if (state == State.Check) MessageBox.Show("Шах!");
            if (state == State.Checkmate) MessageBox.Show("Мат!");
            if (state == State.Draw) MessageBox.Show("Пат");
            _isBlock = false;
        }
    }
}
