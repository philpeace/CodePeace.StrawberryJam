using DrFoster.Common.Web.Minification;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CodePeace.StrawberryJam.Tests
{
    [TestClass()]
    public class ScriptGenerationInfo_Test
    {
        [TestMethod()]
        public void ScriptGenerationInfoConstructor_Test()
        {
            ScriptGenerationInfo target = new ScriptGenerationInfo();
        }

        [TestMethod()]
        public void Area_Test()
        {
            ScriptGenerationInfo target = new ScriptGenerationInfo(); // TODO: Initialize to an appropriate value
            string expected = "Foo"; // TODO: Initialize to an appropriate value
            string actual;
            target.Area = expected;
            actual = target.Area;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Type_Test()
        {
            ScriptGenerationInfo target = new ScriptGenerationInfo(); // TODO: Initialize to an appropriate value
            ScriptType expected = new ScriptType(); // TODO: Initialize to an appropriate value
            ScriptType actual;
            target.Type = expected;
            actual = target.Type;
            Assert.AreEqual(expected, actual);
        }
    }
}
