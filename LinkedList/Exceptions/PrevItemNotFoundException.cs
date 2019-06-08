using System;

namespace LinkedList.Exceptions
{
    public class PrevItemNotFoundException : Exception
    {
        public override string Message
        {
            get
            {
                return "LinkedList does not contain desired value";
            }
        }
    }
}
