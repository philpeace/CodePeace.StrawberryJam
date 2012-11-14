using System.Web;
using DrFoster.Common.Web.MVC;
using DrFoster.Common.Web.Minification;
using DrFoster.Common.Web.Tests.MVC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DrFoster.Common.Caching;
using System.Collections.Generic;
using Moq;

namespace CodePeace.StrawberryJam.Tests
{
    [TestClass()]
    public class ScriptManager_Test
    {
        [TestMethod()]
        public void ScriptManager_Constructor_Returns_Not_Null()
        {
            var cacheManager = new Mock<ICacheManager>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var target = new ScriptManager(cacheManager.Object, contextAccessor.Object);

            Assert.IsNotNull(target);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ScriptManager_Add_Null_Throws_Exception_Test()
        {
            var cacheManager = new Mock<ICacheManager>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var target = new ScriptManager(cacheManager.Object, contextAccessor.Object);

            ScriptInfo info = null;
            target.Add(info);
        }

        [TestMethod()]
        public void ScriptManager_Add_External_JavaScript_ScriptInfo_Test()
        {
            var cacheManager = new Mock<ICacheManager>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var list = new Mock<IScriptFileList>();

            var context = new Mock<HttpContextBase>
            {
                DefaultValue = DefaultValue.Mock
            };

            contextAccessor.Setup(c => c.Current()).Returns(context.Object);
            context.SetupGet(c => c.Application["__sm__JavaScript"]).Returns(() => list.Object);

            var target = new ScriptManager(cacheManager.Object, contextAccessor.Object);

            IScriptInfo info = new ScriptInfo(null, "SomeLocalPath", null, ScriptType.JavaScript);

            target.Add(info);

            list.Verify(l => l.Add(info), Times.Once());
        }

        [TestMethod()]
        public void ScriptManager_Add_External_StyleSheet_ScriptInfo_Test()
        {
            var cacheManager = new Mock<ICacheManager>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var list = new Mock<IScriptFileList>();

            var context = new Mock<HttpContextBase>
            {
                DefaultValue = DefaultValue.Mock
            };

            contextAccessor.Setup(c => c.Current()).Returns(context.Object);
            context.SetupGet(c => c.Application["__sm__Stylesheet"]).Returns(() => list.Object);

            var target = new ScriptManager(cacheManager.Object, contextAccessor.Object);

            IScriptInfo info = new ScriptInfo(null, "SomeLocalPath", null, ScriptType.Stylesheet);

            target.Add(info);

            list.Verify(l => l.Add(info), Times.Once());
        }

        [TestMethod()]
        public void ScriptManager_ListForType_Creates_New_ScriptFileList_Test()
        {
            var cacheManager = new Mock<ICacheManager>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var list = new Mock<IScriptFileList>();
            var items = new Dictionary<string, object>();
            items.Add("__sm__Stylesheet", list.Object);

            var context = new Mock<HttpContextBase>
            {
                DefaultValue = DefaultValue.Mock
            };

            contextAccessor.Setup(c => c.Current()).Returns(context.Object);
            context.SetupGet(c => c.Items).Returns(items);

            var target = new ScriptManager(cacheManager.Object, contextAccessor.Object);

            var type = ScriptType.JavaScript;
            IScriptFileList actual;

            actual = target.ListForType(type);
            Assert.IsNotNull(actual);
        }

        [TestMethod()]
        public void ScriptManager_ListForType_Returns_Existing_ScriptFileList_Test()
        {
            var cacheManager = new Mock<ICacheManager>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var list = new Mock<IScriptFileList>();

            var context = new Mock<HttpContextBase>
            {
                DefaultValue = DefaultValue.Mock
            };

            contextAccessor.Setup(c => c.Current()).Returns(context.Object);
            context.SetupGet(c => c.Application["__sm__JavaScript"]).Returns(() => list.Object);

            var target = new ScriptManager(cacheManager.Object, contextAccessor.Object);

            context.Object.Items["__sm__JavaScript"] = list.Object;

            var type = ScriptType.JavaScript;
            IScriptFileList actual;

            actual = target.ListForType(type);
            Assert.IsNotNull(actual);
            Assert.AreSame(list.Object, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ScriptManager_Generate_With_Null_ScriptInfo_Throws_Exception_Test()
        {
            var cacheManager = new Mock<ICacheManager>();
            var contextAccessor = new Mock<IHttpContextAccessor>();

            var target = new ScriptManager(cacheManager.Object, contextAccessor.Object);

            IScriptGenerationInfo info = null;
            IScriptOutput actual;
            actual = target.Generate(info);
        }

        [TestMethod()]
        public void ScriptManager_Generate_Sets_Correct_ContentType_For_JavaScript()
        {
            var cacheManager = new Mock<ICacheManager>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var info = new Mock<IScriptGenerationInfo>();
            info.SetupGet(i => i.Type).Returns(ScriptType.JavaScript);
            var list = new Mock<IScriptFileList>();
            var items = new Dictionary<string, object>();
            items.Add("__sm__JavaScript", list.Object);

            var context = new Mock<HttpContextBase>
            {
                DefaultValue = DefaultValue.Mock
            };

            contextAccessor.Setup(c => c.Current()).Returns(context.Object);
            context.SetupGet(c => c.Items).Returns(items);

            var target = new ScriptManager(cacheManager.Object, contextAccessor.Object);

            IScriptOutput actual;
            actual = target.Generate(info.Object);

            var expected = @"application/javascript";

            Assert.AreEqual(expected, actual.ContentType);
        }

        [TestMethod()]
        public void ScriptManager_Generate_Sets_Correct_ContentType_For_Stylesheet()
        {
            string area = "foo";

            List<IScriptInfo> scripts = new List<IScriptInfo>();

            var cacheManager = new Mock<ICacheManager>();
            var contextAccessor = new Mock<IHttpContextAccessor>();

            var info = new Mock<IScriptGenerationInfo>();
            info.SetupGet(i => i.Type).Returns(ScriptType.Stylesheet);
            info.SetupGet(i => i.Area).Returns(area);

            var list = new Mock<IScriptFileList>();

            list.Setup(l => l.ScriptsForArea(area)).Returns(scripts);

            var items = new Dictionary<string, object>();
            items.Add("__sm__Stylesheet", list.Object);

            var context = new Mock<HttpContextBase>
            {
                DefaultValue = DefaultValue.Mock
            };

            contextAccessor.Setup(c => c.Current()).Returns(context.Object);
            context.SetupGet(c => c.Items).Returns(items);

            var target = new ScriptManager(cacheManager.Object, contextAccessor.Object);

            IScriptOutput actual;
            actual = target.Generate(info.Object);

            var expected = @"text/css";

            Assert.AreEqual(expected, actual.ContentType);
        }
    }
}
