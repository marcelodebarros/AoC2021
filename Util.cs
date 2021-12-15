using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
    public class PriorityQueue
    {
        public struct HeapEntry
        {
            private object item;
            private double priority;
            public HeapEntry(object item, double priority)
            {
                this.item = item;
                this.priority = priority;
            }
            public object Item
            {
                get
                {
                    return item;
                }
            }
            public double Priority
            {
                get
                {
                    return priority;
                }
            }
        }

        private bool ascend;
        private int count;
        private int capacity;
        private HeapEntry[] heap;

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public PriorityQueue(bool ascend)
        {
            capacity = 10000000;
            heap = new HeapEntry[capacity];
            this.ascend = ascend;
        }

        public object Dequeue(out double priority)
        {
            priority = heap[0].Priority;
            object result = heap[0].Item;
            count--;
            trickleDown(0, heap[count]);
            return result;
        }

        public object Peak(/*out double priority*/)
        {
            //priority = heap[0].Priority;
            object result = heap[0].Item;
            return result;
        }

        public void Enqueue(object item, double priority)
        {
            count++;
            bubbleUp(count - 1, new HeapEntry(item, priority));
        }

        private void bubbleUp(int index, HeapEntry he)
        {
            int parent = (index - 1) / 2;
            // note: (index > 0) means there is a parent
            if (this.ascend)
            {
                while ((index > 0) && (heap[parent].Priority > he.Priority))
                {
                    heap[index] = heap[parent];
                    index = parent;
                    parent = (index - 1) / 2;
                }
                heap[index] = he;
            }
            else
            {
                while ((index > 0) && (heap[parent].Priority < he.Priority))
                {
                    heap[index] = heap[parent];
                    index = parent;
                    parent = (index - 1) / 2;
                }
                heap[index] = he;
            }
        }

        private void trickleDown(int index, HeapEntry he)
        {
            int child = (index * 2) + 1;
            while (child < count)
            {
                if (this.ascend)
                {
                    if (((child + 1) < count) &&
                        (heap[child].Priority > heap[child + 1].Priority))
                    {
                        child++;
                    }
                }
                else
                {
                    if (((child + 1) < count) &&
                        (heap[child].Priority < heap[child + 1].Priority))
                    {
                        child++;
                    }
                }
                heap[index] = heap[child];
                index = child;
                child = (index * 2) + 1;
            }
            bubbleUp(index, he);
        }
    }
}
