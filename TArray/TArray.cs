using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TArray
{
    class TArray : System.Collections.IEnumerable
    {
        public delegate bool CompareOp(Object el1,Object el2);

        private int size;
        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        private int reservedSize;
        private Element firstElement;
        private Element lastElement;
        public TArray()
        {
            size = 0;
            reservedSize = 0;
            firstElement = null;
            lastElement = null;
        }

        public TArray(int reserved):this()
        {            
            for (int i = 0; i < reserved; ++i)
                Insert(null);
            size = 0;
            this.reservedSize = reserved;
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            Element currentElement = firstElement;
            while(currentElement != null)
            {
                yield return currentElement.Data;
                currentElement = currentElement.Next;
            }
        }

        public void Insert(Object data)
        {            
            Element element;
            if (size >= reservedSize)
                element = new Element(data);
            else
            {
                element = GetElement(size);
                element.Data = data;
            }

            
            if (size == 0)
            {
                firstElement = element;
                lastElement = element;
            }
            else
            {
                element.Previous = lastElement;
                lastElement.Next = element;
                lastElement = element;
            }
            ++size;
        }

        public void Insert(int position, Object data)
        {
            if (position == size)
                Insert(data);
            else
            {
                Element beforeElement = GetElement(position);
                if (beforeElement != null)
                {
                    Element element;
                    if (size >= reservedSize)
                        element = new Element(data);
                    else
                    {
                        element = GetElement(reservedSize - 1);
                        element.Previous.Next = null;
                        element.Data = data;
                    }
                    element.Previous = beforeElement.Previous;
                    element.Next = beforeElement;
                    beforeElement.Previous = element;
                    if (position == 0)
                        firstElement = element;
                    ++size;
                }
            }
        }

        public void Clear()
        {
            firstElement = null;
            lastElement = null;
            size = 0;
        }

        public void RemoveAt(int index)
        {
            Element removeElement = GetElement(index);
            if (removeElement != null)
            {
                if(removeElement.Previous != null)
                    removeElement.Previous.Next = removeElement.Next;
                if(removeElement.Next != null)
                    removeElement.Next.Previous = removeElement.Previous;
                if (index == 0)
                    firstElement = (removeElement.Next == null) ? null : removeElement.Next;
                if (index == size - 1)
                    lastElement = (removeElement.Previous == null) ? null : removeElement.Previous;
                --size;
                if (size < reservedSize)
                {
                    Element last = GetElement(reservedSize - 2);
                    if (last != null)
                    {
                        removeElement.Data = null;
                        removeElement.Next = null;
                        last.Next = removeElement;
                        removeElement.Previous = last;
                    }
                }
            }
        }

        public int Find(Object data)
        {
            int result = -1;

            Element currentElement = firstElement;
            int position = 0;
            while (currentElement != null)
            {
                if (currentElement.Data==data)
                {
                    result = position;
                    break;
                }
                ++position;
                currentElement = currentElement.Next;
            }

            return result;
        }

        public void CopyTo(TArray toArray,int length)
        {
            toArray.Clear();
            Element currElement = firstElement;
            int count = (length <= this.size) ? length : size;
            for (int i = 0; i < count; ++i)
            {
                toArray.Insert(currElement.Data);
                currElement = currElement.Next;
            }
        }

        public void Sort(CompareOp Compare)
        {
            Element currElement;
            for (int i = 0; i < size - 1; ++i)
            {
                currElement = firstElement;
                for (int j = 0; j < size - 1 - i; ++j)
                {
                    if (Compare(currElement.Data,currElement.Next.Data))                   
                        Swap(currElement, currElement.Next);
                    currElement = currElement.Next;
                }
            }
        }

        private void Swap(Element el1,Element el2)
        {
            Object temp = el1.Data;
            el1.Data = el2.Data;
            el2.Data = temp;
        }


        private Element GetElement(int index)
        {
            Element result = null;

            if ((index < size || index < reservedSize) && index > -1)
            {
                result = firstElement;
                for (int i = 0; i < index; ++i)
                    result = result.Next;
            }

            return result;
        }

    }
}
