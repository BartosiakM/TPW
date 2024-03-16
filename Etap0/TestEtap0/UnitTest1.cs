using Etap0;

namespace TestEtap0
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void poprawnaSuma()
        {
            Kalkulator kalkulator = new Kalkulator();
            int wynik = kalkulator.suma(1, 2);
            Assert.AreEqual(3, wynik);
        }

        [TestMethod]
        public void niepoprawnaSuma()
        {
            Kalkulator kalkulator = new Kalkulator();
            int wynik = kalkulator.suma(1, 2);
            Assert.AreNotEqual(4, wynik);
        }

        [TestMethod]
        public void poprawnaRoznica()
        {
            Kalkulator kalkulator = new Kalkulator();
            int wynik = kalkulator.roznica(1, 2);
            Assert.AreEqual(-1, wynik);
        }

        [TestMethod]
        public void niepoprawnaRoznica()
        {
            Kalkulator kalkulator = new Kalkulator();
            int wynik = kalkulator.roznica(1, 2);
            Assert.AreNotEqual(-2, wynik);
        }
    }
}