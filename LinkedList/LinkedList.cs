using System;
using System.Collections;
using System.Collections.Generic;

using LinkedList.Exceptions;

namespace LinkedList
{
    public delegate void LinkedListItemHandler(object sender, int item);

    public class LinkedList : IEnumerable<int>
    {
        private LinkedListItem FirstItem { get; set; }

        public event LinkedListItemHandler OnItemIsAdded;
        public event LinkedListItemHandler OnItemIsRemoved;

        /// <summary>
        /// Gets the number of items contained in the LinkedList.
        /// </summary>
        public int Count
        {
            get
            {
                var item = FirstItem;

                if (item == null)
                {
                    return 0;
                }

                var count = 1;
                while (item.NextItem != null)
                {
                    item = item.NextItem;
                    count++;
                }
                return count;
            }
        }

        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <param name="index">
        /// The zero-based index of the item to get or set.
        /// </param>
        /// <returns>
        /// The item at the specified index.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        /// index is less than 0.
        /// -or-
        /// index is equal to or greater than Count. 
        /// </exception>
        public int this[int index]
        {
            get
            {
                var item = GetItemByIndex(FirstItem, index);
                return item.Value;
            }
            set
            {
                var item = GetItemByIndex(FirstItem, index);
                item.Value = value;
            }
        }

        #region IEnumerable methods

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new LinkedListEnumerator(FirstItem);
        }

        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            return new LinkedListEnumerator(FirstItem);
        }

        #endregion IEnumerable methods

        /// <summary>
        /// Adds an item to the end of the LinkedList.
        /// </summary>
        /// <param name="item">
        /// The item to be added to the end of the LinkedList.
        /// </param>
        public void Add(int item)
        {
            var lastItem = GetLastItem(FirstItem);

            if (lastItem == null)
            {
                FirstItem = new LinkedListItem(item);
            }
            else
            {
                lastItem.NextItem = new LinkedListItem(item);
            }

            if (OnItemIsAdded != null)
            {
                OnItemIsAdded.Invoke(this, item);
            }
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the LinkedList.
        /// </summary>
        /// <param name="collection">
        /// The collection whose elements should be added to the end of the LinkedList.
        /// The collection itself cannot be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The collection is null.
        /// </exception>
        public void AddRange(IEnumerable<int> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }

            var lastItem = GetLastItem(FirstItem);

            foreach (var item in collection)
            {
                if (lastItem == null)
                {
                    FirstItem = new LinkedListItem(item);
                    lastItem = FirstItem;
                }
                else
                {
                    lastItem.NextItem = new LinkedListItem(item);
                    lastItem = lastItem.NextItem;
                }

                if (OnItemIsAdded != null)
                {
                    OnItemIsAdded.Invoke(this, item);
                }
            }
        }

        /// <summary>
        /// Searches for the specified item and returns the zero-based
        /// index of the first occurrence within the entire LinkedList.
        /// </summary>
        /// <param name="item">The item to locate in the LinkedList.</param>
        /// <returns>
        /// The zero-based index of the first occurrence of item
        /// within the entire LinkedList, if found; otherwise, –1.
        /// </returns>
        public int IndexOf(int item)
        {
            var index = 0;
            foreach (var i in this)
            {
                if (i.CompareTo(item) == 0)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        /// <summary>
        /// Removes all elements from the LinkedList.
        /// </summary>
        public void Clear()
        {
            foreach (var item in this)
            {
                if (OnItemIsRemoved != null)
                {
                    OnItemIsRemoved.Invoke(this, item);
                }
            }
            FirstItem = null;
        }

        /// <summary>
        /// Determines whether an item is in the LinkedList.
        /// </summary>
        /// <param name="value">The item to locate in the LinkedList.</param>
        /// <returns>
        /// true if item is found in the LinkedList; otherwise, false.
        /// </returns>
        public bool Contains(int item)
        {
            foreach (var i in this)
            {
                if (i.CompareTo(item) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Removes the first occurrence of a specific item from the LinkedList.
        /// </summary>
        /// <param name="item">The item to remove from the LinkedList.</param>
        /// <returns>
        /// true if item is successfully removed; otherwise, false.
        /// This method also returns false if item was not found in the LinkedList.
        /// </returns>
        public bool Remove(int item)
        {
            try
            {
                var prevItem = GetPrevItem(FirstItem, item);

                if (prevItem == null)
                {
                    var temp = FirstItem.NextItem;
                    FirstItem.NextItem = null;
                    FirstItem = temp;
                }
                else
                {
                    var temp = prevItem.NextItem;
                    prevItem.NextItem = prevItem.NextItem.NextItem;
                    temp.NextItem = null;
                }

                if (OnItemIsRemoved != null)
                {
                    OnItemIsRemoved.Invoke(this, item);
                }

                return true;
            }
            catch (PrevItemNotFoundException ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Reverses the order of the items in the entire LinkedList.
        /// </summary>
        public void Reverse()
        {
            FirstItem = Reverse(FirstItem, null);
        }

        /// <summary>
        /// Sorts the items in the entire LinkedList using the default comparer.
        /// </summary>
        public void Sort()
        {
            if (FirstItem == null)
            {
                return;
            }

            var currentItem = FirstItem;
            LinkedListItem prevCurrentItem = null;

            while (currentItem.NextItem != null)
            {
                var minOrMaxItem = currentItem.NextItem;
                var prevMinOrMaxItem = currentItem;

                var item = minOrMaxItem;
                var prevItem = prevMinOrMaxItem;

                while (item != null)
                {
                    if (minOrMaxItem.Value.CompareTo(item.Value) > 0)
                    {
                        minOrMaxItem = item;
                        prevMinOrMaxItem = prevItem;
                    }

                    prevItem = item;
                    item = item.NextItem;
                }

                if (currentItem.Value.CompareTo(minOrMaxItem.Value) > 0)
                {
                    if (FirstItem == currentItem)
                    {
                        FirstItem = minOrMaxItem;
                    }

                    var temp = currentItem.NextItem;
                    currentItem.NextItem = minOrMaxItem.NextItem;
                    minOrMaxItem.NextItem = currentItem == prevMinOrMaxItem
                        ? currentItem
                        : temp;

                    if (prevMinOrMaxItem != currentItem)
                    {
                        prevMinOrMaxItem.NextItem = currentItem;
                    }

                    if (prevCurrentItem != null)
                    {
                        prevCurrentItem.NextItem = minOrMaxItem;
                    }

                    currentItem = minOrMaxItem;
                }

                prevCurrentItem = currentItem;
                currentItem = currentItem.NextItem;
            }
        }

        #region Protected static methods

        /// <summary>
        /// Looks for LinkedListItem in LinkedList at specified index.
        /// </summary>
        /// <param name="item">FirstLinkedListItem.</param>
        /// <param name="index">The zero-based index.</param>
        /// <returns>
        /// The LinkedListItem at the specified index.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        /// index is less than 0.
        /// -or-
        /// index is equal to or greater than Count. 
        /// </exception>
        protected static LinkedListItem GetItemByIndex(LinkedListItem item, int index)
        {
            if (item == null || index < 0)
                throw new IndexOutOfRangeException();
            if (index == 0)
                return item;
            return GetItemByIndex(item.NextItem, index - 1);
        }

        /// <summary>
        /// Looks for the last LinkedListItem in LinkedList.
        /// </summary>
        /// <param name="item">FirstLinkedListItem.</param>
        /// <returns>The last LinkedListItem.</returns>
        protected static LinkedListItem GetLastItem(LinkedListItem item)
        {
            if (item == null)
                return null;
            if (item.NextItem == null)
                return item;
            return GetLastItem(item.NextItem);
        }

        /// <summary>
        /// Looks for a previous item of specified item;
        /// </summary>
        /// <param name="firstItem">FirstLinkedListItem.</param>
        /// <param name="value">Specified LinkedListItem value.</param>
        /// <returns>
        /// null if value is equals to FirstItem.Value; otherwise, Previous LinkedListItem.
        /// </returns>
        /// <exception cref="PrevItemNotFoundException">
        /// There is not any LinkedListItem with the value that equals to specified value.
        /// </exception>
        public static LinkedListItem GetPrevItem(LinkedListItem firstItem, int value)
        {
            if (firstItem.Value.CompareTo(value) == 0)
            {
                return null;
            }

            var item = firstItem;
            while (item != null)
            {
                if (item.NextItem != null
                    && item.NextItem.Value.CompareTo(value) == 0)
                {
                    return item;
                }
                item = item.NextItem;
            }

            throw new PrevItemNotFoundException();
        }

        /// <summary>
        /// Reverses the order of the LinkedListItems in the entire LinkedList.
        /// </summary>
        /// <param name="currentItem">The current LinkedListItem.</param>
        /// <param name="prevItem">The previous LinkedListItem.</param>
        /// <returns>
        /// The last LinkedList item that has been maked the first in reversed LinkedList.
        /// </returns>
        protected static LinkedListItem Reverse(LinkedListItem currentItem, LinkedListItem prevItem)
        {
            if (currentItem == null)
            {
                return prevItem;
            }
            var firstItem = Reverse(currentItem.NextItem, currentItem);
            currentItem.NextItem = prevItem;
            return firstItem;
        }

        #endregion Protected static methods
    }
}