using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Xml.XPath;

namespace Brainfuck
{
    class Brainfuck
    {
        /**
         * Interpret @code with @symbols and return the resulting string.
         * In case of error,  an ArgumentException is raised.
         * Possible error cases are:
         *     - Invalid braces number / order
         *     - Symbol not present in @symbols
         */
        public static string Interpret(string code, Dictionary<char, char> symbols)
        {   
            Stack<int> loop_save = new Stack<int>();
            byte[] strip = new byte[30000];
            int cursor = 15000;
            string result = "";
            
            if(!symbols.ContainsKey('+') || !symbols.ContainsKey('-') || !symbols.ContainsKey('.') || 
               !symbols.ContainsKey(',') || !symbols.ContainsKey('>') || !symbols.ContainsKey('<') || 
               !symbols.ContainsKey('[') || !symbols.ContainsKey(']'))
                throw new ArgumentException("Symbol not found");
            
            for (int i = 0; i < code.Length; i++)
            {
                char c = code[i];
                if(!symbols.ContainsValue(code[i]))
                    throw new ArgumentException("Unknown symbol : " + code[i]);
                
                //+
                if (c == symbols['+'])
                    strip[cursor]++;
                //-
                else if (c == symbols['-'])
                    strip[cursor]--;
                //>
                else if (c == symbols['>'])
                {
                    cursor++;
                    if (cursor == 30000)
                        cursor = 0;
                }
                //<
                else if (c == symbols['<'])
                {
                    cursor--;
                    if (cursor == -1)
                        cursor = 29999;
                }
                //.
                else if (c == symbols['.'])
                    result += (char) strip[cursor];
                //,
                else if (c == symbols[','])
                    strip[cursor] = (byte) Console.Read();
                
                /*
                 * LOOP
                 */
                
                //[
                else if (c == symbols['['])
                {

                    if (strip[cursor] != 0)
                        loop_save.Push(i);
                    else
                        SearchBracket(code, ref i, symbols);
                        
                    
                }
                    
                //]
                else if (c == symbols[']'])
                {
                    if (strip[cursor] != 0)
                    {
                        if (loop_save.Count == 0)
                            throw new ArgumentException("Corresponding - open loop symbol - not found");
                        i = loop_save.Peek();
                    }
                    else
                        loop_save.Pop();
                    //Console.WriteLine("Closing Bracket found");
                }
            }
            
            return result;
        }

        public static void SearchBracket(string code, ref int i, Dictionary<char, char> symbols)
        {
            int nb_br = 0;
            i++;
            char cb = symbols[']'];
            char ob = symbols['['];
            for (;  i < code.Length; i++)
            {
                char c = code[i];
                if (c == cb && nb_br == 0)
                    return;
                if (c == cb)
                    nb_br--;
                else if (c == ob)
                    nb_br++;
            }
            throw new ArgumentException("Corresponding - close loop symbol - not found");
        }

        
        /**
         * Generate the brainfuck code to print @text with @symbols.and return it.
         * In case of error, an ArgumentException is raised.
         * Possible error case is:
         *     - Missing symbol in @symbols
         */
        public static string GenerateCodeFromText(string text, Dictionary<char, char> symbols)
        {
            if(!symbols.ContainsKey('+') || !symbols.ContainsKey('-') || !symbols.ContainsKey('.') || 
               !symbols.ContainsKey(',') || !symbols.ContainsKey('>') || !symbols.ContainsKey('<') || 
               !symbols.ContainsKey('[') || !symbols.ContainsKey(']'))
                throw new ArgumentException("Symbol not found");
            
            char plus = symbols['+'];
            char point = symbols['.'];
            char chevron = symbols['>'];
            string code = "";
            
            for (int i = 0; i < text.Length - 1; i++)
            {
                for (int j = 0; j < text[i]; j++)
                {
                    code += plus;
                }

                code += point +""+ chevron;
            }
            for (int j = 0; j < text[text.Length - 1]; j++)
            {
                code += plus;
            }

            code += point;

            return code;
        }
        
         /**
         * Shorten @program with @symbols and return the resulting string.
         * In case of error, an ArgumentException is raised.
         * Possible error cases are:
         *     - Symbol not present in @symbols
         *     - Invalid symbol
         */
        public static string ShortenCode(string program, Dictionary<char, char> symbols)
        {
            
            if(!symbols.ContainsKey('+') || !symbols.ContainsKey('-') || !symbols.ContainsKey('.') || 
               !symbols.ContainsKey(',') || !symbols.ContainsKey('>') || !symbols.ContainsKey('<') || 
               !symbols.ContainsKey('[') || !symbols.ContainsKey(']'))
                throw new ArgumentException("Symbol not found");

            char plus = symbols['+'];
            string bracket_sup = symbols['['] + "" + symbols['>'];
            string inf_minus_bracket_sup = symbols['<'] + "" + symbols['-'] + "" + symbols[']'] + "" + symbols['>']; 
            
            bool modified = false;
            string short_program = "";
            int length = program.Length;
            
            for (int i = 0; i < length; i++)
            {
                char c = program[i];
                if(!symbols.ContainsValue(c))
                    throw new ArgumentException("Unknown symbol");

                if (c != plus)
                    short_program += c;
                else
                {
                    int count_plus = 0;
                    while (i < length && program[i] == plus)
                    {
                        count_plus++;
                        i++;
                    }

                    if (count_plus != 0)
                        i--;
                    //Si moins de 12 '+', les boucles rallongent le code
                    if (count_plus > 11)
                    {
                        modified = true;
                        int r = 0;
                        int m = 0;
                        int min_m = count_plus;
                        int min_d = count_plus;
                        int min_r = count_plus;
                        for (int d = 2; d < count_plus; d++)
                        {
                            m = count_plus / d;
                            r = count_plus % d;
                            if (min_d + min_m + min_r > m + r + d)
                            {
                                min_d = d;
                                min_m = m;
                                min_r = r;
                            }
                        }

                        for (int j = 0; j < min_d; j++)
                            short_program += plus;
                        short_program += bracket_sup;
                        for (int j = 0; j < min_m; j++)
                            short_program += plus;
                        short_program += inf_minus_bracket_sup;
                        for (int j = 0; j < min_r; j++)
                            short_program += plus;
                    }
                    else
                    {
                        for (int j = 0; j < count_plus; j++)
                            short_program += plus;
                    }
                }
            }

            if (modified)
                return short_program;//return ShortenCode(short_program, symbols);
            return program;
        }
    }
}
