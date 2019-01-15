using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.DataStructures.Stacks
{
    public static class Medium
    {
        // https://www.hackerrank.com/challenges/balanced-brackets/problem
        public static bool balancedBrackets(string s)
        {
            if (s[0] == ')' ||
                s[0] == '}' ||
                s[0] == ']') { return false; }

            Stack<char> openingBrackets = new Stack<char>();

            openingBrackets.Push(s[0]);

            for (int i = 1; i < s.Length; i++)
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
            int max = h[0];

            for (int i = 1; i < h.Length; i++)
            {
                int left = i;
                int right = i;

                while (left > 0 && h[left - 1] >= h[i]) { left--; }

                while (right < h.Length - 1 && h[right + 1] >= h[i]) { right++; }

                if (h[i] * (right - left + 1) > max) { max = h[i] * (right - left + 1); }
            }

            return max;
        }

        // https://www.hackerrank.com/challenges/equal-stacks/problem
        static int equalStacks(int[] h1, int[] h2, int[] h3)
        {
            //tops of stack
            int i = 0;
            int j = 0;
            int k = 0;

            //sum arrays, o(n)
            int s1 = h1.Sum();
            int s2 = h2.Sum();
            int s3 = h3.Sum();

            //continue to 'Pop()' from largest stack, o(n)
            while (i < h1.Length && j < h2.Length && k < h3.Length)
            {
                if (s1 > s2 && s1 >= s3) { s1 -= h1[i++]; }
                else if (s2 > s3 && s2 >= s1) { s2 -= h2[j++]; }
                else if (s3 > s1 && s3 >= s2) { s3 -= h3[k++]; }
                else { return s1; }
            }

            return 0;
        }


        // https://www.hackerrank.com/challenges/simple-text-editor/problem
        //============================================================================================
        public class TextEditor
        {
            private Stack<string> undos;

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
            foreach (string[] command in commands)
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
