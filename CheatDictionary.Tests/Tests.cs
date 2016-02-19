using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheatDictionary;

namespace CheatDictionary.Tests
{   
    [TestFixture]
    public class Tests
    {
        public CheatDictionary<string, string> cDic { get; set; }

        [SetUp]
        public void SetUp()
        {
            cDic = new CheatDictionary<string, string>();
        }

        [Test]
        public void Add_KeyExists()
        {
            const string key = "123";
            const string value = "321";

            cDic.Add(key, value);

            Assert.IsTrue(cDic.ContainsKey(key));
        }

        [Test]
        public void Add_IndexerValueSame()
        {
            const string key = "123";
            const string value = "321";

            cDic.Add(key, value);

            Assert.AreEqual(cDic[key], value);
        }

        [Test]
        public void Add_TryGetValueSame()
        {
            const string key = "123";
            const string value = "321";
            string outValue = null;

            cDic.Add(key, value);

            Assert.IsTrue(cDic.TryGetValue(key, out outValue));
            Assert.AreEqual(outValue, value);
        }

        [Test]
        public void AddSameKeys_NoException()
        {
            const string key = "123";
            const string value1 = "321";
            const string value2 = "321321";

            cDic.Add(key, value1);
            cDic.Add(key, value2);
        }

        [Test]
        public void AddSameKeys_ForeachContainsBoth()
        {
            const string key = "123";
            const string value1 = "321";
            const string value2 = "321321";

            cDic.Add(key, value1);
            cDic.Add(key, value2);

            var l = cDic.ToList();

            Assert.AreEqual(l[0].Key, key);
            Assert.AreEqual(l[1].Key, key);

            Assert.AreEqual(l[0].Value, value1);
            Assert.AreEqual(l[1].Value, value2);
        }

        [Test]
        public void AddKeyValuePair_KeyExists()
        {
            const string key = "123";
            const string value = "321";

            cDic.Add(new KeyValuePair<string, string>(key, value));

            Assert.IsTrue(cDic.ContainsKey(key));
        }

        [Test]
        public void AddKeyValuePair_KvpExists()
        {
            const string key = "123";
            const string value = "321";

            var kvp = new KeyValuePair<string, string>(key, value);

            cDic.Add(new KeyValuePair<string, string>(key, value));

            Assert.IsTrue(cDic.Contains(kvp));
        }

        [Test]
        public void Add100_CountEquals100()
        {
            for (int i = 0; i < 100; i++)
            {
                cDic.Add(i.ToString(), (i * 2 + 1).ToString());
            }

            Assert.AreEqual(cDic.Count, 100);
        }

        [Test]
        public void Add100_Clear_CountEquals0()
        {
            for (int i = 0; i < 100; i++)
            {
                cDic.Add(i.ToString(), (i * 2 + 1).ToString());
            }

            Assert.AreEqual(cDic.Count, 100);

            cDic.Clear();

            Assert.AreEqual(cDic.Count, 0);
        }
    }
}
