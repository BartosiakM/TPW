using System.Numerics;
using Data;
using Logic;

namespace LogicTest;

internal class FakeDataApi : DataAPI
{
    public override Vector2 Position => throw new NotImplementedException();

    public override Vector2 Velocity
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public override int Radius => throw new NotImplementedException();

    public override bool IsStopped
    {
        set { }
    }

    public override IDisposable Subscribe(IObserver<DataAPI> observer)
    {
        throw new NotImplementedException();
    }
}

[TestClass]
public class TableTest
{
    [TestMethod]
    public void CreateApiTest()
    {
        var fakeDataApis = new List<FakeDataApi>();
        var balls = fakeDataApis.OfType<DataAPI>().ToList();
        var table = AbstractLogicAPI.CreateApi(1, 1, balls);
        Assert.IsNotNull(table);
    }

    [TestMethod]
    public void TestResetTable()
    {
        var fakeDataApis = new List<FakeDataApi>();
        var data1 = new FakeDataApi();
        var data2 = new FakeDataApi();
        var data3 = new FakeDataApi();
        fakeDataApis.Add(data1);
        fakeDataApis.Add(data2);
        fakeDataApis.Add(data3);
        var balls = fakeDataApis.OfType<DataAPI>().ToList();
        var table = AbstractLogicAPI.CreateApi(1, 1, balls);
        table.ResetTable();
        Assert.AreEqual(0, table.GetBallPositions().Count);
    }
}