using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace EvalExpr
{
    public class BinaryNode : INode
    {
        public enum Operator
        {
            PLUS,
            MINUS,
            MULT,
            DIV
        };

        private Operator _op;
        private INode _left;
        private INode _right;

        public Operator Op
        {
            get { return _op; }
        }

        public BinaryNode(Operator op)
        {
            _op = op;
            _left = null;
            _right = null;
        }

        public void Build(Stack<INode> output)
        {
            if (output.Count > 0)
            {
                _right = output.Pop();
                _right.Build(output);
                
                _left = output.Pop();
                _left.Build(output);
            }
        }
 
        public void Print()
        {
            Console.Write("(");
            _left.Print();
            switch (_op)
            {
                case Operator.PLUS:
                    Console.Write(" + ");
                    break;
                case Operator.MINUS:
                    Console.Write(" - ");
                    break;
                case Operator.MULT:
                    Console.Write(" * ");
                    break;
                case Operator.DIV:
                    Console.Write(" / ");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _right.Print();
            Console.Write(") ");
        }

        public void PrintRevertPolish()
        {
            _left.PrintRevertPolish();
            Console.Write(" ");
            _right.PrintRevertPolish();
            switch (_op)
            {
                case Operator.PLUS:
                    Console.Write(" + ");
                    break;
                case Operator.MINUS:
                    Console.Write(" - ");
                    break;
                case Operator.MULT:
                    Console.Write(" * ");
                    break;
                case Operator.DIV:
                    Console.Write(" / ");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        
        public int Eval()
        {
            int leftEval = _left.Eval();
            int rightEval = _right.Eval();
            switch (_op)
            {
                case Operator.PLUS:
                    return leftEval + rightEval;
                    break;
                case Operator.MINUS:
                    return leftEval - rightEval;
                    break;
                case Operator.MULT:
                    return leftEval * rightEval;
                    break;
                case Operator.DIV:
                    return leftEval / rightEval;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}