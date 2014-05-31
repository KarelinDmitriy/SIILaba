using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public sealed class StepFromPosition
    {
#region variable
        public Ray[] Rays;
        public Dictionary<int, int> Attack; //достяжима ли координата <1>, если да, то из какого луча <2>
#endregion 

#region public methods
        public StepFromPosition(int len)
        {
            Rays = new Ray[len];
            Attack = new Dictionary<int, int>();
        }
#endregion

#region private methods

#endregion
    }
}
