using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerRank.DataStructures.Medium
{
    public static class Stacks
    {
        // https://www.hackerrank.com/challenges/balanced-brackets/problem
        public static bool balancedBrackets(string s)
        {
            if (s[0] == ')' ||
                s[0] == '}' ||
                s[0] == ']') { return false; }

            var openingBrackets = new Stack<char>();

            openingBrackets.Push(s[0]);

            for (var i = 1; i < s.Length; i++)
            {
                if (s[i] == '(' ||
                    s[i] == '{' ||
                    s[i] == '[') { openingBrackets.Push(s[i]); }

                else if (openingBrackets.Count > 0 && (
                         (openingBrackets.Peek() == '(' && s[i] == ')') ||
                         (openingBrackets.Peek() == '{' && s[i] == '}') ||
                         (openingBrackets.Peek() == '[' && s[i] == ']'))
                         ) { openingBrackets.Pop(); }

                else return false;
            }

            return openingBrackets.Count == 0;
        }

        // https://www.hackerrank.com/challenges/largest-rectangle/problem
        public static long largestRectangle(int[] h)
        {
            var max = h[0];

            for (var i = 1; i < h.Length; i++)
            {
                var left = i;
                var right = i;

                while (left > 0 && h[left - 1] >= h[i]) { left--; }

                while (right < h.Length - 1 && h[right + 1] >= h[i]) { right++; }

                if (h[i] * (right - left + 1) > max) { max = h[i] * (right - left + 1); }
            }

            return max;
        }

        // https://www.hackerrank.com/challenges/equal-stacks/problem
        public static int equalStacks(int[] h1, int[] h2, int[] h3)
        {
            // tops of stack
            var i = 0;
            var j = 0;
            var k = 0;

            // sum arrays, o(n)
            var s1 = h1.Sum();
            var s2 = h2.Sum();
            var s3 = h3.Sum();

            // continue to 'Pop()' from largest stack, o(n)
            while (i < h1.Length && j < h2.Length && k < h3.Length)
            {
                if (s1 > s2 && s1 >= s3) { s1 -= h1[i++]; }
                else if (s2 > s3 && s2 >= s1) { s2 -= h2[j++]; }
                else if (s3 > s1 && s3 >= s2) { s3 -= h3[k++]; }
                else { return s1; }
            }

            return 0;
        }

        // https://www.hackerrank.com/challenges/jim-and-the-skyscrapers/problem
        //============================================================================================
        static long solve(int[] arr)
        {
            var total = 0L;
            var stk = new Stack<int>();

            for (int i = 0; i < arr.Length; i++)
            {
                if (stk.Count == 0) { stk.Push(arr[i]); continue; }

                if (arr[i] > stk.Peek())
                {
                    // at this point we calculate and remove all elements
                    // less than the current a[i] on the stack
                    while (stk.Count > 0 && arr[i] > stk.Peek())
                    {
                        total += CalculatePaths(stk);
                    }
                }

                stk.Push(arr[i]);
            }

            while (stk.Count > 0) total += CalculatePaths(stk);

            return total;
        }

        private static long CalculatePaths(Stack<int> stk)
        {
            if (stk.Count == 0) return 0;

            var reps = 0L;
            var count = 0L;
            var temp = stk.Pop();

            while (stk.Count > 0 && temp == stk.Peek())
            {
                reps++;
                count += reps;
                temp = stk.Pop();
            }

            return count * 2;
        }
        //============================================================================================

        // https://www.hackerrank.com/challenges/simple-text-editor/problem
        //============================================================================================
        public class TextEditor
        {
            private readonly Stack<string> undos;

            private string text;
            public string Text
            {
                get => text;
                set
                {
                    undos.Push(text);
                    text = value;
                }
            }

            public TextEditor()
            {
                undos = new Stack<string>();
                text = "";
            }

            public void AppendText(string text) { Text += text; }

            public void DeleteText(int num)
            {
                if (num >= Text.Length) { Text = ""; }
                else { Text = Text.Substring(0, Text.Length - num); }
            }

            public char Print(int num)
            {
                if (num < 1 || num > Text.Length)
                { throw new Exception("the argument supplied was not valid for the length of the string"); }
                else return Text[num - 1];
            }

            public void Undo() { text = undos.Pop(); }
        }

        public static void ExecuteTextEditorCommands(string[][] commands, TextEditor textEditor)
        {
            foreach (var command in commands)
            {
                switch (command[0])
                {
                    case "1":
                        //append
                        textEditor.AppendText(command[1]);
                        break;
                    case "2":
                        //delete
                        textEditor.DeleteText(int.Parse(command[1]));
                        break;
                    case "3":
                        //print 
                        Console.WriteLine(textEditor.Print(int.Parse(command[1])));
                        break;
                    case "4":
                        //undo
                        textEditor.Undo();
                        break;
                    default:
                        //the command argument was not valid
                        throw new Exception("the command argument was not valid");
                }
            }
        }
        //============================================================================================
    }
}
