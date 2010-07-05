using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marius.Html.Dom
{
    public class ElementAttribute
    {
        public static readonly IComparer<ElementAttribute> Comparer = new ElementAttributeComparer();

        private class ElementAttributeComparer: IComparer<ElementAttribute>
        {
            public int Compare(ElementAttribute x, ElementAttribute y)
            {
                if (x == null && y == null)
                    return 0;

                if (x == null)
                    return -1;

                if (y == null)
                    return 1;

                return StringComparer.InvariantCultureIgnoreCase.Compare(x.Value, y.Value);
            }
        }

        public string Name { get; private set; }
        public string Value { get; set; }

        public ElementAttribute(string name)
            : this(name, null)
        {
        }

        public ElementAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            if (Value == null)
                return Name;
            else
                return string.Format("{0}='{1}'", Name, Value);
        }
    }
}
