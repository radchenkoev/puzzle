using System;
using System.Collections.Generic;

namespace LinkedList.Extensions
{
    public static class LinkedListExtensions
    {
        public static void ForEach(this IEnumerable<int> source, Action<int> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}
