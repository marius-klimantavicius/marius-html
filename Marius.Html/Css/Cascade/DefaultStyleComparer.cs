using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marius.Html.Css.Dom;

namespace Marius.Html.Css.Cascade
{
    public class DefaultStyleComparer: IComparer<CssPreparedStyle>
    {
        public static readonly DefaultStyleComparer Instance = new DefaultStyleComparer();

        public int Compare(CssPreparedStyle x, CssPreparedStyle y)
        {
            if (object.ReferenceEquals(x, y))
                return 0;

            int xweight, yweight;
            xweight = Importance(x);
            yweight = Importance(y);

            if (xweight != yweight)
                return -(xweight - yweight);

            int speccomp = x.Selector.Specificity.CompareTo(y.Selector.Specificity);
            if (speccomp != 0)
                return -speccomp;

            return -(x.Index - y.Index);
        }

        private int Importance(CssPreparedStyle s)
        {
            /*
               1 1. user agent declarations
               2 1.1 user agent important
               3 2. user normal declarations
               4 3. author normal declarations
               5 4. author important declarations
               6 5. user important declarations 
            */
            switch (s.Source)
            {
                case CssStylesheetSource.Agent:
                    if (s.IsImportant)
                        return 2;
                    return 1;
                case CssStylesheetSource.Author:
                    if (s.IsImportant)
                        return 5;
                    return 4;
                case CssStylesheetSource.User:
                    if (s.IsImportant)
                        return 6;
                    return 3;
            }
            throw new CssInvalidStateException();
        }
    }
}
