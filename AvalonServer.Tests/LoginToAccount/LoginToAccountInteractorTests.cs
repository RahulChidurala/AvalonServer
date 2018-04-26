using System;
using AvalonServer.Entities;
using AvalonServer.LoginToAccount;
using AvalonServer.Tests.CreateAccount;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AvalonServer.Tests.LoginToAccount
{
    [TestClass]
    public class LoginToAccountInteractorTests
    {

        ILoginValidator Validator = new LoginValidator();
        AccountGatewaySpy AccountGateway = new AccountGatewaySpy();

        ILoginInteractor Sut;

        [TestInitialize]
        public void Setup()
        {
            Sut = new LoginInteractor(Validator, AccountGateway);
        }

        #region Login Request Validation tests
        [TestMethod]
        public void TestLogin_WithInvalidUsernameRequest_ShouldThrowValidationError()
        {

            var request = new LoginMessages.Request()
            {
                Username = "",
                Password = "password"
            };

            try
            {
                Validator.Validate(request);
                Assert.Fail("Should have thrown an exception!");

            } catch(LoginValidationException ex)
            {
                Assert.AreEqual(ex.Message, LoginValidationException.ValidationMessageFor(LoginValidationExceptions.InvalidUsername));
            }
        }

        [TestMethod]
        public void TestLogin_WithInvalidPasswordRequest_ShouldThrowValidationError()
        {

            var request = new LoginMessages.Request()
            {
                Username = "User",
                Password = ""
            };

            try
            {
                Validator.Validate(request);
                Assert.Fail("Should have thrown an exception!");

            }
            catch (LoginValidationException ex)
            {
                Assert.AreEqual(ex.Message, LoginValidationException.ValidationMessageFor(LoginValidationExceptions.InvalidPassword));
            }
        }
        #endregion

        #region Login Request Validation Interactor response
        [TestMethod]
        public void TestLoginInteractorValidation_WithEmptyUsername_ShouldRespondFailWithUsernameError()
        {
            var request = new LoginMessages.Request()
            {
                Username = "",
                Password = "password"
            };

            var response = Sut.Handle(request);

            Assert.IsFalse(response.Success, "Should have failed!");
            Assert.AreEqual(response.Exception.Message, LoginValidationException.ValidationMessageFor(LoginValidationExceptions.InvalidUsername), "Unexpected message!");
        }

        [TestMethod]
        public void TestLoginInteractorValidation_WithEmptyPassword_ShouldRespondFailWithPasswordError()
        {
            var request = new LoginMessages.Request()
            {
                Username = "User",
                Password = null
            };

            var response = Sut.Handle(request);

            Assert.IsFalse(response.Success, "Should have failed!");
            Assert.AreEqual(response.Exception.Message, LoginValidationException.ValidationMessageFor(LoginValidationExceptions.InvalidPassword), "Unexpected message!");
        }
        #endregion

        #region Login to Account
        [TestMethod]
        public void TestLogin_WithRequestWithNoAccount_ShouldFail()
        {
            var request = new LoginMessages.Request()
            {
                Username = "User123",
                Password = "password"
            };

            var response = Sut.Handle(request);

            Assert.IsFalse(response.Success, "Should have failed!");
            Assert.AreEqual(response.Exception.Message, LoginException.GetMessageFor(LoginExceptions.IncorrectCredentials), "Unexpected error message");
        }

        [TestMethod]
        public void TestLogin_WithRequestWithValidAccount_ShouldSucceed()
        {
            var request = new LoginMessages.Request()
            {
                Username = "User123",
                Password = "password"
            };
            var account = new Account()
            {
                Id = 0,
                PlayerId = 0,
                Username = request.Username,
                Password = request.Password,
            };
            AccountGateway.CreateAccount(account);            

            var response = Sut.Handle(request);

            Assert.IsTrue(response.Success, "Should have succeeded!");           
        }
        #endregion
    }
}
