#region License
/*
Distributed under the terms of a MIT-style license:

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
using Marius.Html.Css.Values;
using System.Runtime.CompilerServices;
using Marius.Html.Dom;
using System.Diagnostics;
using Marius.Html.Css.Box.Debug;
using System.ComponentModel;

namespace Marius.Html.Css.Box
{
    public class CssBox: ITreeNode<CssBox>
    {
        private CssContext _context;
        private CssBox _inheritanceParent;
        private LinkedList<CssBox> _inheritedChildren;

        private CssPropertyValueDictionary _properties;
        private CssPropertyValueDictionary _firstLineProperties;
        private CssPropertyValueDictionary _firstLetterProperties;

        public CssBox(CssContext context)
        {
            _context = context;

            _inheritedChildren = new LinkedList<CssBox>();

            _properties = new CssPropertyValueDictionary(this);
            _firstLineProperties = new CssPropertyValueDictionary(this);
            _firstLetterProperties = new CssPropertyValueDictionary(this);
        }

        public CssContext Context { get { return _context; } }

        public CssBox Parent { get; private set; }
        public CssBox InheritanceParent
        {
            get
            {
                return _inheritanceParent;
            }
            set
            {
                if (_inheritanceParent != null)
                    _inheritanceParent._inheritedChildren.Remove(this);

                _inheritanceParent = value;
                if (_inheritanceParent != null)
                    _inheritanceParent._inheritedChildren.AddLast(this);
            }
        }

        public CssBox FirstChild { get; private set; }
        public CssBox LastChild { get; private set; }
        public CssBox NextSibling { get; private set; }
        public CssBox PreviousSibling { get; private set; }

        public CssPropertyValueDictionary Properties { get { return _properties; } }
        public CssPropertyValueDictionary FirstLineProperties { get { return _firstLineProperties; } }
        public CssPropertyValueDictionary FirstLetterProperties { get { return _firstLetterProperties; } }

        public bool IsRunIn { get; set; }

        public virtual void Append(CssBox child)
        {
            if (child.Parent != null)
                child.Parent.Remove(child);

            child.Parent = this;
            if (FirstChild == null)
            {
                FirstChild = LastChild = child;
            }
            else
            {
                child.PreviousSibling = LastChild;
                LastChild.NextSibling = child;
                LastChild = child;
            }
        }

        public virtual void Insert(CssBox child)
        {
            if (child.Parent != null)
                child.Parent.Remove(child);

            child.Parent = this;
            if (FirstChild == null)
            {
                FirstChild = LastChild = child;
            }
            else
            {
                child.NextSibling = FirstChild;
                FirstChild.PreviousSibling = child;
                FirstChild = child;
            }
        }

        public virtual void Remove(CssBox child)
        {
            if (child.Parent != this)
                throw new CssInvalidStateException();

            if (child == FirstChild)
            {
                if (child == LastChild)
                {
                    FirstChild = LastChild = null;
                }
                else
                {
                    FirstChild = FirstChild.NextSibling;
                    FirstChild.PreviousSibling = null;
                }
            }
            else if (child == LastChild)
            {
                // cannot be the only one
                LastChild = LastChild.PreviousSibling;
                LastChild.NextSibling = null;
            }
            else
            {
                var prev = child.PreviousSibling;
                var next = child.NextSibling;

                prev.NextSibling = next;
                next.PreviousSibling = prev;
            }

            child.Parent = null;
            child.PreviousSibling = child.NextSibling = null;
        }

        public virtual void Wrap(CssBox start, CssBox end, CssBox within)
        {
            if (start.Parent != this || end.Parent != this)
                throw new CssInvalidStateException();

            this.InsertBefore(within, start);

            var finish = end.NextSibling;
            var current = start;
            while (current != finish)
            {
                var next = current.NextSibling;
                within.Append(current);
                current = next;
            }
        }

        public virtual void Replace(CssBox box, CssBox with)
        {
            if (box.Parent != this)
                throw new CssInvalidStateException();

            if (box == with)
                return;

            if (with.Parent != null)
                with.Parent.Remove(with);

            with.Parent = this;

            var prev = box.PreviousSibling;
            var next = box.NextSibling;

            if (prev != null)
                prev.NextSibling = with;

            if (next != null)
                next.PreviousSibling = with;

            with.PreviousSibling = prev;
            with.NextSibling = next;

            if (FirstChild == box)
                FirstChild = with;
            if (LastChild == box)
                LastChild = with;

            box.NextSibling = box.PreviousSibling = null;
        }

        public virtual void InsertBefore(CssBox newChild, CssBox refChild)
        {
            if (refChild.Parent != this)
                throw new CssInvalidStateException();

            if (newChild == refChild)
                return;

            if (newChild.Parent != null)
                newChild.Parent.Remove(newChild);

            newChild.Parent = this;

            var prev = refChild.PreviousSibling;
            refChild.PreviousSibling = newChild;

            newChild.NextSibling = refChild;
            newChild.PreviousSibling = prev;

            if (prev != null)
                prev.NextSibling = newChild;

            if (refChild == FirstChild)
                FirstChild = newChild;
        }

        public virtual void InsertAfter(CssBox newChild, CssBox refChild)
        {
            if (refChild.Parent != this)
                throw new CssInvalidStateException();

            if (newChild == refChild)
                return;

            if (newChild.Parent != null)
                newChild.Parent.Remove(newChild);

            newChild.Parent = this;

            var next = refChild.NextSibling;
            refChild.NextSibling = newChild;

            newChild.PreviousSibling = refChild;
            newChild.NextSibling = next;

            if (next != null)
                next.PreviousSibling = newChild;

            if (refChild == LastChild)
                LastChild = newChild;
        }

        public static void Split(ref CssBox parent, CssBox beforeChild)
        {
            if (parent.Parent == null)
                throw new CssInvalidStateException();

            if (beforeChild.Parent != parent)
                throw new CssInvalidStateException();

            var grand = parent.Parent;
            CssSplitBox left, right;

            if (parent is CssSplitBox)
            {
                CssSplitBox split = (CssSplitBox)parent;
                if (split.Part == CssSplitPart.Start)
                {
                    left = new CssSplitBox(parent, CssSplitPart.Start);
                    right = new CssSplitBox(parent, CssSplitPart.Middle);
                }
                else if (split.Part == CssSplitPart.End)
                {
                    left = new CssSplitBox(parent, CssSplitPart.Middle);
                    right = new CssSplitBox(parent, CssSplitPart.End);
                }
                else
                {
                    left = new CssSplitBox(parent, CssSplitPart.Middle);
                    right = new CssSplitBox(parent, CssSplitPart.Middle);
                }
            }
            else
            {
                left = new CssSplitBox(parent, CssSplitPart.Start);
                right = new CssSplitBox(parent, CssSplitPart.End);
            }

            grand.Replace(parent, left);
            grand.InsertAfter(right, left);

            var current = parent.FirstChild;
            while (current != beforeChild)
            {
                var next = current.NextSibling;
                left.Append(current);
                if (current.InheritanceParent == parent)
                    current.InheritanceParent = left;
                current = next;
            }

            while (current != null)
            {
                var next = current.NextSibling;
                right.Append(current);
                if (current.InheritanceParent == parent)
                    current.InheritanceParent = right;
                current = next;
            }

            var inherited = parent._inheritedChildren.ToArray();
            for (int i = 0; i < inherited.Length; i++)
            {
                inherited[i].InheritanceParent = left;
            }

            parent = left;
        }

        [Conditional("DEBUG")]
        public virtual void Debug()
        {
            BoxDebugDisplay debug = new BoxDebugDisplay();
            debug.Box = this;
            debug.ShowDialog();
        }
    }
}
