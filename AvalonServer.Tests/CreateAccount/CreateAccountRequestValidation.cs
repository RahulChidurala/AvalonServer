using System;
using AvalonServer.CreateAccount;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AvalonServer.Tests.CreatePlayer
{
    [TestClass]
    public class CreateAccountRequestValidation
    {
        ICreateAccountValidator sut = new CreateAccountValidator();
        String usernameException = CreateAccountValidationException.ValidationMessageFor(CreateAccountValidationExceptions.InvalidUsername);
        String passwordException = CreateAccountValidationException.ValidationMessageFor(CreateAccountValidationExceptions.InvalidPassword);
        
        [TestMethod]
        public void TestCreateAccount_WithValidUsernameAndPassword_ShouldNotThrow()
        {

            var request = new CreateAccountMessages.Request()
            {
                Username = "User12",
                Password = "Password1"
            };

            sut.Validate(request);
        }

        [TestMethod]
        public void TestCreateAccount_WithShortUsername_ShouldThrow()
        {

            var request = new CreateAccountMessages.Request()
            {
                Username = "User",
                Password = "Password1"
            };

            try
            {
                sut.Validate(request);
                Assert.Fail("Should have thrown invalid username exception!");

            } catch(CreateAccountValidationException ex)
            {
                Assert.AreEqual(ex.Message, CreateAccountValidationException.ValidationMessageFor(CreateAccountValidationExceptions.InvalidUsername));
            } catch (Exception ex)
            {
                Assert.Fail("Threw unexpected exception! " + ex.Message);
            }
            
        }

        [TestMethod]
        public void TestCreateAccount_WithEmptyPassword_ShouldThrow()
        {

            var request = new CreateAccountMessages.Request()
            {
                Username = "User123",
                Password = ""
            };

            try
            {
                sut.Validate(request);
                Assert.Fail("Should have thrown invalid password exception!");

            }
            catch (CreateAccountValidationException ex)
            {
                Assert.AreEqual(ex.Message, CreateAccountValidationException.ValidationMessageFor(CreateAccountValidationExceptions.InvalidPassword));
            }
            catch (Exception ex)
            {
                Assert.Fail("Threw unexpected exception! " + ex.Message);
            }
        }

        [TestMethod]
        public void TestCreateAccount_WithNullUsername_ShouldThrow()
        {

            var request = new CreateAccountMessages.Request()
            {
                Username = null,
                Password = ""
            };

            try
            {
                sut.Validate(request);
                Assert.Fail("Should have thrown invalid username exception!");

            }
            catch (CreateAccountValidationException ex)
            {
                Assert.AreEqual(ex.Message, CreateAccountValidationException.ValidationMessageFor(CreateAccountValidationExceptions.InvalidUsername));
            }
            catch (Exception ex)
            {
                Assert.Fail("Threw unexpected exception! " + ex.Message);
            }
        }

        [TestMethod]
        public void TestCreateAccount_WithNullPassword_ShouldThrow()
        {

            var request = new CreateAccountMessages.Request()
            {
                Username = "User123",
                Password = null
            };

            try
            {
                sut.Validate(request);
                Assert.Fail("Should have thrown invalid password exception!");

            }
            catch (CreateAccountValidationException ex)
            {
                Assert.AreEqual(ex.Message, CreateAccountValidationException.ValidationMessageFor(CreateAccountValidationExceptions.InvalidPassword));
            }
            catch (Exception ex)
            {
                Assert.Fail("Threw unexpected exception! " + ex.Message);
            }
        }
    }
}
