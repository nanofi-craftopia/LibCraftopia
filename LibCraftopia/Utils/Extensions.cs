using Oc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;

namespace LibCraftopia.Utils
{
    public static class Extensions
    {
        public static string RemoveTag(this string original)
        {
            return Regex.Replace(original, @"\<([^\>]*)\>", match =>
            {
                return match.Groups[1].Value;
            });

        }
        public static string ToValidKey(this string original)
        {
            Regex pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
            original = original.RemoveTag();
            var matches = pattern.Matches(original);
            var term = new List<string>(matches.Count);
            foreach (var obj in matches)
            {
                var match = (Match)obj;
                term.Add(match.Value);
            }
            return new CultureInfo("en-US", false).TextInfo
                .ToTitleCase(string.Join(" ", term).ToLower())
                .Replace(@" ", string.Empty);
        }

        internal static Task LogError(this Task self)
        {
            return self.ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    var e = task.Exception;
                    Logger.Inst.LogError(e);
                    Logger.Inst.LogError(e.Message);
                    Logger.Inst.LogError(e.StackTrace);
                }
            });
        }

        public static void Increment<K>(this Dictionary<K,int> self, K key)
        {
            if (self.TryGetValue(key, out var count))
            {
                self[key] = count + 1;
            }
            else
            {
                self.Add(key, 1);
            }
        }
    }
}
