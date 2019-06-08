using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedList
{
    public class LinkedListEnumerator : IEnumerator<int>
    {
        private LinkedListItem FirstItem { get; set; }
        private LinkedListItem CurrentItem { get; set; }

        public LinkedListEnumerator(LinkedListItem firstItem)
        {
            FirstItem = firstItem;
        }

        object IEnumerator.Current
        {
            get { return CurrentItem.Value; }
        }

        int IEnumerator<int>.Current
        {
            get { return CurrentItem.Value; }
        }
        
        public bool MoveNext()
        {
            if (FirstItem == null)
            {
                return false;
            }

            if (CurrentItem == null)
            {
                CurrentItem = FirstItem;
                return true;
            }

            CurrentItem = CurrentItem.NextItem;
            return CurrentItem != null;
        }
        
        public void Reset()
        {
            CurrentItem = null;
        }

        public void Dispose()
        {
        }
    }
}
