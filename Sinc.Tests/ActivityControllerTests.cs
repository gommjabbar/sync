using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sinq.Controllers;
using System.Threading;
using Sinq.App_Start;
using Sinq.Repositories;
using Moq;
using Sinq.Models;

namespace Sinc.Tests
{
    [TestClass]
    public class ActivityControllerTests
    {
        Mock<IActivityUnitOfWork> _unitOfWork;
        public ActivityControllerTests()
        {
            AutoMapperConfig.Configure();
            _unitOfWork = new Mock<IActivityUnitOfWork>();
        }

        [TestMethod]
        public void GetAllActivities_ActivitiesExist_ResultsOK()
        {
            Mock<IGenericRepository<Activity>> repository = new Mock<IGenericRepository<Activity>>();

            repository.Setup(repo => repo.GetAll()).Returns(
                new Activity[] { new Activity() { Id =0, Name="test" } }
                );
            _unitOfWork.Setup(unitOfWork => unitOfWork.ActivityRepository)
                .Returns(repository.Object);

            var controller = new ActivitiesApiController(_unitOfWork.Object);

            var response = controller.GetAllActivities();
            response.ExecuteAsync(new CancellationToken(true));

            Assert.IsNotNull(response.Result);
        }

        [TestMethod]
        public void Delete_ActivityExists_DeleteOK()
        {
            Mock<IGenericRepository<Activity>> repository = new Mock<IGenericRepository<Activity>>();

            repository.Setup(repo => repo.Delete(It.IsAny<int>()))
                .Returns(true);

            _unitOfWork.Setup(unitOfWork => unitOfWork.ActivityRepository)
                .Returns(repository.Object);

            var controller = new ActivitiesApiController(_unitOfWork.Object);

            var response = controller.Delete(2);

            response.ExecuteAsync(new CancellationToken(true));

            Assert.IsTrue(response.Result);
        }
    }
}
