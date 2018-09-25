using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace EvalExpr
{
    public static class Lexer
    {

        public static Token Tokenize(ref int pos, string expr)
        {
            while (expr[pos] == ' ' || expr[pos] == '\n')
                pos++;
            char c = expr[pos];
            
            switch (c)
            {
               case '+':
                   pos++;
                   return new Token(Token.Type.PLUS, "+");
               case '-':
                   pos++;
                   return new Token(Token.Type.MINUS, "-");
               case '/':
                   pos++;
                   return new Token(Token.Type.DIV, "/");
               case '*':
                   pos++;
                   return new Token(Token.Type.MULT, "*");
               case '(':
                   pos++;
                   return new Token(Token.Type.OPEN, "(");
                case ')':
                    pos++;
                    return new Token(Token.Type.CLOSE, ")");
            }

            string integer = "";
            int l = expr.Length;
            while (c >= '0' && c <= '9')
            {
                integer += c;
                pos++;
                if(pos == l) break;
                c = expr[pos];
            }
            
            if(integer != "")
                return new Token(Token.Type.INT, integer);
            
            throw new ArgumentException("Cannot interpret character :" + c);
            
            
        }
        
        public static List<Token> Lex(string expr)
        {
            List<Token> tokens = new List<Token>();
            int l = expr.Length;
            int i = 0;
            
            while (i < l)
            {
                tokens.Add(Tokenize(ref i, expr));
            }

            return tokens;
        }
    }
}