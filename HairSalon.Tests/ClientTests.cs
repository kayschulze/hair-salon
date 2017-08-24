using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class HairSalonTests : IDisposable
    {
        public HairSalonTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=hair_salon_test;";
        }

        [TestMethod]
        public void Equals_OverrideTrueForEntireClient_True()
        {
            //Arrange, Act
            Client firstClient = new Client("Wesley Crusher", "1234 Enterprise Road", "206-555-1701", 1);
            Client secondClient = new Client("Wesley Crusher", "1234 Enterprise Road", "206-555-1701", 1);

            //Assert
            Assert.AreEqual(firstClient, secondClient);
        }

        [TestMethod]
        public void Save_SaveClientToDatabase_ClientList()
        {
            //Arrange
            Client testClient = new Client("Wesley Crusher", "1234 Enterprise Road", "206-555-1701", 1);
            testClient.Save();

            //Act
            List<Client> resultList = Client.GetAll();
            List<Client> testList = new List<Client> {testClient};

            //Assert
            CollectionAssert.AreEqual(testList, resultList);
        }

        public void Dispose()
        {
            Stylist.DeleteAll();
            Client.DeleteAll();
        }
    }
}
