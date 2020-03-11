using System.Collections.Generic;

namespace Shapes.Comparers
{
    class PerimetersComparer : IComparer<IShape>
    {
        public int Compare(IShape s1, IShape s2)
        {
            if (s1.GetPerimeter().CompareTo(s2.GetPerimeter()) < 0)
            {
                return 1;
            }

            if (s1.GetPerimeter().CompareTo(s2.GetPerimeter()) > 0)
            {
                return -1;
            }

            return 0;
        }
    }
}
