using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marius.Html.Css.Parser
{
    public interface IInputSource
    {
        char this[int count] { get; }
        char Current { get; }

        string Value { get; }
        
        bool Eof { get; }

        void ClearValue();

        void MoveNext();
        void Skip(int count);
        void PushState();
        void PopState(bool discard);
    }
}
