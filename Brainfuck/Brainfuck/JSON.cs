using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.IO;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Brainfuck
{
    class JSONElement
    {
        public enum JSONType
        {
            DIC,
            LIST,
            STR,
            NB,
            BOOL,
            NULL
        }
        
        private JSONType type;
        
        public bool bool_value;
        public int int_value;
        public string string_value;
        public List<JSONElement> data;
        public List<string> key;

        public JSONElement(JSONType type)
        {
            this.type = type;
            if (type == JSONType.LIST || type == JSONType.DIC)
                data = new List<JSONElement>();
            if (type == JSONType.DIC)
                key = new List<string>();
        }

        public JSONType Type
        {
            get { return this.type; }
        }
        
    }

    static class JSON
    {

        public static JSONElement.JSONType GetJsonType(char c)
        {
            if(c == '"')
                return JSONElement.JSONType.STR;
            if (c >= '0' && c <= '9' || c == '-')
                return JSONElement.JSONType.NB;
            if(c == '[')
                return JSONElement.JSONType.LIST;
            if(c == 't' || c =='f')
                return JSONElement.JSONType.BOOL;
            if(c == 'n')
                return JSONElement.JSONType.NULL;
            if(c == '{')
                return JSONElement.JSONType.DIC;

            throw new ArgumentException("Invalid Type");
        }

        public static string ParseString(string json, ref int index)
        {
            string name = "\"";
            int nb_backslash = 0;
            for (index++; index < json.Length; index++)
            {
                char c = json[index];

                if (c == '\\')
                {
                    nb_backslash++;
                    if (index + 1 < json.Length)
                    {
                        index++;
                        c = json[index];
                        name += c;
                        if (c == '"')
                            break;
                        continue;
                    }
                }
                name += c;
            }

            index -= nb_backslash;
            return name;
        }


        public static int ParseInt(string json, ref int index)
        {
            string integer = "" + json[index];
            for (index++; index < json.Length && json[index] >= 0 && json[index] <= 9; index++)
                integer += json[index];
            
            return int.Parse(integer);
        }

        public static bool ParseBool(string json, ref int index)
        {
            char c = json[index];
            if (c == 't')
            {
                index += 4;
                return true;
            }

            index += 5;
            return false;
        }

        public static void EatBlank(string json, ref int index)
        {
            while(index < json.Length && (json[index] == ' ' || json[index] == '\t' || json[index] == '\n' || json[index] == '\r'))
                index++;
        }
        
        public static JSONElement ParseJSONString(string json, ref int index)
        {
            if (index >= json.Length)
                return null;
            
            JSONElement jelement = new JSONElement(GetJsonType(json[index]));
            
            for (int i = 0; i < json.Length; i++)
            {
                JSONElement.JSONType jtype = jelement.Type;
                switch (jtype)
                {
                    case JSONElement.JSONType.BOOL:
                        
                         break;
                    case JSONElement.JSONType.DIC:
                        break;
                    case JSONElement.JSONType.LIST:
                        break;
                    case JSONElement.JSONType.NB:
                        break;
                    case JSONElement.JSONType.NULL:
                        break;
                    case JSONElement.JSONType.STR:
                        break;
                }
            }

            return jelement;
        }
        
        public static JSONElement ParseJSONFile(string file)
        {
            // TODO
            throw new NotImplementedException();
        }

        public static void PrintJSON(JSONElement el)
        {
            // TODO
            throw new NotImplementedException();
        }

        public static JSONElement SearchJSON(JSONElement element, string key)
        {
            // TODO
            throw new NotImplementedException();
            return null;
        }
    }   
}