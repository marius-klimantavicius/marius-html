using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marius.Html.Css.Box;
using Marius.Html.Css.Values;

namespace Marius.Html.Css.Layout.BoxGeneration
{
    public class CssRunInBoxesStep: IGeneratorStep
    {
        private Dictionary<CssBox, bool> _hasBlock;

        public void Execute(CssContext context, CssBox box)
        {
            _hasBlock = new Dictionary<CssBox, bool>();

            RunInBoxes(box);

            _hasBlock = null;
        }

        private void RunInBoxes(CssBox box)
        {
            CssBox current = box.FirstChild;
            while (current != null)
            {
                RunInBoxes(current);
                current = current.NextSibling;
            }

            current = box.FirstChild;
            while (current != null)
            {
                CssBox next = current.NextSibling;

                var display = current.Computed.Display;
                if (display.Equals(CssKeywords.RunIn))
                {
                    bool rfixed = false;

                    //1. If the run-in box contains a block box, the run-in box becomes a block box.
                    if (ContainsBlockBox(current))
                    {
                        current.Properties.Display = CssKeywords.Block;
                        rfixed = true;
                    }

                    //2. If a sibling block box (that does not float and is not absolutely positioned) follows the run-in box, the run-in box becomes the first inline box of the block box. A run-in cannot run in to a block that already starts with a run-in or that itself is a run-in.
                    if (!rfixed && next != null && CssUtils.IsBlock(next))
                    {
                        var nfloat = next.Computed.Float;
                        var npos = next.Computed.Position;

                        if (nfloat.Equals(CssKeywords.None)
                            && !(npos.Equals(CssKeywords.Absolute) || npos.Equals(CssKeywords.Fixed)))
                        {
                            if (next.FirstChild == null || (!next.FirstChild.IsRunIn))
                            {
                                current.Properties.Display = CssKeywords.Inline;
                                current.IsRunIn = true;
                                current.InheritanceParent = current.Parent;

                                next.Insert(current);
                                rfixed = true;
                            }
                        }
                    }

                    //3. Otherwise, the run-in box becomes a block box.
                    if (!rfixed)
                    {
                        current.Properties.Display = CssKeywords.Block;
                        rfixed = true;
                    }
                }

                current = next;
            }
        }

        private bool ContainsBlockBox(CssBox box)
        {
            if (_hasBlock.ContainsKey(box))
                return _hasBlock[box];

            CssBox current = box.FirstChild;
            while (current != null)
            {
                if (CssUtils.IsBlock(current))
                {
                    _hasBlock.Add(box, true);
                    return true;
                }

                current = current.NextSibling;
            }

            current = box.FirstChild;
            while (current != null)
            {
                if (ContainsBlockBox(current))
                {
                    _hasBlock.Add(box, true);
                    return true;
                }
                current = current.NextSibling;
            }

            return false;
        }
    }
}
