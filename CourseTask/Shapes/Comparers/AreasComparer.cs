using System.Collections.Generic;

namespace Shapes.Comparers
{
    class AreasComparer : IComparer<IShape>
    {
        public int Compare(IShape s1, IShape s2)
        {
            return s1.GetArea().CompareTo(s2.GetArea());
        }
    }
}
