using System;
using System.Collections.Generic;
using System.Linq;

using LinkedList.Extensions;

namespace LinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList list = new LinkedList();

            //Handlers
            list.OnItemIsAdded += delegate(object sender, int item)
                { Console.WriteLine("A new item {0} is added to LinkedList", item); };
            list.OnItemIsRemoved += delegate(object sender, int item)
                { Console.WriteLine("Item {0} is removed from LinkedList", item); };

            //Add
            LinkedList_Add_Test_01(list);

            //AddRange
            LinkedList_AddRange_Test_01(list);

            //Reverse
            LinkedList_Reverse_Test_01(list);

            //Sort
            LinkedList_Sort_Test_01(list);

            //Count
            Console.WriteLine("Count: {0}{1}", list.Count, Environment.NewLine);

            //Index
            var index = 7;
            Console.WriteLine("Item with index {0} is {1}", index, list[index]);

            var newValue = 111;
            list[7] = newValue;
            Console.WriteLine("Item with index {0} is changed to {1}", index, newValue);
            Console.WriteLine("Item with index {0} is {1}", index, list[index]);
            PrintLinkedList(list);

            //IndexOf
            LinkedList_IndexOf_Test_01(list);

            //Contains
            LinkedList_Contains_Test_01(list);

            //IEnumerable, Where, ForEach...
            var selectedList = list.Where(x => x > 20);
            Console.WriteLine("IEnumerable, Where, ForEach...");
            Console.Write("SelectedList (where item > 20): ");
            selectedList.ForEach(item => Console.Write("{0} ", item));
            Console.WriteLine("{0}", Environment.NewLine);

            //Remove
            LinkedList_Remove_Test_01(list, 100);
            LinkedList_Remove_Test_01(list, list.First());
            LinkedList_Remove_Test_01(list, list
                    .GroupBy(i => i)
                    .Select(g => new { Item = g.Key, Count = g.Count() })
                    .Where(v => v.Count > 1)
                    .Select(v => v.Item)
                    .First());
            LinkedList_Remove_Test_01(list, list.Last());

            //Clear
            LinkedList_Clear_Test_01(list);

            Console.ReadKey();
        }

        public static void LinkedList_Add_Test_01(LinkedList list)
        {
            Console.WriteLine("LinkedList_Add_Test_01:");
            list.Add(22);
            list.Add(18);
            list.Add(35);
            list.Add(7);
            list.Add(5);
            list.Add(44);
            list.Add(67);
            list.Add(55);
            PrintLinkedList(list);
        }

        public static void LinkedList_AddRange_Test_01(LinkedList list)
        {
            Console.WriteLine("LinkedList_AddRange_Test_01:");
            list.AddRange(new List<int> { 27, 9, 22, 61 });
            PrintLinkedList(list);
        }

        public static void LinkedList_Clear_Test_01(LinkedList list)
        {
            Console.WriteLine("LinkedList_Clear_Test_01:");
            list.Clear();
            PrintLinkedList(list);
        }

        public static void LinkedList_Contains_Test_01(LinkedList list)
        {
            Console.WriteLine("LinkedList_Contains_Test_01:");
            var item = 9;
            Console.WriteLine("LinkedList contains {0}: {1}", item, list.Contains(item));
            item = 100;
            Console.WriteLine("LinkedList contains {0}: {1}{2}", item, list.Contains(item), Environment.NewLine);
        }

        public static void LinkedList_IndexOf_Test_01(LinkedList list)
        {
            Console.WriteLine("LinkedList_IndexOf_Test_01:");
            var item = 9;
            Console.WriteLine("Index of {0} is {1}", item, list.IndexOf(item));
            item = 100;
            Console.WriteLine("Index of {0} is {1}{2}", item, list.IndexOf(item), Environment.NewLine);
        }

        public static void LinkedList_Remove_Test_01(LinkedList list, int itemToRemove)
        {
            Console.WriteLine("LinkedList_Remove_Test_01:");
            Console.WriteLine("Removed item: {0}", itemToRemove);
            var result = list.Remove(itemToRemove);
            Console.WriteLine("Result: {0}", result);
            PrintLinkedList(list);
        }

        public static void LinkedList_Reverse_Test_01(LinkedList list)
        {
            Console.WriteLine("LinkedList_Reverse_Test_01:");
            list.Reverse();
            PrintLinkedList(list);
        }

        public static void LinkedList_Sort_Test_01(LinkedList list)
        {
            Console.WriteLine("LinkedList_Sort_Test_01:");
            list.Sort();
            PrintLinkedList(list);
        }

        public static void PrintLinkedList(LinkedList list)
        {
            Console.Write("List: ");
            foreach (var item in list)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
