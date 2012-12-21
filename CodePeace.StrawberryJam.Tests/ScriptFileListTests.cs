using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CodePeace.StrawberryJam.Tests
{
    [TestClass()]
    public class ScriptFileList_Test
    {
        [TestMethod()]
        public void ScriptFileList_Constructor_Returns_Not_Null()
        {
            ScriptFileList target = new ScriptFileList();

            Assert.IsNotNull(target);
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void ScriptFileList_Add_Null_ScriptInfo()
        {
            ScriptFileList target = new ScriptFileList();
            ScriptInfo info = null;
            target.Add(info);
        }

        [TestMethod()]
        public void ScriptFileList_Add_ScriptInfo_Increases_Collection_Count()
        {
            ScriptFileList target = new ScriptFileList();
            ScriptInfo info = new ScriptInfo("", "", ScriptType.JavaScript);
            target.Add(info);

            int expected = 1;
            int actual = target.Scripts.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ScriptFileList_Add_JavaScript_ScriptInfo_Is_Added_To_Collection()
        {
            ScriptFileList target = new ScriptFileList();
            ScriptInfo info = new ScriptInfo("", "", ScriptType.JavaScript);
            target.Add(info);

            bool contains = target.Scripts.Contains(info);

            Assert.IsTrue(contains);
        }

        [TestMethod()]
        public void ScriptFileList_Add_StyleSheet_ScriptInfo_Is_Added_To_Collection()
        {
            ScriptFileList target = new ScriptFileList();
            ScriptInfo info = new ScriptInfo("", "", ScriptType.Stylesheet);
            target.Add(info);

            bool contains = target.Scripts.Contains(info);

            Assert.IsTrue(contains);
        }
    }
}
