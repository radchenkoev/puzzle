using System;

namespace LinkedList
{
    public class LinkedListItem
    {
        public int Value { get; set; }
        public LinkedListItem NextItem { get; set; }

        public LinkedListItem(int value)
        {
            Value = value;
        }
    }
}
