﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class Chessboard
    {
#region variable
        Figure[,] _matrix;
#endregion 

#region public methods
        public Chessboard()
        {
            _matrix = new Figure[8, 8];
            InitDesk();
        }

#endregion

#region private methods
        private void InitDesk()
        {
            throw new NotImplementedException();
        }
#endregion
    }
}
