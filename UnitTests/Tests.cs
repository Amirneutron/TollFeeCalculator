using System;
using NUnit.Framework;
using TollFeeCalculator;

namespace UnitTests
{
    [TestFixture]
    public class Tests
    {
        
        [Test]
        public void GetTollFeeForHoliday()
        {
            DateTime[] date = { new DateTime(2021, 05, 01, 06, 50, 0) };
            TollCalculator toll = new TollCalculator();
            int actual = toll.GetTollFee(new Car(), date);
            Assert.AreEqual(0, actual);
        }
        
       

        [Test]
        public void GetTollFeeForMotorbike()
        {
            DateTime[] date = { new DateTime(2021, 08, 17, 6, 25, 0), new DateTime(2021, 08, 17, 7, 40, 0) };
            TollCalculator toll = new TollCalculator();
            int actual = toll.GetTollFee(new Motorbike(), date);
            Assert.AreEqual(0, actual);
        }
        
        [Test]
        public void TollFeeForTollFreeDateTime()
        {
            DateTime[] date = { new DateTime(2021, 02, 17, 20, 15, 0), new DateTime(2021, 02, 17, 21, 40, 0) };
            TollCalculator toll = new TollCalculator();
            int actual = toll.GetTollFee(new Car(), date);
            Assert.AreEqual(0, actual);
        }
        
        [Test]
        public void GetTollFeeForEmergency()
        {
            DateTime[] date = { new DateTime(2021, 08, 17, 6, 25, 0), new DateTime(2021, 08, 17, 7, 40, 0), new DateTime(2021, 08, 17, 8, 55, 0), new DateTime(2021, 08, 17, 15, 40, 0), new DateTime(2021, 08, 17, 17, 40, 0) };
            TollCalculator toll = new TollCalculator();
            int actual = toll.GetTollFee(new Emergency(), date);
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void GetMaxTollFeeForCar()
        {
            DateTime[] date =
            {
                new DateTime(2021, 08, 21, 6, 25, 0), 
                new DateTime(2021, 08, 21, 6, 25, 0), 
                new DateTime(2021, 08, 17, 7, 40, 0), 
                new DateTime(2021, 08, 17, 15, 40, 0), 
                new DateTime(2021, 08, 17, 15, 55, 0)
            };

            TollCalculator toll = new TollCalculator();
            int actual = toll.GetTollFee(new Car(), date);
            Assert.AreEqual(60, actual);
        }

        

        [Test]
        public void TollFeeForTollFreeDate()
        {
            DateTime[] date = { new DateTime(2021, 12, 5, 19, 25, 0) };
            TollCalculator toll = new TollCalculator();
            int actual = toll.GetTollFee(new Car(), date);
            Assert.AreEqual(0, actual);
        }
        
        [Test]
        public void GetTollFeeCar()
        {
           
            DateTime[] date = { new DateTime(2021, 08, 17, 6, 25, 0), new DateTime(2021, 08, 17, 7, 40, 0), new DateTime(2021, 08, 17, 8, 55, 0), new DateTime(2021, 08, 17, 15, 40, 0), new DateTime(2021, 08, 17, 17, 40, 0) };
            TollCalculator toll = new TollCalculator();
            int actual = toll.GetTollFee(new Car(), date);
            Assert.AreEqual(42, actual);
        }

        
    }
}