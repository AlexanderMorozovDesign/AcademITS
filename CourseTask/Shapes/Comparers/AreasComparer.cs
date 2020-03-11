using System.Collections.Generic;

namespace Shapes.Comparers
{
    class AreasComparer : IComparer<IShape>
    {
        public int Compare(IShape s1, IShape s2)
        {
            if (s1.GetArea().CompareTo(s2.GetArea()) < 0)
            {
                return 1;
            }

            if (s1.GetArea().CompareTo(s2.GetArea()) > 0)
            {
                return -1;
            }

            return 0;
        }
    }
}
