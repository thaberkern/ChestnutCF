using Chestnut;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChestnutTests
{
    [TestClass]
    public class ContainerTest
    {
        private Container container; 

        public ContainerTest()
        {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional Testattributes

        [TestInitialize()]
        public void setup()
        {
            this.container = new Container();
        }
        #endregion

        [TestMethod]
        public void HasService()
        {
            Assert.IsFalse(this.container.Has("some.service.id"));

            Service dummy = new Service(typeof(DummyTestClass), Scope.Singleton, ResolveType.Eager);
            container.Register("some.service.id", dummy);
            Assert.IsTrue(container.Has("some.service.id"));
        }

        [TestMethod]
        public void RegisterOneService()
        {
            Service dummy = new Service(typeof(DummyTestClass), Scope.Singleton, ResolveType.Eager);
            this.container.Register("some.service.id", dummy);
            Assert.AreEqual(1, this.container.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ContainerException))]
        public void ExceptionReusingIdDuringRegister()
        {
            Service dummy = new Service(typeof(DummyTestClass), Scope.Singleton, ResolveType.Eager);
            this.container.Register("some.service.id", dummy);
            this.container.Register("some.service.id", dummy);
        }

        [TestMethod]
        public void RemoveRegisterdService()
        {
            Service dummy1 = new Service(typeof(DummyTestClass), Scope.Singleton, ResolveType.Eager);
            Service dummy2 = new Service(typeof(DummyTestClass), Scope.Singleton, ResolveType.Eager);
            this.container.Register("some.service.id.1", dummy1);
            this.container.Register("some.service.id.2", dummy1);

            this.container.Remove("some.service.id.1");

            Assert.AreEqual(1, this.container.Count());
            Assert.IsTrue(this.container.Has("some.service.id.2"));
        }
    }

    public class DummyTestClass
    {
        
    }
}
