using System;
using System.Collections.Generic;
using System.Linq;

namespace Kontur
{
    public static class ChildActions
    {
        public static List<string> UnlovedChildren(this Dictionary<int, string> childNames, ICollection<Tuple<int, int>> relationships)
        {
            var unlovedIds = childNames.Select(x => x.Key).Except(relationships.Select(x => x.Item2)).ToList();

            return unlovedIds.Select(id => childNames[id]).ToList();
        }

        public static List<string> FriendzonedChildren(this Dictionary<int, string> childNames, ICollection<Tuple<int, int>> relationships)
        {
            var loveDict = new Dictionary<int, List<int>>();
            var whoLovedIds = relationships.Select(x => x.Item1).Distinct().ToList();
            foreach (var id in whoLovedIds)
            {
                foreach (var relation in relationships)
                {
                    if (id == relation.Item1)
                    {
                        if (loveDict.ContainsKey(id))
                        {
                            loveDict[id].Add(relation.Item2);
                        }
                        else
                        {
                            loveDict[id] = new List<int> { relation.Item2 };
                        }
                    }
                }
            }
            var childrenWithFairLove = (from love in loveDict
                                        from whoLoved in love.Value
                                        where loveDict.ContainsKey(whoLoved)
                                        where loveDict[whoLoved].Contains(love.Key)
                                        select love.Key).ToList();

            return whoLovedIds.Where(x => !childrenWithFairLove.Contains(x)).Select(x => childNames[x]).ToList();
        }

        public static List<string> LovePopularChildren(this Dictionary<int, string> childNames, ICollection<Tuple<int, int>> relationships)
        {
            var whoLovedIds = relationships.Select(x => x.Item1).Distinct().ToList();
            var maxForEachChild = new Dictionary<int, int>();
            foreach (var id in whoLovedIds)
            {
                var max = 0;
                foreach (var relation in relationships)
                {
                    if (id == relation.Item2)
                    {
                        max++;
                    }
                }
                maxForEachChild.Add(id, max);
            }
            var maximum = maxForEachChild.Max(x => x.Value);

            return maxForEachChild
                .Where(x => x.Value == maximum)
                .Select(x => childNames[x.Key]).ToList();
        }
    }
}
