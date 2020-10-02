using Oc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using LibCraftopia.Registry;

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
                    Logger.Inst.LogException(task.Exception);
                }
            });
        }

        public static void Increment<K>(this Dictionary<K, int> self, K key)
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

        internal static IEnumerator LogErrored(this IEnumerator self)
        {
            while (true)
            {
                try
                {
                    var next = self.MoveNext();
                    if (!next) break;
                }
                catch (Exception e)
                {
                    Logger.Inst.LogException(e);
                    break;
                }
                yield return self.Current;
            }
        }

        internal static Task RegisterVanillaElements<T>(this Registry<T> registry, IEnumerable<T> elements, Func<T, string> keyGen, Func<T, object> conflictInfo = null) where T : IRegistryEntry
        {
            return Task.Run(() =>
            {
                var counts = new Dictionary<string, int>();
                var list = new List<Tuple<string, T>>();
                foreach (var elem in elements)
                {
                    string key = keyGen(elem);
                    counts.Increment(key);
                    list.Add(Tuple.Create(key, elem));
                }
                var unique = new Dictionary<string, int>();
                foreach (var tuple in list)
                {
                    var key = tuple.Item1;
                    var elem = tuple.Item2;
                    if(counts[key] > 1)
                    {
                        var info = conflictInfo?.Invoke(elem); 
                        Logger.Inst.LogWarning($"Confliction: {elem.Id}, {key}, {info}");
                        unique.Increment(key);
                        key += $"-{unique[key]}";
                    }
                    registry.RegisterVanilla(key, elem);
                }
            }).LogError();
        }

        public static IEnumerator AsCoroutine(this Task task, float sec = 0.1f)
        {
            while (!task.IsCompleted && !task.IsCanceled) yield return new WaitForSeconds(sec);
        }
    }
}
