using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public sealed class Ray
    {
        public Ray NextRay;
        public Step step;

        public Ray(Step s, Ray next = null)
        {
            step = s;
            NextRay = next;
        }
    }
}
