using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices.WindowsRuntime;

namespace EvalExpr
{
    public static class Parser
    {
        public static INode Parse(string expr)
        {
            List<Token> tokens = Lexer.Lex(expr);
            Stack<Token> operators = new Stack<Token>();
            Stack<UnaryNode> unary = new Stack<UnaryNode>();
            Stack<INode> output = new Stack<INode>();

            
            
            for(int i = 0; i < tokens.Count; i++)
            {
                Token token = tokens[i];
                //Si on a un operateur qui peut etre unaire, alors on regarde a gauche ce qu'il y a
                if ((token.TokType == Token.Type.PLUS || token.TokType == Token.Type.MINUS) &&
                    (i - 1 < 0 || tokens[i - 1].TokType != Token.Type.INT && 
                     token.TokType != Token.Type.OPEN &&
                     token.TokType != Token.Type.CLOSE))
                {
                    if(token.TokType == Token.Type.MINUS)
                        unary.Push(new UnaryNode(false));
                    else if(token.TokType == Token.Type.PLUS)
                        unary.Push(new UnaryNode(true));
                }
                else{
                    switch (token.TokType)
                    {
                        case Token.Type.PLUS:
                                
                            while(operators.Count > 0)
                            {
                                Token.Type toktype = operators.Peek().TokType;
                                if (toktype != Token.Type.OPEN)
                                {
                                    operators.Pop();
                                                                        
                                    switch (toktype)
                                    {
                                        case Token.Type.DIV:
                                            output.Push(new BinaryNode(BinaryNode.Operator.DIV));
                                            break;
                                        case Token.Type.MINUS:
                                            output.Push(new BinaryNode(BinaryNode.Operator.MINUS));
                                            break;
                                        case Token.Type.MULT:
                                            output.Push(new BinaryNode(BinaryNode.Operator.MULT));
                                            break;
                                        case Token.Type.PLUS:
                                            output.Push(new BinaryNode(BinaryNode.Operator.PLUS));
                                            break;
                                    }
                                }
                                else
                                    break;
                            } 
                            operators.Push(token);
                        break;
                            
                        case Token.Type.MINUS:
                            while(operators.Count > 0)
                            {
                                Token.Type toktype = operators.Peek().TokType;
                                if (toktype != Token.Type.OPEN)
                                {
                                    operators.Pop();
                                                                        
                                    switch (toktype)
                                    {
                                        case Token.Type.DIV:
                                            output.Push(new BinaryNode(BinaryNode.Operator.DIV));
                                            break;
                                        case Token.Type.MINUS:
                                            output.Push(new BinaryNode(BinaryNode.Operator.MINUS));
                                            break;
                                        case Token.Type.MULT:
                                            output.Push(new BinaryNode(BinaryNode.Operator.MULT));
                                            break;
                                        case Token.Type.PLUS:
                                            output.Push(new BinaryNode(BinaryNode.Operator.PLUS));
                                            break;
                                    }
                                }
                                else
                                    break;
                            } 
                            operators.Push(token);
                            break;
                        case Token.Type.MULT:
                            operators.Push(token);
                            break;
                        case Token.Type.DIV:
                            operators.Push(token);
                            break;
                        case Token.Type.INT:
                            output.Push(new ValueNode(int.Parse(token.Val)));
                            if(unary.Count > 0)
                                output.Push(unary.Pop());
                            break;
                        case Token.Type.OPEN:
                            operators.Push(token);
                            break;
                        case Token.Type.CLOSE:
                            
                            if(operators.Count <= 0)
                                throw new ArgumentException("Matching parenthesis not found");

                            Token current = operators.Pop();
                            while(current.TokType != Token.Type.OPEN)
                            {
                                
                                switch (current.TokType)
                                {
                                    case Token.Type.DIV:
                                        output.Push(new BinaryNode(BinaryNode.Operator.DIV));
                                        break;
                                    case Token.Type.MINUS:
                                        output.Push(new BinaryNode(BinaryNode.Operator.MINUS));
                                        break;
                                    case Token.Type.MULT:
                                        output.Push(new BinaryNode(BinaryNode.Operator.MULT));
                                        break;
                                    case Token.Type.PLUS:
                                        output.Push(new BinaryNode(BinaryNode.Operator.PLUS));
                                        break;
                                }
                                
                                if(operators.Count <= 0)
                                    throw new ArgumentException("Matching parenthesis not found");
                                current = operators.Pop();
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            while (operators.Count > 1)
            {
                Token token = operators.Pop();
                switch (token.TokType)
                {
                    case Token.Type.PLUS:
                        output.Push(new BinaryNode(BinaryNode.Operator.PLUS));
                        break;
                    case Token.Type.MINUS:
                        output.Push(new BinaryNode(BinaryNode.Operator.MINUS));
                        break;
                    case Token.Type.MULT:
                        output.Push(new BinaryNode(BinaryNode.Operator.MULT));
                        break;
                    case Token.Type.DIV:
                        output.Push(new BinaryNode(BinaryNode.Operator.DIV));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            if(operators.Count > 0)
            {
                Token rootTok = operators.Pop();
                switch (rootTok.TokType)
                {
                    case Token.Type.PLUS:
                        BinaryNode root1 = new BinaryNode(BinaryNode.Operator.PLUS);
                        root1.Build(output);
                        return root1;
                        break;
                    case Token.Type.MINUS:
                        BinaryNode root2 = new BinaryNode(BinaryNode.Operator.MINUS);
                        root2.Build(output);
                        return root2;
                        break;
                    case Token.Type.MULT:
                        BinaryNode root3 = new BinaryNode(BinaryNode.Operator.MULT);
                        root3.Build(output);
                        return root3;
                        break;
                    case Token.Type.DIV:
                        BinaryNode root4 = new BinaryNode(BinaryNode.Operator.DIV);
                        root4.Build(output);
                        return root4;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            //Si il n'y a aucun operateur binaire, alors on monte l'arbre
            //à partir du 1er element de la pile "output"
            INode root5 = output.Pop();
            root5.Build(output);
            return root5;
        }
    }
}