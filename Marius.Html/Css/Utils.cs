#region License
/*
Distributed under the terms of an MIT-style license:

The MIT License

Copyright (c) 2010 Marius Klimantavičius

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Marius.Html.Css
{
    public static class Utils
    {
        public static string Escape(this string value)
        {
            // \"([^\n\r\f\\"]|\\{nl}|{escape})*\"
            StringBuilder sb = new StringBuilder(value);

            sb.Replace("\n", "\\A ");
            sb.Replace("\r", "\\D ");
            sb.Replace("\f", "\\C ");
            sb.Replace("\"", "\\\"");

            return sb.ToString();
        }

        public static string EscapeIdentifier(this string value)
        {
            //-?{nmstart}{nmchar}*
            //[_a-z]|{nonascii}|{escape}
            //[_a-z0-9-]|{nonascii}|{escape}

            StringBuilder sb = new StringBuilder();

            int start = 0;
            if (start < value.Length)
            {
                if (value[start] == '-')
                {
                    sb.Append(value[start]);
                    start++;
                }
            }

            if (start < value.Length)
            {
                if (value[start] == '_' || (Char.ToLowerInvariant(value[start]) >= 'a' && Char.ToLowerInvariant(value[start]) <= 'z') || (int)value[start] >= 0x80)
                    sb.Append(value[start]);
                else
                    sb.AppendFormat("\\{0} ", (Char.ConvertToUtf32(value, start)).ToString("X"));
                start++;
            }

            for (int i = start; i < value.Length; i++)
            {
                if (value[i] == '_' || value[i] == '-' || (Char.ToLowerInvariant(value[i]) >= 'a' && Char.ToLowerInvariant(value[i]) <= 'z') || (int)value[i] >= 0x80 || (value[i] >= '0' && value[i] <= '9'))
                    sb.Append(value[i]);
                else
                    sb.AppendFormat("\\{0} ", (Char.ConvertToUtf32(value, start)).ToString("X"));
            }

            return sb.ToString();
        }

        public static bool ArraysEqual<T>(this T[] first, T[] second)
            where T: IEquatable<T>
        {
            if(first.Length!=second.Length)
                return false;
            
            for (int i = 0; i < first.Length; i++)
                if(!first[i].Equals((T)second[i]))
                    return false;

            return true;
        }

        public static int ArrayHashCode(this object[] array)
        {
            int result = 0;

            for (int i = 0; i < array.Length; i++)
            {
                result += array[i].GetHashCode();
                result = (result << 3) ^ result;
            }

            return result;
        }

        public static int GetHashCode(params object[] items)
        {
            int result = 0;
            
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null)
                {
                    if (items[i] is object[])
                        result += ArrayHashCode((object[])items[i]);
                    else
                        result += items[i].GetHashCode();
                }
                result = (result << 3) ^ result;
            }

            return result;
        }
    }
}
