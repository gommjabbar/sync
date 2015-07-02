using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sinq.Repositories;

namespace Sinc.Tests
{
    /// <summary>
    /// Summary description for ActivityRepositoryTests
    /// </summary>
    [TestClass]
    public class ActivityRepositoryTests
    {
        public ActivityRepositoryTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
         
        [TestMethod]
        public void FindActivityById_EntityExists_EntityFound()
        {
            var repo = new ActivityRepository();
            var res = repo.FindActivityBy(3);
            Assert.IsNotNull(res);
        }

        public void FindActivityById_EntityExists_EntityNotFound()
        {
            var repo = new ActivityRepository();
            var res = repo.FindActivityBy(5459);
            Assert.IsNull(res);
        }


        public void StartActivity_EntityExists_EntityFound()
        {
            //Daca imi gaseste o activitate ->> start de activitate
            //tre' sa fie diferit de null (orice activitate tre' sa inceapa)
            var repo = new ActivityRepository();
            var res = repo.FindActivityBy(3);
            var res2 = repo.StartActivity(3);
            if (res != null)
            {
                Assert.IsNotNull(res2);
            }
        }

        public void StartActivity_EntityExists_EntityNotFound()
        {
            //Daca nu imi gaseste o activitate ->> nu exista start
            var repo = new ActivityRepository();
            var res = repo.FindActivityBy(5555);
            var res2 = repo.StartActivity(5555);
            if (res == null)
            {
                Assert.IsNull(res2);
            }
        }

        
        public void EndActivity_EntityExists_EntityFound()
        {
            var repo = new ActivityRepository();
            var res = repo.FindActivityBy(3);
            if (res != null) {
 
            
            }

            
        }

    }
}
