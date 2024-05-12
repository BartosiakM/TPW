using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using System.Numerics;

namespace DataTests
{
    [TestClass]
    public class DataTests
    {
        private BallAPI ball;

        [TestInitialize]
        public void Setup()
        {
            Vector2 position = new Vector2(2, 2);
            int deltaX = 1;
            int deltaY = 1;
            int size = 10;
            int mass = 5;
            bool isSimRunning = false;
            ball = BallAPI.CreateBallAPI(position, deltaX,deltaY,size,mass, isSimRunning);
        }

        [TestMethod]
        public void BallAPI_CreateBallAPITest()
        {
            Assert.IsNotNull(ball);
            Assert.IsInstanceOfType(ball, typeof(BallAPI));
        }

        [TestMethod]
        public void BallAPI_PostionTest()
        {
            Vector2 position = new Vector2(2, 2);

            var excpectedPosition = ball.Position;

            Assert.AreEqual(excpectedPosition, position);
        }

        [TestMethod]
        public void BallAPI_GetX()
        {
            int x = ball.X;

            Assert.AreEqual(2, x);
        }

        [TestMethod]
        public void BallAPI_GetY()
        {
            int y = ball.Y;

            Assert.AreEqual(2, y);
        }

        [TestMethod]
        public void BallAPI_GetDiameter()
        {

            int diameter = ball.Diameter;


            Assert.AreEqual(20, diameter);
        }

        [TestMethod]
        public void BallAPI_GetMass()
        {

            int mass = ball.Mass;

            Assert.AreEqual(5, mass);
        }

        [TestMethod]
        public void BallAPI_GetSize()
        {

            int size = ball.Size;


            Assert.AreEqual(10, size);
        }

        [TestMethod]
        public void BallAPI_SetVelocity()
        {
            int newDeltaX = 10;
            int newDeltaY = 10;

            ball.setVelocity(newDeltaX, newDeltaY);

            Assert.AreEqual(10, ball.Vx);
            Assert.AreEqual(10, ball.Vy);
        }

        [TestMethod]
        public void BallAPI_IsSimRunning_SetValue()
        {

            bool newValue = true;


            ball.isSimRunning = newValue;


            Assert.IsTrue(ball.isSimRunning);
        }

        [TestMethod]
        public void BallAPI_IsSimRunning_GetValue()
        {

            Assert.IsFalse(ball.isSimRunning);
        }

        [TestMethod]
        public void BallAPI_VxGetter()
        {
            int expectedVx = 1;

            int actualVx = ball.Vx;

            Assert.AreEqual(expectedVx, actualVx);
        }


        [TestMethod]
        public void BallAPI_VyGetter()
        {
            int expectedVy = 1;

            int actualVy = ball.Vy;

            Assert.AreEqual(expectedVy, actualVy);
        }
    }
    [TestClass]
    public class DataAPITest
    {
        private DataAPI data;

        [TestInitialize]
        public void Setup()
        {
            data = DataAPI.CreateDataAPI();
        }

        [TestMethod]
        public void DataAPI_getBoardWidth()
        {
            int expectedValue = data.getBoardWidth();

            Assert.AreEqual(expectedValue, 315);
        }
        [TestMethod]
        public void DataAPI_getBoardHeight()
        {
            int expectedValue = data.getBoardHeight();

            Assert.AreEqual(expectedValue, 150);
        }
    }
}
