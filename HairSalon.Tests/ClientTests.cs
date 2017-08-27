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

        [TestMethod]
        public void UpdateClient_ClientUpdatesNameAddressPhoneNumber_Client()
        {
            //Arrange
            Client testClient = new Client("Beverly Crusher", "876 Sick Bay", "206-777-1701", 5);

            //Act
            string newName = "Beverly Picard";
            string newAddress = "111 Bridge Way";
            string newPhoneNumber = "206-555-1701";
            Client expectedClient = new Client(newName, newAddress, newPhoneNumber, testClient.GetStylistId());

            testClient.UpdateClient(newName, newAddress, newPhoneNumber, testClient.GetStylistId());

            //Assert
            Assert.AreEqual(expectedClient, testClient);
        }

        [TestMethod]
        public void DeleteClient_DeleteSpecifiedClient()
        {
            //Arrange
            Client firstClient = new Client("Wesley Crusher", "1234 Enterprise Road", "206-555-1701", 1);
            firstClient.Save();
            Client secondClient = new Client("Beverly Crusher", "876 Sick Bay", "206-777-1701", 5);
            secondClient.Save();

            //List<Client> testClientList = new List<Client> {firstClient, secondClient};

            //List<Client> expectedClientList = new List<Client> {secondClient};

            //Act
            Client.DeleteClient(firstClient.GetId());

            bool expected = false;
            bool actual = Client.GetAll().Contains(firstClient);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        public void Dispose()
        {
            Stylist.DeleteAll();
            Client.DeleteAll();
        }
    }
}
