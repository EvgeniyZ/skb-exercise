using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Kontur
{
    public class Tests
    {
        [Test]
        public void TestUnlovedChildren()
        {
            var childNames = new Dictionary<int, string>
            {
                {1, "Masha" },
                {2, "Sasha" },
                {3, "Pasha" },
                {4, "Sveta" },
                {5, "Roma" },
                {6, "Andrew" },
                {7, "Eva" },
                {8, "Nastya" },
                {9, "Eugene" },
            };
            var relationships = new List<Tuple<int, int>>
            {
                new Tuple<int, int> (1, 2),
                new Tuple<int, int> (1, 3),
                new Tuple<int, int> (2, 9),
                new Tuple<int, int> (4, 5),
                new Tuple<int, int> (5, 1),
                new Tuple<int, int> (6, 7),
                new Tuple<int, int> (7, 6),
                new Tuple<int, int> (8, 2),
                new Tuple<int, int> (9, 2),
            };
            var expected = new List<string> { "Sveta", "Nastya" };
            Assert.AreEqual(expected, childNames.UnlovedChildren(relationships));
            relationships = new List<Tuple<int, int>>
            {
                new Tuple<int, int> (1, 2),
                new Tuple<int, int> (2, 4),
                new Tuple<int, int> (4, 5),
                new Tuple<int, int> (5, 1),
                new Tuple<int, int> (6, 7),
                new Tuple<int, int> (7, 6),
                new Tuple<int, int> (8, 9),
                new Tuple<int, int> (9, 8),
            };
            expected = new List<string> {"Pasha"};
            Assert.AreEqual(expected, childNames.UnlovedChildren(relationships));
        }

        [Test]
        public void TestFriendzonedChildren()
        {
            var childNames = new Dictionary<int, string>
            {
                {1, "Masha" },
                {2, "Sasha" },
                {3, "Pasha" },
                {4, "Sveta" },
                {5, "Roma" },
            };
            var relationships = new List<Tuple<int, int>>
            {
                new Tuple<int, int> (1, 2),
                new Tuple<int, int> (1, 3),
                new Tuple<int, int> (2, 3),
                new Tuple<int, int> (3, 1),
                new Tuple<int, int> (3, 5),
                new Tuple<int, int> (4, 1),
                new Tuple<int, int> (4, 2),
                new Tuple<int, int> (5, 4),
                new Tuple<int, int> (5, 3),
            };
            var expected = new List<string> { "Sasha", "Sveta" };
            Assert.AreEqual(expected, childNames.FriendzonedChildren(relationships));
            relationships = new List<Tuple<int, int>>
            {
                new Tuple<int, int> (1, 2),
                new Tuple<int, int> (1, 3),
                new Tuple<int, int> (2, 3),
                new Tuple<int, int> (3, 2),
            };
            expected = new List<string> { "Masha" };
            Assert.AreEqual(expected, childNames.FriendzonedChildren(relationships));
        }

        [Test]
        public void TestLovePopularChildren()
        {
            var childNames = new Dictionary<int, string>
            {
                {1, "Masha" },
                {2, "Sasha" },
                {3, "Pasha" },
            };
            var relationships = new List<Tuple<int, int>>
            {
                new Tuple<int, int> (1, 2),
                new Tuple<int, int> (1, 3),
                new Tuple<int, int> (2, 3),
                new Tuple<int, int> (3, 1),
            };
            var expected = new List<string> { "Pasha" };
            Assert.AreEqual(expected, childNames.LovePopularChildren(relationships));
            relationships = new List<Tuple<int, int>>
            {
                new Tuple<int, int> (1, 2),
                new Tuple<int, int> (1, 3),
                new Tuple<int, int> (3, 1),
                new Tuple<int, int> (2, 1),
                new Tuple<int, int> (2, 3),
            };
            expected = new List<string> { "Masha", "Pasha" };
            Assert.AreEqual(expected, childNames.LovePopularChildren(relationships));
            childNames = new Dictionary<int, string>
            {
                {1, "Masha" },
                {2, "Sasha" },
                {3, "Pasha" },
                {4, "Sveta" },
                {5, "Roma" },
                {6, "Andrew" },
                {7, "Eva" },
            };
            relationships = new List<Tuple<int, int>>
            {
                new Tuple<int, int> (1, 2),
                new Tuple<int, int> (1, 3),
                new Tuple<int, int> (3, 1),
                new Tuple<int, int> (2, 1),
                new Tuple<int, int> (4, 7),
                new Tuple<int, int> (5, 7),
                new Tuple<int, int> (6, 7),
                new Tuple<int, int> (7, 3),
            };
            expected = new List<string> { "Eva" };
            Assert.AreEqual(expected, childNames.LovePopularChildren(relationships));
        }
    }
}
