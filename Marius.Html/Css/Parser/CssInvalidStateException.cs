using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marius.Html.Css.Parser
{
    [Serializable]
    public class CssInvalidStateException: Exception
    {
        public CssInvalidStateException() { }
        public CssInvalidStateException(string message) : base(message) { }
        public CssInvalidStateException(string message, Exception inner) : base(message, inner) { }
        protected CssInvalidStateException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
