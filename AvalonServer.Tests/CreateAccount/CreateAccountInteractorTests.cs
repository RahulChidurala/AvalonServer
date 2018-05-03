using System;
using AvalonServer.CreateAccount;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using AvalonServer.Entities;
using AvalonServer.Tests.Createplayer;

namespace AvalonServer.Tests.CreateAccount
{
    [TestClass]
    public class CreateAccountInteractorTests
    {
        ICreateAccountInteractor Sut;
        AccountGatewaySpy AccountGateway;
        PlayerGatewaySpy PlayerGateway;

        [TestInitialize]
        public void Setup()
        {
            var validator = new CreateAccountValidator();
            AccountGateway = new AccountGatewaySpy();
            PlayerGateway = new PlayerGatewaySpy();
            Sut = new CreateAccountInteractor(validator, AccountGateway, PlayerGateway);
        }

        #region Validation tests
        [TestMethod]
        public void TestCreateAccount_WithEmptyUsername_ShouldRespondWithValidationError()
        {
            var request = new CreateAccountMessages.Request()
            {
                Username = "",
                Password = "password"
            };

            var response = Sut.Handle(request);
            Assert.IsFalse(response.Success, "Should not be successful!");
            Assert.AreEqual(response.Exception.Message, CreateAccountValidationException.ValidationMessageFor(CreateAccountValidationExceptions.InvalidUsername));
        }

        [TestMethod]
        public void TestCreateAccount_WithEmptyPassword_ShouldRespondWithValidationError()
        {
            var request = new CreateAccountMessages.Request()
            {
                Username = "User123",
                Password = null
            };

            var response = Sut.Handle(request);
            Assert.IsFalse(response.Success, "Should not be successful!");
            Assert.AreEqual(response.Exception.Message, CreateAccountValidationException.ValidationMessageFor(CreateAccountValidationExceptions.InvalidPassword));
        }

        [TestMethod]
        public void TestCreateAccount_WithValidRequest_ShouldRespondSuccess()
        {
            var request = new CreateAccountMessages.Request()
            {
                Username = "User123",
                Password = "password"
            };

            var response = Sut.Handle(request);
            Assert.IsTrue(response.Success, "Should be successful!");            
        }
        #endregion

        #region Creates account in gateway
        [TestMethod]
        public void TestCreateAccount_WithValidRequest_ShouldRespondSuccessAndSaveToGateway()
        {
            var request = new CreateAccountMessages.Request()
            {
                Username = "User123",
                Password = "password"
            };
            var response = Sut.Handle(request);

            Assert.AreEqual(AccountGateway.repo.Count, 1, "Should have 1 account in repo, but have " + AccountGateway.repo.Count);                        
            Assert.AreEqual(AccountGateway.repo.Values.ElementAt(0).Username, request.Username);
        }

        [TestMethod]
        public void TestCreateAccount_WithExistingUsername_ShouldRespondCreateAccountExceptionError()
        {
            var request = new CreateAccountMessages.Request()
            {
                Username = "User123",
                Password = "password"
            };
            Sut.Handle(request);
            
            var response = Sut.Handle(request);

            Assert.IsFalse(response.Success, "Should have failed!");
            Assert.AreEqual(response.Exception.Message, CreateAccountException.GetMessage(CreateAccountExceptions.UsernameAlreadyExists, request));
            Assert.AreEqual(AccountGateway.repo.Count, 1, "Should have 1 account in repo, but have " + AccountGateway.repo.Count);
        }


        //[TestMethod]
        //public void TestCreateAccount_WithExistingUsername_ShouldRespondWithExistingUsernameError()
        //{
        //    var request = new CreateAccountMessages.Request()
        //    {
        //        Username = "User123",
        //        Password = null
        //    };

        //    var response = Sut.Handle(request);
        //    Assert.IsFalse(response.Success, "Should not be successful!");
        //    Assert.AreEqual(response.Exception.Message, CreateAccountValidationException.ValidationMessageFor(CreateAccountValidationExceptions.InvalidPassword));
        //}

        #endregion

        #region Create associated Player tests
        [TestMethod]
        public void TestCreateAccount_WithValidRequest_ShouldCreateAssociatedPlayer()
        {
            var request = new CreateAccountMessages.Request()
            {
                Username = "User123",
                Password = "password"
            };
            var response = Sut.Handle(request);

            Assert.AreEqual(PlayerGateway.repo.Count, 1, "Should have 1 account in repo, but have " + PlayerGateway.repo.Count);
            Assert.AreEqual(PlayerGateway.repo.Values.ElementAt(0).AccountId, AccountGateway.repo.Values.ElementAt(0).Id, "Player's AccountId does not match IAccount.");
        }

        [TestMethod]
        public void TestCreateAccount_With2ValidRequest_ShouldCreate2AssociatedPlayers()
        {
            var request = new CreateAccountMessages.Request()
            {
                Username = "User123",
                Password = "password"
            };
            var request2 = new CreateAccountMessages.Request()
            {
                Username = "User1234",
                Password = "password"
            };
            Sut.Handle(request);
            Sut.Handle(request2);

            Assert.AreEqual(PlayerGateway.repo.Count, 2, "Should have 2 account in repo, but have " + PlayerGateway.repo.Count);
            Assert.AreEqual(PlayerGateway.repo.Values.ElementAt(0).AccountId, AccountGateway.repo.Values.ElementAt(0).Id, "Player's AccountId does not match IAccount.");
            Assert.AreEqual(PlayerGateway.repo.Values.ElementAt(1).AccountId, AccountGateway.repo.Values.ElementAt(1).Id, "Player's AccountId does not match IAccount.");
        }
        #endregion
    }
}
