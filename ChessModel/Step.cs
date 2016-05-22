using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Step
    {
#region variable

	    #endregion 

#region public methods

        public Step(int fromX, int fromY, int toX, int toY)
        {
            this.FromX = fromX;
            this.FromY = fromY;
            this.ToX = toX;
            this.ToY = toY;
        }

        public Step(Step s)
        {
            FromX = s.FromX;
            FromY = s.FromY;
            ToX = s.ToX;
            ToY = s.ToY;
        }

        public static Step StringToStep(string a)
        {
            var y1 = a[0] - 'a';
            var x1 = int.Parse(a[1]+"")-1;
            var y2 = a[2] - 'a';
            var x2 = int.Parse(a[3] + "")-1;
            return new Step(x1, y1, x2, y2);
        }

        public int FromX { get; set; }

	    public int FromY { get; set; }

	    public int ToX { get; set; }

	    public int ToY { get; set; }

	    public override string ToString()
        {
            var a = "";
            a += (FromX + 1).ToString();
            a += (char)('a' + FromY);
            a += " - ";
            a += (ToX + 1).ToString();
            a += (char)('a' + ToY);
            return a;
        }
#endregion

#region private methods

#endregion      
    }
}
