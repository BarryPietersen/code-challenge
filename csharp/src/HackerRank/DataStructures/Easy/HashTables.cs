using System;
using System.Collections.Generic;

namespace HackerRank.DataStructures.Easy
{
    public static class HashTables
    {
        // https://www.hackerrank.com/challenges/ctci-ransom-note/problem
        static void checkMagazine(string[] magazine, string[] note)
        {
            var words = new Dictionary<string, int>();

            foreach (var word in note)
            {
                if (!words.ContainsKey(word)) words.Add(word, 1);
                else words[word]++;
            }

            foreach (var word in magazine)
            {
                if (words.ContainsKey(word))
                {
                    if (words[word] > 1) words[word]--;
                    else words.Remove(word);
                }
            }

            Console.WriteLine(words.Count == 0 ? "Yes" : "No");
        }
    }
}
