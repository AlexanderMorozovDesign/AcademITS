using System.Collections.Generic;

namespace Shapes.Comparers
{
    class PerimetersComparer : IComparer<IShape>
    {
        public int Compare(IShape s1, IShape s2)
        {
            return s1.GetPerimeter().CompareTo(s2.GetPerimeter());
        }
    }
}
