using Data;
using Logic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace LogicTest
{
    [TestClass]
    public class BallsAPITests
    {
        private LogicApi logicApi;

        public class TestData : DataAPI
        {
            public TestData() { }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            logicApi = LogicApi.CreateApi(800, 600, new TestData());
        }

        [TestMethod]
        public void TestCreateBall()
        {
            logicApi.CreateBall();
            Assert.AreEqual(1, logicApi.GetBallsNumber());


            int x = logicApi.GetX(0);
            int y = logicApi.GetY(0);
            int size = logicApi.GetSize(0);
            Assert.IsTrue(x >= 20 && x <= 780);
            Assert.IsTrue(y >= 20 && y <= 580);
            Assert.AreEqual(20, size);

        }
    }
}
