using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CodePeace.StrawberryJam.Tests
{
    [TestClass()]
    public class ScriptInfo_Test
    {
        [TestMethod()]
        public void ScriptInfoConstructor_Test()
        {
            string url = "Foo";
            string localPath = "Foo";
            string cdnPath = "Foo";
            ScriptType scriptType = new ScriptType();
            bool siteWide = false;
            string area = "Foo";
            ScriptInfo target = new ScriptInfo(url, localPath, cdnPath, scriptType, siteWide, area);
        }

        [TestMethod()]
        public void ScriptInfoConstructor_Source_Test()
        {
            string source = "Foo";
            string cdnPath = "Foo";
            ScriptType scriptType = new ScriptType();
            bool siteWide = false;
            string area = "Foo";
            string key = "123";
            ScriptInfo target = new ScriptInfo(source, cdnPath, scriptType, key, siteWide, area);
        }

        [TestMethod()]
        public void Area_Test()
        {
            string url = "Foo";
            string localPath = "Foo";
            string cdnPath = "Foo";
            ScriptType scriptType = new ScriptType();
            bool siteWide = false;
            string area = "Foo";
            ScriptInfo target = new ScriptInfo(url, localPath, cdnPath, scriptType, siteWide, area);
            string expected = "Foo";
            string actual;
            target.Area = expected;
            actual = target.Area;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CDNPath_Test()
        {
            string url = "Foo";
            string localPath = "Foo";
            string cdnPath = "Foo";
            ScriptType scriptType = new ScriptType();
            bool siteWide = false;
            string area = "Foo";
            ScriptInfo target = new ScriptInfo(url, localPath, cdnPath, scriptType, siteWide, area);
            string expected = "Foo";
            string actual;
            target.CDNPath = expected;
            actual = target.CDNPath;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LocalPath_Test()
        {
            string url = "Foo";
            string localPath = "Foo";
            string cdnPath = "Foo";
            ScriptType scriptType = new ScriptType();
            bool siteWide = false;
            string area = "Foo";
            ScriptInfo target = new ScriptInfo(url, localPath, cdnPath, scriptType, siteWide, area);
            string expected = "Foo";
            string actual;
            target.LocalPath = expected;
            actual = target.LocalPath;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ScriptType_Test()
        {
            string url = "Foo";
            string localPath = "Foo";
            string cdnPath = "Foo";
            ScriptType scriptType = new ScriptType();
            bool siteWide = false;
            string area = "Foo";
            ScriptInfo target = new ScriptInfo(url, localPath, cdnPath, scriptType, siteWide, area);
            ScriptType expected = new ScriptType();
            ScriptType actual;
            target.ScriptType = expected;
            actual = target.ScriptType;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SiteWide_Test()
        {
            string url = "Foo";
            string localPath = "Foo";
            string cdnPath = "Foo";
            ScriptType scriptType = new ScriptType();
            bool siteWide = false;
            string area = "Foo";
            ScriptInfo target = new ScriptInfo(url, localPath, cdnPath, scriptType, siteWide, area);
            bool expected = false;
            bool actual;
            target.SiteWide = expected;
            actual = target.SiteWide;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Url_Test()
        {
            string url = "Foo";
            string localPath = "Foo";
            string cdnPath = "Foo";
            ScriptType scriptType = new ScriptType();
            bool siteWide = false;
            string area = "Foo";
            ScriptInfo target = new ScriptInfo(url, localPath, cdnPath, scriptType, siteWide, area);
            string expected = "Foo";
            string actual;
            target.Url = expected;
            actual = target.Url;
            Assert.AreEqual(expected, actual);
        }
    }
}
