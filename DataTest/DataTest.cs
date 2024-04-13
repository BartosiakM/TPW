using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;

namespace DataTest
{
    [TestClass]
    public class DataTest
    {
        [TestMethod]
        public void DataFactoryTest()
        {
            DataApi dataApi = DataApi.dataFactory();
            Assert.IsNotNull(dataApi);
        }

        [TestMethod]
        public void DataVariablesTest()
        {
            DataApi dataApi = DataApi.dataFactory();

            Assert.IsTrue(dataApi.windowHeight > 0);
            Assert.IsTrue(dataApi.windowWidth > 0);
            Assert.IsTrue(dataApi.ballSize > 0);
        }
    }
}
