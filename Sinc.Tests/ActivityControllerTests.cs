using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sinq.Controllers;

namespace Sinc.Tests
{
    [TestClass]
    public class ActivityControllerTests
    {
        [TestMethod]
        public void Details_IdIsNull_BadRequestReturned()
        {
            var controller = new ActivitiesController();

            var result = controller.Edit((int?)null);

            
        }
    }
}
