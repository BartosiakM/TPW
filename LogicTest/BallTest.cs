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
        private AbstractLogicAPI abstractLogicAPI;

        public class TestData : DataAPI
        {
            public TestData() { }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            abstractLogicAPI = AbstractLogicAPI.CreateApi(800, 600, new TestData());
        }

        [TestMethod]
        public void TestCreateBall()
        {
            abstractLogicAPI.CreateBall();
            Assert.AreEqual(1, abstractLogicAPI.GetBallsNumber());


            int x = abstractLogicAPI.GetX(0);
            int y = abstractLogicAPI.GetY(0);
            int size = abstractLogicAPI.GetSize(0);
            Assert.IsTrue(x >= 20 && x <= 780);
            Assert.IsTrue(y >= 20 && y <= 580);
            Assert.AreEqual(20, size);

        }

        [TestMethod]
        public void TestMoveBall()
        {

            abstractLogicAPI.CreateBall();
            int x0 = abstractLogicAPI.GetX(0);
            int y0 = abstractLogicAPI.GetY(0);


            abstractLogicAPI.Start();
            System.Threading.Thread.Sleep(100);
            abstractLogicAPI.Stop();

            int x1 = abstractLogicAPI.GetX(0);
            int y1 = abstractLogicAPI.GetY(0);
            Assert.AreNotEqual(x0, x1);
            Assert.AreNotEqual(y0, y1);
        }

        [TestMethod]
        public void TestBounds()
        {

            abstractLogicAPI.CreateBall();

            abstractLogicAPI.Start();
            System.Threading.Thread.Sleep(10000);
            abstractLogicAPI.Stop();

            int x = abstractLogicAPI.GetX(0);
            int y = abstractLogicAPI.GetY(0);
            int size = abstractLogicAPI.GetSize(0);

            Assert.IsTrue(x >= size && x <= abstractLogicAPI.BoardWidth - size);
            Assert.IsTrue(y >= size && y <= abstractLogicAPI.BoardHeight - size);
        }

    }
}
