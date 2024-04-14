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
    [TestFixture]
    public class BallTests
    {
        [Test]
        public void Ball_PositionChangesAfterMovement()
        {
            var ball = new Ball();
            var initialX = ball.X;
            var initialY = ball.Y;
            var width = 800;
            var height = 600;

            ball.ChangingPosition(height, width);

            Assert.AreNotEqual(initialX, ball.X);
            Assert.AreNotEqual(initialY, ball.Y);
        }

        [Test]
        public void Ball_ReversesVelocityWhenReachingBoundaries()
        {
            var ball = new Ball
            {
                X = 790,
                Y = 300,
                VelocityX = 2,
                VelocityY = 0,
                Radius = 10
            };
            var width = 800;
            var height = 600;

            ball.ChangingPosition(height, width);

            // Assert
            Assert.AreEqual(-2, ball.VelocityX); // Velocity should reverse after hitting right boundary
        }

        [Test]
        public void Ball_DoesNotReverseVelocityInsideBoundaries()
        {
            // Arrange
            var ball = new Ball
            {
                X = 100,
                Y = 300,
                VelocityX = 2,
                VelocityY = 0,
                Radius = 10
            };
            var width = 800;
            var height = 600;

            ball.ChangingPosition(height, width);

            Assert.AreEqual(2, ball.VelocityX);
        }
    }
}
