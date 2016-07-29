﻿using System;
using System.Collections.Generic;

namespace Tetris
{
    public static class Extensions
    {
        public static IEnumerable<IEnumerable<int>> GroupConsecutive(this IEnumerable<int> list)
        {
            var group = new List<int>();
            foreach (var i in list)
            {
                if (group.Count == 0 || Math.Abs(i - group[group.Count - 1]) <= 1)
                    group.Add(i);
                else
                {
                    yield return group;
                    group = new List<int> { i };
                }
            }
            yield return group;
        }
    }
}
