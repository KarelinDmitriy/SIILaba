﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChessModel
{
    public class ErrorStepExveption : Exception
    {
        string errTok;
        public ErrorStepExveption() : base() { }
        public ErrorStepExveption(string str) : base(str) { }
        public ErrorStepExveption(string msg, string info)
            : base(msg)
        {
            errTok = info;
        }
        public ErrorStepExveption(string str, Exception inner) { }
        protected ErrorStepExveption(
            SerializationInfo si,
            StreamingContext sc) :
            base(si, sc) { }

        public string ErrTok
        {
            get { return errTok; }
        }

        public override string ToString()
        {
            return Message;
        }

    }

}
