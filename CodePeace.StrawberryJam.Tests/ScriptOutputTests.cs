using DrFoster.Common.Web.Minification;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CodePeace.StrawberryJam.Tests
{
    [TestClass()]
    public class ScriptOutput_Test
    {
        [TestMethod()]
        public void ScriptOutputConstructor_Test()
        {
            ScriptOutput target = new ScriptOutput();
        }

        [TestMethod()]
        public void ContentType_Test()
        {
            ScriptOutput target = new ScriptOutput();
            string expected = "Foo";
            string actual;
            target.ContentType = expected;
            actual = target.ContentType;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FromCache_Test()
        {
            ScriptOutput target = new ScriptOutput();
            bool expected = false;
            bool actual;
            target.FromCache = expected;
            actual = target.FromCache;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Output_Test()
        {
            ScriptOutput target = new ScriptOutput();
            string expected = "Foo";
            string actual;
            target.Output = expected;
            actual = target.Output;
            Assert.AreEqual(expected, actual);
        }
    }
}
