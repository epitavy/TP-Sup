using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Remoting.Messaging;

namespace EvalExpr
{
    public class UnaryNode : INode
    {
        private bool _positive;
        private INode _val;

        public UnaryNode(bool positive)
        {
            _positive = positive;
            _val = null;
        }

        public void Build(Stack<INode> output)
        {
            _val = output.Pop();
            _val.Build(output);
        }

        public void Print()
        {
            Console.Write(_positive ? "+" : "-");
            _val.Print();
        }
        
        public void PrintRevertPolish()
        {
            _val.PrintRevertPolish();
            Console.Write(_positive ? "+" : "-");
        }

        public int Eval()
        {
            return (_positive ? 1 : -1) * _val.Eval();
        }
    }
}