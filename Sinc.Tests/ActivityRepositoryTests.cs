using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sinq.Repositories;
using System.Diagnostics;
using Sinq.Controllers;
using Sinq.Models;
namespace Sinc.Tests
{
    /// <summary>
    /// Summary description for ActivityRepositoryTests
    /// </summary>
    [TestClass]
    public class ActivityRepositoryTests
    {

        public ActivityRepositoryTests()
        {    //
            // TODO: Add constructor logic here
            //
        }

//        private TestContext testContextInstance;

//        /// <summary>
//        ///Gets or sets the test context which provides
//        ///information about and functionality for the current test run.
//        ///</summary>
//        public TestContext TestContext
//        {
//            get
//            {
//                return testContextInstance;
//            }
//            set
//            {
//                testContextInstance = value;
//            }
//        }

//        #region Additional test attributes
//        //
//        // You can use the following additional attributes as you write your tests:
//        //
//        // Use ClassInitialize to run code before running the first test in the class
//        // [ClassInitialize()]
//        // public static void MyClassInitialize(TestContext testContext) { }
//        //
//        // Use ClassCleanup to run code after all tests in a class have run
//        // [ClassCleanup()]
//        // public static void MyClassCleanup() { }
//        //
//        // Use TestInitialize to run code before running each test 
//        // [TestInitialize()]
//        // public void MyTestInitialize() { }
//        //
//        // Use TestCleanup to run code after each test has run
//        // [TestCleanup()]
//        // public void MyTestCleanup() { }
//        //
//        #endregion
         
//        [TestMethod]
//        public void FindActivityById_EntityExists_EntityFound()
//        {
//            var repo = new ActivityRepository();
//            var act = new Activity();
//            act.Id = 4;
//            repo.Add(act);
//            var res = repo.FindActivityBy(act.Id);
//            Assert.IsNotNull(res);
//        }

//        public void FindActivityById_EntityExists_EntityNotFound()
//        {
//            var repo = new ActivityRepository();
//            var res = repo.FindActivityBy(5459);
//            if (res==null)
//            {
//                Assert.IsNull(res);
//            }
//        }

//        public void StartActivity_EntityExists_EntityFound()
//        {
//            //Daca ∃ start ->> ∃ act <> null ( ∀ activitate tre' sa inceapa)
//            var act = new Activity();
//            act.Id = 4;
//            ActivityRepository repo = new ActivityRepository();
//            repo.Add(act);
//            var res = repo.FindActivityBy(act.Id);
//            var result = repo.StartActivity(act.Id);
//            if (result != null)
//            {
//                Assert.IsNotNull(res);       
//            }

//        }

//        public void StartActivity_EntityExists_EntityNotFound()
//        {
//            //Daca nu imi gaseste o activitate ->> nu exista start si nici end
//            var repo = new ActivityRepository();
//            var res = repo.FindActivityBy(5555);
//            var res2 = repo.StartActivity(5555);
//            var res3 = repo.EndActivity(5555);
//            if (res == null)
//            {
//                Assert.IsNull(res2);
//                Assert.IsNull(res3);
//            }
//        }


//        public void EndActivity_EntityExists_EntityFound()
//        {
//            //verific daca ∃ end act =>> trebuie sa ∃ activitatea respectiva =>> ∃ start 
//            // nu poate ∃ end fara start 
//            var act = new Activity();
//            act.Id = 4;
//            ActivityRepository repo = new ActivityRepository();
//            repo.Add(act);
//            var result = repo.EndActivity(act.Id);
//            var res = repo.FindActivityBy(act.Id);
//            if (result != null) 
//            {
//                if (result != null) { 
//                    var rez = repo.StartActivity(act.Id);
//                    Assert.IsNotNull(rez);
//                }
//            }     
//        }

//        [TestMethod]
//        public void GetActivities_EntityExists_EntityFound(){
//            var repo = new ActivityRepository();
            
//            var act = new Activity();
//            act.Id = 4;
//            repo.Add(act);
            
//            var act2 = new Activity();
//            act2.Id = 5;
//            repo.Add(act2);

//            var rez = repo.GetActivities();
//            Assert.IsNotNull(rez);
//        }

        
//        public void RemoveActivityById_EntityExists_EntityFound()
//        {
//            var repo = new ActivityRepository();
//            var act = new Activity();
//            act.Id = 4;
//            repo.Add(act);
//            var res = repo.FindActivityBy(act.Id);
//            if (res != null)
//            {
//                repo.Remove(act.Id);
//                var res2 = repo.FindActivityBy(4);
//                Assert.IsNull(res2);
//            }
//        }

        [TestMethod]
        public void StartToEndFlow()
        {
            var unitOfWork = new ActivityUnitOfWork();
            var repo = unitOfWork.ActivityRepository;

            var at = new ActivityTime();
            var act = new Activity()
            {
                Name = "test",
                DueDate = DateTimeOffset.Now.AddHours(2),
                CompletedAt = DateTime.Now,
                //act.ActivityTimes.StartDate = DateTimeOffset.Now.AddHours(10),
            };

            at.EndDate = DateTimeOffset.Now;

            repo.Insert(act);
            unitOfWork.Save();

            var startedActivityTime = unitOfWork.StartActivity(act.Id);
            Assert.IsNotNull(startedActivityTime);
            Assert.IsTrue(DateTimeOffset.Now > startedActivityTime.StartDate);

            //var endedActivityTime = repo.EndActivity(act.Id);
            //repo.SaveChanges();
            //   Assert.IsNotNull(endedActivityTime);
            //Assert.IsNull(endedActivityTime);

            //      Assert.IsNotNull(endedActivityTime.EndDate);

            //           Assert.IsTrue(DateTimeOffset.Now > startedActivityTime.EndDate);
            //            Assert.AreEqual(startedActivityTime.StartDate, endedActivityTime.StartDate);
        }
        

        
    }
}
