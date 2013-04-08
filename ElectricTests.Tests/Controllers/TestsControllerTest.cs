using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElectricTests.Controllers;
using ElectricTests.Repository;
using ElectricTests.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Web.Mvc;

namespace ElectricTests.Tests.Controllers
{
    [TestClass]
    public class TestsControllerTest
    {
        [TestMethod]
        public void TestControllerShowAction()
        {
            int Id = 1;
            TestModel testModel = new TestModel();
            Test test = testModel.test;

            ICollection<Question> questions = new List<Question>();
            questions.Add(testModel.question1);
            questions.Add(testModel.question2);
            questions.Add(testModel.question3);
            questions.Add(testModel.question4);
            test.Questions = questions;

            // Arrange
            Mock<ITestRepository> repository = new Mock<ITestRepository>();
            repository.Setup(x => x.GetTestById(Id)).Returns(test);

            TestsController controller = new TestsController(repository.Object);

            // Act
            ViewResult result = controller.Show(Id) as ViewResult;
            Test testFromView = (Test)result.ViewData.Model;

            // Assert
            Assert.AreEqual(1, testFromView.Id);
            Assert.AreEqual("Test Nr. 1", testFromView.Title);
            Assert.AreEqual(true, testFromView.IsAvaible);
            Assert.AreEqual(300, testFromView.AllQuestionsNumber);
            Assert.AreEqual(20, testFromView.OneTestQuestionsNumber);

            Assert.AreEqual(4, testFromView.Questions.Count);
        }
    }
}
