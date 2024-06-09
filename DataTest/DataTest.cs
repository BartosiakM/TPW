using System.Numerics;
using Data;

namespace DataTest;

[TestClass]
public class BallTest
{
    [TestMethod]
    public void CreateApiTest()
    {
        var dataApi = DataAPI.CreateBall(new Vector2(1, 1), 1, 1, new Random(),1);
        Assert.IsNotNull(dataApi);
    }
}