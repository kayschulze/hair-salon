using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylistTests : IDisposable
    {
        public StylistTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=hair_salon_test;";
        }

        [TestMethod]
        public void Save_SaveStylistToDatabase_StylistList()
        {
            //Arrange
            Stylist testStylist = new Stylist("Yvonne Renwick");
            testStylist.Save();

            //Act
            List<Stylist> resultList = Stylist.GetAll();
            List<Stylist> testList = new List<Stylist> {testStylist};

            //Assert
            CollectionAssert.AreEqual(testList, resultList);
        }

        [TestMethod]
        public void Find_FindsStylistInDatabase_Stylist()
        {
            //Arrange
            Stylist testStylist = new Stylist("Yvonne Renwick");
            testStylist.Save();

            //Act
            Stylist foundStylist = Stylist.Find(testStylist.GetId());

            //Assert
            Assert.AreEqual(testStylist, foundStylist);
        }

        public void Dispose()
        {
            Client.DeleteAll();
            Stylist.DeleteAll();
        }
    }
}
