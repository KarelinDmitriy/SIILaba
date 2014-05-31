﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Step
    {
#region variable
        int _fx;
        int _fy;
        int _tx;
        int _ty;

#endregion 

#region public methods

        public Step(int fx, int fy, int tx, int ty)
        {
            _fx = fx;
            _fy = fy;
            _tx = tx;
            _ty = ty;
        }

        public Step(Step s)
        {
            _fx = s.fx;
            _fy = s.fy;
            _tx = s.tx;
            _ty = s.ty;
        }

        public static Step stringToStep(string a)
        {
            var y1 = a[0] - 'a';
            var x1 = int.Parse(a[1]+"")-1;
            var y2 = a[2] - 'a';
            var x2 = int.Parse(a[3] + "")-1;
            return new Step(x1, y1, x2, y2);
        }

        public int fx 
        {
            get { return _fx; }
            set { _fx = value; } 
        }
        public int fy 
        { 
            get { return _fy; }
            set { _fy = value; }
        }
        public int tx
        {
            get { return _tx; }
            set { _tx = value; } 
        }
        public int ty
        {
            get { return _ty; }
            set { _ty = value; } 
        }

        public override string ToString()
        {
            var a = "";
            a += (fx + 1).ToString();
            a += (char)('a' + fy);
            a += " - ";
            a += (tx + 1).ToString();
            a += (char)('a' + ty);
            return a;
        }
#endregion

#region private methods

#endregion      
    }
}
