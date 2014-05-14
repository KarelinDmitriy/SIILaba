using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public sealed class StepsNode
    {
        Step _step;
        StepsNode nextNode;
        StepsNode nextSpecialNode;

        public StepsNode(Step s, StepsNode next = null, StepsNode special = null)
        {
            _step = s;
            nextNode = next;
            nextSpecialNode = special;
        }

        public Step step
        {
            get { return _step; }
        }

        public StepsNode NextNode
        {
            get { return nextNode; }
            set { nextNode = value; }
        }

        public StepsNode NextSpecialNode
        {
            get { return nextSpecialNode; }
            set { nextSpecialNode = value; }
        }
    }
}
