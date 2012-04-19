using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class ChangeEmailSpellingHandlerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheHandleMethod
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenCommandArgIsNull()
            {
                ArgumentNullException exception = null;
                var handler = new ChangeEmailSpellingHandler(null, null);
                try
                {
                    handler.Handle(null);
                }
                catch (ArgumentNullException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                exception.ParamName.ShouldEqual("command");
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void ExecutesQuery_ToGetEmailAddress_ByUserNameAndNumber()
            {
                var command = new ChangeEmailSpellingCommand();
                Expression<Func<GetEmailAddressByUserNameAndNumberQuery, bool>> emailAddressQueryFromCommand = q =>
                    q.UserName == command.UserName && q.Number == command.Number;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(emailAddressQueryFromCommand)))
                    .Returns(null as EmailAddress);
                var handler = new ChangeEmailSpellingHandler(queryProcessor.Object, null);

                handler.Handle(command);

                queryProcessor.Verify(m => m.Execute(It.Is(emailAddressQueryFromCommand)), Times.Once());
            }

            [TestMethod]
            public void DoesNotUpdate_WhenEmailAddressIsNull()
            {
                var command = new ChangeEmailSpellingCommand();
                Expression<Func<GetEmailAddressByUserNameAndNumberQuery, bool>> emailAddressQueryFromCommand = q =>
                    q.UserName == command.UserName && q.Number == command.Number;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(emailAddressQueryFromCommand)))
                    .Returns(null as EmailAddress);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                entities.Setup(m => m.Update(It.IsAny<EmailAddress>()));
                var handler = new ChangeEmailSpellingHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                entities.Verify(m => m.Update(It.IsAny<EmailAddress>()), Times.Never());
                command.ChangedState.ShouldEqual(false);
            }

            [TestMethod]
            public void DoesNotUpdate_WhenNewValue_IsSameAsOldSpelling()
            {
                const string value = "user@domain.tld";
                var command = new ChangeEmailSpellingCommand { NewValue = value };
                Expression<Func<GetEmailAddressByUserNameAndNumberQuery, bool>> emailAddressQueryFromCommand = q =>
                    q.UserName == command.UserName && q.Number == command.Number;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(emailAddressQueryFromCommand)))
                    .Returns(new EmailAddress { Value = value });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                entities.Setup(m => m.Update(It.IsAny<EmailAddress>()));
                var handler = new ChangeEmailSpellingHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                entities.Verify(m => m.Update(It.IsAny<EmailAddress>()), Times.Never());
                command.ChangedState.ShouldEqual(false);
            }

            [TestMethod]
            public void ChangesEmailSpelling_WhenNewValue_IsDifferentFromOldSpelling()
            {
                const string newValue = "User@Domain.Tld";
                const string oldValue = "user@domain.tld";
                EmailAddress updatedEntity = null;
                var command = new ChangeEmailSpellingCommand { NewValue = newValue };
                Expression<Func<GetEmailAddressByUserNameAndNumberQuery, bool>> emailAddressQueryFromCommand = q =>
                    q.UserName == command.UserName && q.Number == command.Number;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(emailAddressQueryFromCommand)))
                    .Returns(new EmailAddress { Value = oldValue });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                entities.Setup(m => m.Update(It.Is<EmailAddress>(a => a.Value == newValue)))
                    .Callback((Entity entity) => updatedEntity = (EmailAddress)entity);
                var handler = new ChangeEmailSpellingHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                entities.Verify(m => m.Update(It.Is<EmailAddress>(a => a.Value == newValue)), Times.Once());
                command.ChangedState.ShouldEqual(true);
                updatedEntity.Value.ShouldEqual(newValue);
            }
        }
    }
}
