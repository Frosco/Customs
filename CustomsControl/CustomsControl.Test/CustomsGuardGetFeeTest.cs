﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomsControl;

namespace CustomsControl.Test
{
    [TestClass]
    public class CustomsGuardGetFeeTest
    {
        [TestMethod]
        public void Case1_5_Over1000()
        {
            // arrange
            var car = new Vehicle(1100, vehicleType.car, false);
            //act
            var actual = CustomsGuard.GetFee(car, new DateTime(2016, 12, 19, 15, 0, 0));
            // assert
            var expected = 1000;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Case2_5_Under1000()
        {
            // arrange
            var car = new Vehicle(999, vehicleType.car, false);
            //act
            var actual = CustomsGuard.GetFee(car, new DateTime(2016, 12, 19, 15, 0, 0));
            // assert
            var expected = 500;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Case3_Night1()
        {
            // arrange
            var car = new Vehicle(900, vehicleType.car, false);
            //act
            var actual = CustomsGuard.GetFee(car, new DateTime(2016, 12, 19, 18, 0, 0));
            // assert
            var expected = 250;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Case3_Night2()
        {
            // arrange
            var car = new Vehicle(900, vehicleType.car, false);
            //act
            var actual = CustomsGuard.GetFee(car, new DateTime(2016, 12, 19, 5, 59, 59));
            // assert
            var expected = 250;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Case4_Truck()
        {
            // arrange
            var car = new Vehicle(5000, vehicleType.truck, false);
            //act
            var actual = CustomsGuard.GetFee(car, new DateTime(2016, 12, 19, 15, 0, 0));
            // assert
            var expected = 2000;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Case6_Motorcycle()
        {
            // arrange
            var car = new Vehicle(1000, vehicleType.motorcycle, false);
            //act
            var actual = CustomsGuard.GetFee(car, new DateTime(2016, 12, 19, 15, 0, 0));
            // assert
            var expected = 700;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Case7_Weekend1()
        {
            // arrange
            var car = new Vehicle(1000, vehicleType.motorcycle, false);
            //act
            var actual = CustomsGuard.GetFee(car, new DateTime(2016, 12, 17, 15, 0, 0));
            // assert
            var expected = 1400;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Case7_Weekend2()
        {
            // arrange
            var car = new Vehicle(1000, vehicleType.motorcycle, false);
            //act
            var actual = CustomsGuard.GetFee(car, new DateTime(2016, 12, 17, 19, 0, 0));
            // assert
            var expected = 1400;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Case7_NationalDay()
        {
            // arrange
            var car = new Vehicle(1000, vehicleType.motorcycle, false);
            //act
            var actual = CustomsGuard.GetFee(car, new DateTime(2016, 6, 6, 19, 0, 0));
            // assert
            var expected = 1400;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Case7_Easter2016()
        {
            // arrange
            var car = new Vehicle(1000, vehicleType.motorcycle, false);
            //act
            var actual = CustomsGuard.GetFee(car, new DateTime(2016, 3, 28, 19, 0, 0));
            // assert
            var expected = 1400;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Case7_Easter2020()
        {
            // arrange
            var car = new Vehicle(1000, vehicleType.motorcycle, false);
            //act
            var actual = CustomsGuard.GetFee(car, new DateTime(2020, 4, 13, 19, 0, 0));
            // assert
            var expected = 1400;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Case8_Environmental()
        {
            // arrange
            var car = new Vehicle(1000, vehicleType.motorcycle, true);
            //act
            var actual = CustomsGuard.GetFee(car, new DateTime(2020, 4, 13, 19, 0, 0));
            // assert
            var expected = 0;
            Assert.AreEqual(expected, actual);
        }

    }
}