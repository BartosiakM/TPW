using Data;
using Logic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace LogicTest
{
    [TestClass]
    public class BallsAPITests
    {
        [TestClass]
        public class LogicAPITests
        {
            private AbstractLogicAPI ballsApi;

            public class TestData : DataAPI
            {
                private int _boardWidth;
                private int _boardHeight;
                public TestData(int boardWidth, int boardHeight)
                {
                    _boardHeight = boardHeight;
                    _boardWidth = boardWidth;
                }

                public override BallAPI createBall(bool isSimRunning)
                {
                    Random random = new Random();
                    int x = random.Next(20, _boardWidth - 20);
                    int y = random.Next(20, _boardHeight - 20);
                    int valueX = random.Next(-3, 4);
                    int valueY = random.Next(-3, 4);
                    Vector2 position = new Vector2((int)x, (int)y);

                    if (valueX == 0)
                    {
                        valueX = random.Next(1, 3) * 2 - 3;
                    }
                    if (valueY == 0)
                    {
                        valueY = random.Next(1, 3) * 2 - 3;
                    }

                    int Vx = valueX;
                    int Vy = valueY;
                    int radius = 20;
                    int mass = 200;
                    return BallAPI.CreateBallAPI(position, Vx, Vy, radius, mass, isSimRunning);
                }
                public override int getBoardHeight()
                {
                    return _boardHeight;
                }

                public override int getBoardWidth()
                {
                    return _boardWidth;
                }
            }

            [TestInitialize]
            public void TestInitialize()
            {
                ballsApi = AbstractLogicAPI.CreateApi(null);
            }

            [TestMethod]
            public void TestCreateBall()
            {
                ballsApi.CreateBall();
                ballsApi.Stop();
                Assert.AreEqual(1, ballsApi.GetBallsNumber());

                int x = ballsApi.GetX(0);
                int y = ballsApi.GetY(0);
                int size = ballsApi.GetSize(0);
                Assert.IsTrue(x >= 0);
                Assert.IsTrue(x <= 315);
                Assert.IsTrue(y >= 0);
                Assert.IsTrue(y <= 150);
                Assert.AreEqual(20, size);
            }


            [TestMethod]
            public void CreateBall_AddsNewBallToList()
            {


                ballsApi.CreateBall();


                Assert.IsNotNull(ballsApi.balls);
                Assert.AreEqual(1, ballsApi.GetBallsNumber());
            }

            [TestMethod]
            public void Test_GetAllBalls()
            {
                int expectedCount = 1;
                ballsApi.CreateBall();


                List<BallAPI> balls = ballsApi.balls;


                Assert.AreEqual(expectedCount, balls.Count);
            }

        }
    }
}
