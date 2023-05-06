using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackerRank.Algorithms.Easy
{
    public static class Strings
    {
        // https://www.hackerrank.com/challenges/reduced-string/problem
        public static string super_reduced_string(string s)
        {
            var reduced = false;

            while (!reduced)
            {
                reduced = true;

                for (var i = 1; i < s.Length - 1; i++)
                {
                    if (s[i] == s[i - 1])
                    {
                        s = s.Remove(i - 1, 2);
                        reduced = false;
                        i--;
                    }
                    else if (s[i] == s[i + 1])
                    {
                        s = s.Remove(i, 2);
                        reduced = false;
                    }
                }
            }

            if (s.Length == 2 && s[0] == s[1]) s = "";

            return s.Length > 0 ? s : "Empty String";
        }

        // https://www.hackerrank.com/challenges/camelcase/problem
        public static int camelcase(string s)
        {
            return s.Where(ch => char.IsUpper(ch)).Count() + 1;
        }

        // https://www.hackerrank.com/challenges/strong-password/problem
        public static int minimumNumber(int n, string password)
        {
            var min = 0;

            if (!password.Any(c => char.IsLower(c))) { min++; }
            if (!password.Any(c => char.IsUpper(c))) { min++; }
            if (!password.Any(c => char.IsDigit(c))) { min++; }
            if (password.IndexOfAny("!@#$%^&*()-+".ToCharArray()) < 0) { min++; }

            return min > (6 - n) ? min : (6 - n);
        }

        // https://www.hackerrank.com/challenges/mars-exploration/problem
        public static int marsExploration(string s)
        {
            //1 2 3 4 5 6 7 8 9 10 11 12
            //S O S S O S S O S S  O  S
            var sos = "SOS";
            var count = 0;

            for (var i = 0; i < s.Length; i++)
            {
                if (s[i] != sos[i % 3]) count++;
            }

            return count;
        }

        // hackerrank
        // i        ^
        // hereiamstackerrank
        // j                ^
        // https://www.hackerrank.com/challenges/hackerrank-in-a-string/problem
        public static string hackerrankInString(string s)
        {
            var h = "hackerrank";
            var i = 0;
            var j = 0;

            while (i < h.Length && j < s.Length)
            {
                if (h[i] != s[j]) j++;
                else { i++; j++; }
            }

            return i == h.Length ? "YES" : "NO";
        }

        // https://www.hackerrank.com/challenges/pangrams/problem
        public static string pangrams(string s)
        {
            var chs = new HashSet<char>();
            var len = 0;

            foreach (var ch in s)
            {
                if (!chs.Contains(char.ToLower(ch)) && !char.IsWhiteSpace(ch))
                {
                    chs.Add(char.ToLower(ch));
                    if (++len == 26) return "pangram";
                }
            }
            return "not pangram";
        }

        // https://www.hackerrank.com/challenges/weighted-uniform-string/problem
        public static string[] weightedUniformStrings(string s, int[] queries)
        {
            var results = new string[queries.Length];
            var weights = new HashSet<int>();
            var w = ((byte)s[0]) - 96;

            weights.Add(w);

            for (var i = 1; i < s.Length; i++)
            {
                if (s[i] == s[i - 1]) { w += ((byte)s[i]) - 96; }
                else { w = ((byte)s[i]) - 96; }
                if (!weights.Contains(w)) weights.Add(w);
            }

            for (var i = 0; i < queries.Length; i++)
                results[i] = weights.Contains(queries[i]) ? "Yes" : "No";

            return results;
        }

        // https://www.hackerrank.com/challenges/gem-stones/problem
        public static int gemstones(string[] arr)
        {
            var minerals = new Dictionary<char, int>();
            var gems = 0;

            foreach (var mineral in arr[0])
            {
                if (!minerals.ContainsKey(mineral))
                    minerals.Add(mineral, 1);
            }

            for (var i = 1; i < arr.Length; i++)
            {
                foreach (var mineral in arr[i].Distinct())
                {
                    if (minerals.ContainsKey(mineral))
                        minerals[mineral]++;
                }
            }

            foreach (var mineralCount in minerals)
            {
                if (mineralCount.Value == arr.Length)
                    gems++;
            }

            return gems;
        }

        // https://www.hackerrank.com/challenges/alternating-characters/problem
        public static int alternatingCharacters(string s)
        {
            var l = 0;
            var ops = 0;

            for (var r = 1; r < s.Length; r++)
            {
                if (s[l] == s[r]) ops++;
                else l = r;
            }

            return ops;
        }

        // https://www.hackerrank.com/challenges/the-love-letter-mystery/problem
        public static int theLoveLetterMystery(string s)
        {
            var ops = 0;
            var l = 0;
            var r = s.Length - 1;
            var mid = s.Length / 2;

            while (l < mid)
                ops += Math.Abs(s[l++] - s[r--]);

            return ops;
        }

        // https://www.hackerrank.com/challenges/game-of-thrones/problem
        public static string gameOfThrones(string s)
        {
            var chars = new Dictionary<char, int>();
            var containsOdd = false;

            foreach (var ch in s)
            {
                if (!chars.ContainsKey(ch)) chars.Add(ch, 1);
                else chars[ch]--;
            }

            foreach (var kvp in chars)
            {
                if (kvp.Value % 2 == 0) continue;
                if (containsOdd) return "NO";
                containsOdd = true;
            }

            return "YES";
        }

        // https://www.hackerrank.com/challenges/two-strings/problem
        public static string twoStrings(string s1, string s2)
        {
            var chars = new HashSet<char>();

            foreach (var ch in s1)
                if (!chars.Contains(ch)) chars.Add(ch);

            foreach (var ch in s2)
                if (chars.Contains(ch)) return "YES";

            return "NO";
        }

        // https://www.hackerrank.com/challenges/anagram/problem
        public static int anagram(string s)
        {
            if (s.Length % 2 == 1) return -1;

            var count = 0;
            var len = s.Length / 2;
            var leftchars = new Dictionary<char, int>();

            for (var i = 0; i < len; i++)
            {
                if (leftchars.ContainsKey(s[i])) leftchars[s[i]]++;
                else leftchars.Add(s[i], 1);
            }

            for (var i = len; i < s.Length; i++)
            {
                if (leftchars.ContainsKey(s[i]) && leftchars[s[i]] > 0) leftchars[s[i]]--;
                else count++;
            }

            return count;
        }

        // https://www.hackerrank.com/challenges/string-construction/problem
        public static int stringConstruction(string s) => s.Distinct().Count();

        // https://www.hackerrank.com/challenges/funny-string/problem
        public static string funnyString(string s)
        {
            var len = s.Length - 1;

            for (int l = 0, r = len; l < len; l++, r--)
            {
                if (Math.Abs(s[l] - s[l + 1]) !=
                    Math.Abs(s[r] - s[r - 1]))
                {
                    return "Not Funny";
                }
            }

            return "Funny";
        }

        // https://www.hackerrank.com/challenges/separate-the-numbers/problem
        public static string separateNumbers(string s)
        {
            if (s[0] == '0' || s.Length < 2) return "NO";

            List<long[]> firsts = new List<long[]>();

            var l = 0;
            var r = 1;

            while (r <= s.Length / 2)
            {
                firsts.Add(new long[] { long.Parse(s.Substring(l, r)), r++ });
            }

            foreach (long[] first in firsts)
            {
                var right = 0L;
                var count = 1L;
                var l2 = first[1];
                var r2 = first[1];

                while (l2 + r2 <= s.Length)
                {
                    right = long.Parse(s.Substring((int)l2, (int)r2)) - count;

                    if (first[0] == right && first[1] <= r2)
                    {
                        l2 += r2;
                        count++;
                    }
                    else if (first[0] > right) r2++;
                    // right is too great in value, leave while loop
                    else
                        break;
                }

                if (first[0] == right && l2 == s.Length) return $"YES {first[0]}";
            }

            return "NO";
        }

        // https://www.hackerrank.com/challenges/making-anagrams/problem
        public static int makingAnagrams(string s1, string s2)
        {
            var count = 0;
            var d_s1 = new Dictionary<char, int>();
            var d_s2 = new Dictionary<char, int>();

            foreach (char ch in s1)
            {
                if (d_s1.ContainsKey(ch)) d_s1[ch]++;
                else
                    d_s1.Add(ch, 1);
            }

            foreach (char ch in s2)
            {
                if (d_s1.ContainsKey(ch)) d_s1[ch]--;
                else if (d_s2.ContainsKey(ch)) d_s2[ch]++;
                else
                    d_s2.Add(ch, 1);
            }

            foreach (var kvp in d_s1)
                count += Math.Abs(kvp.Value);

            foreach (var kvp in d_s2)
                count += kvp.Value;

            return count;
        }

        // https://www.hackerrank.com/challenges/palindrome-index/problem
        public static int palindromeIndex(string s)
        {
            var idx = -1;
            var l = 0;
            var r = s.Length - 1;
            var found = false;

            while (l < r)
            {
                if (s[l] == s[r]) { l++; r--; }
                else if (found) return -1;
                else if (s[l] == s[r - 1] && s[l + 1] == s[r - 2])
                {
                    found = true;
                    idx = r--;
                }
                else if (s[l + 1] == s[r])
                {
                    found = true;
                    idx = l++;
                }
                else return -1;
            }

            return idx;
        }

        // -------------------------------------------------------------------------------
        // https://www.hackerrank.com/challenges/two-characters/problem
        public static int alternate(string s)
        {
            var i = 0;
            var max = 0;
            var chars = new Dictionary<char, List<int>>();

            foreach (char ch in s)
            {
                if (chars.ContainsKey(ch)) chars[ch].Add(i++);
                else chars.Add(ch, new List<int> { i++ });
            }

            // sort the dictionary by value.count (the number of char occurances)
            var ordered = chars.OrderByDescending(kvp => kvp.Value.Count).ToList();

            for (i = 0; i < ordered.Count; i++)
            {
                for (int j = i + 1; j < ordered.Count; j++)
                {
                    // check if the frequency difference
                    // is within the considerable range
                    if (ordered[i].Value.Count - ordered[j].Value.Count <= 1)
                    {
                        // once we enter this block we are looking to qualify the
                        // characters indexes within the string (alternating fashion)
                        max = Math.Max(CheckIndexesAlternate(ordered[i].Value, ordered[j].Value), max);
                    }
                    else break;
                }
            }

            return max;
        }

        private static int CheckIndexesAlternate(List<int> list1, List<int> list2)
        {
            var l1 = 0;     // list 1 index
            var l2 = 0;     // list 2 index
            bool leftmoved; // keeping track of which character index advanced during the previous iteration

            // initialise leftmoved befor while loop
            if (list1[l1] < list2[l2]) 
            { 
                leftmoved = true;
                l1++;
            }
            else 
            { 
                leftmoved = false;
                l2++;
            }

            while (l1 < list1.Count && l2 < list2.Count)
            {
                if (list1[l1] < list2[l2])
                {
                    if (leftmoved) break; // fail
                    leftmoved = true;     // alternate
                    l1++;
                }
                else
                {
                    if (!leftmoved) break; // fail
                    leftmoved = false;     // alternate
                    l2++;
                }
            }

            // final check if both lists have been iterated through
            if (l1 == list1.Count && l2 == list2.Count - 1 ||
                l2 == list2.Count && l1 == list1.Count - 1)
            {
                return list1.Count + list2.Count;
            }

            return 0;
        }
        // -------------------------------------------------------------------------------

        // https://www.hackerrank.com/challenges/caesar-cipher-1/problem
        public static string caesarCipher(string s, int k)
        {
            k %= 26;
            var b = (char)(k + 'a');
            var sb = new StringBuilder();
            var cypher = new Dictionary<char, char>(26);

            for (var i = (char)97; i <= 122; i++)
            {
                cypher.Add(i, b);
                if (b == 122) b = 'a';
                else b++;
            }

            foreach (var ch in s)
            {
                if (char.IsLetter(ch))
                {
                    if (char.IsLower(ch)) sb.Append(cypher[ch]);
                    else sb.Append(char.ToUpper(cypher[char.ToLower(ch)])).ToString();
                }
                else sb.Append(ch);
            }

            return sb.ToString();
        }

        // https://www.hackerrank.com/challenges/beautiful-binary-string/problem
        public static int beautifulBinaryString(string b)
        {
            var i = 0;
            var count = 0;

            while (i < b.Length - 5)
            {
                if (b[i] == '0' &&
                    b[i + 1] == '1' &&
                    b[i + 2] == '0')
                {
                    i += (b[i + 3] == '1' &&
                          b[i + 4] == '0') ? 3 : 2;
                    count++;
                }

                i++;
            }

            if (b.Substring(i).Contains("010"))
            {
                count++;
            }

            return count;
        }
    }
}
