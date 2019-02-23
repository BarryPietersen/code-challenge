using System;
using System.Collections;
using System.Collections.Generic;

namespace HackerRank
{
    /*
        in this challenge, we are tasked with:
        - create a class that supports the basic behaviors of two stacks
        - both stacks share a single array for data storage
        - the class must implement methods to Push, Pop and Peek (both stacks)
        - the array should resize as required
    */
    public class ArrayStacks<T>
    {
        // the indexes of the peeks
        private int s1;
        private int s2;

        // the storage array
        private T[] arrStack;

        public int Stack1Count => s1 + 1;
        public int Stack2Count => (arrStack.Length - s2);
        private bool IsArrayFull => Stack1Count + Stack2Count == arrStack.Length;
        private bool IsArrayHalfFull => Stack1Count + Stack2Count < arrStack.Length / 2;

        public bool Stack1HasValue => Stack1Count > 0;
        public bool Stack2HasValue => Stack2Count > 0;

        public ArrayStacks(int capacity = 50)
        {
            s1 = -1;
            s2 = capacity;

            arrStack = new T[capacity];
        }

        public T Peek1() => Stack1HasValue ? arrStack[s1] : throw new Exception("Stack one is empty.");
        public T Peek2() => Stack2HasValue ? arrStack[s2] : throw new Exception("Stack two is empty.");

        private void IncreaseArrayCapacity()
        {
            T[] increasedArray = new T[arrStack.Length * 2];

            Array.Copy(arrStack, increasedArray, Stack1Count);
            Array.Copy(arrStack, s2, increasedArray, increasedArray.Length - Stack2Count, Stack2Count);

            s2 = increasedArray.Length - Stack2Count;
            arrStack = increasedArray;
        }

        private void DecreaseArrayCapacity()
        {
            T[] decreasedArray = new T[arrStack.Length / 2];

            Array.Copy(arrStack, decreasedArray, s1);
            Array.Copy(arrStack, s2, decreasedArray, decreasedArray.Length - Stack2Count, Stack2Count);

            s2 = decreasedArray.Length - Stack2Count;
            arrStack = decreasedArray;
        }

        public void Push1(T data)
        {
            if (!IsArrayFull) arrStack[++s1] = data;
            else
            {
                IncreaseArrayCapacity();
                arrStack[++s1] = data;
            }
        }

        public void Push2(T data)
        {
            if (!IsArrayFull) arrStack[--s2] = data;
            else
            {
                IncreaseArrayCapacity();
                arrStack[--s2] = data;
            }
        }

        public T Pop1()
        {
            if (Stack1HasValue)
            {
                T value = arrStack[s1--];

                if (IsArrayHalfFull) DecreaseArrayCapacity();

                return value;
            }
            else
                throw new Exception("Stack one is empty.");
        }

        public T Pop2()
        {
            if (Stack2HasValue)
            {
                T value = arrStack[s2++];

                if (IsArrayHalfFull) DecreaseArrayCapacity();

                return value;
            }
            else
                throw new Exception("Stack two is empty.");
        }

        public IEnumerable<T> Stack1Values()
        {
            for (int i = 0; i < Stack1Count; i++)
            {
                yield return arrStack[i];
            }
        }
    }
}
