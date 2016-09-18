using System;
using System.Collections.Generic;
using System.IO;

namespace Kontur
{
    public class Program
    {
        static void Main(string[] args)
        {
            var namesFile = File.Open("names.txt", FileMode.Open);
            var relationshipsFile = File.Open("relationships.txt", FileMode.Open);
            var childNames = new Dictionary<int, string>();
            var relationships = new List<Tuple<int, int>> {};
            using (var streamReader = new StreamReader(namesFile))
            {
                var namesAsString = streamReader.ReadToEnd();
                var keyValues = namesAsString.Split(' ');
                foreach (var keyValue in keyValues)
                {
                    var split = keyValue.Split(':');
                    childNames.Add(Convert.ToInt32(split[0]), split[1]);
                }
            }
            using (var streamReader = new StreamReader(relationshipsFile))
            {
                var relationshipsAsString = streamReader.ReadToEnd();
                var keyValues = relationshipsAsString.Split(' ');
                foreach (var keyValue in keyValues)
                {
                    var split = keyValue.Split('>');
                    relationships.Add(new Tuple<int, int>(Convert.ToInt32(split[0]), Convert.ToInt32(split[1])));
                }
            }
            var unlovedChildren = childNames.UnlovedChildren(relationships);
            Console.WriteLine("Unloved Children");
            foreach (var child in unlovedChildren)
            {
                Console.WriteLine(child);
            }
            var friendZonedChildren = childNames.FriendzonedChildren(relationships);
            Console.WriteLine("Friendzoned children");
            foreach (var child in friendZonedChildren)
            {
                Console.WriteLine(child);
            }
            var popularChildren = childNames.LovePopularChildren(relationships);
            Console.WriteLine("Popular Children");
            foreach (var child in popularChildren)
            {
                Console.WriteLine(child);
            }
        }
    }
}
