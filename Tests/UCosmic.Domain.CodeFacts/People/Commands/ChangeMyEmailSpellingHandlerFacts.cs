using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class ChangeMyEmailSpellingHandlerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheHandleMethod
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenCommandArgIsNull()
            {
                ArgumentNullException exception = null;
                var handler = new ChangeMyEmailSpellingHandler(null, null);
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
                var command = new ChangeMyEmailSpellingCommand();
                Expression<Func<GetMyEmailAddressByNumberQuery, bool>> emailAddressQueryFromCommand = q =>
                    q.Principal == command.Principal && q.Number == command.Number;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(emailAddressQueryFromCommand)))
                    .Returns(null as EmailAddress);
                var handler = new ChangeMyEmailSpellingHandler(queryProcessor.Object, null);

                handler.Handle(command);

                queryProcessor.Verify(m => m.Execute(It.Is(emailAddressQueryFromCommand)), Times.Once());
            }

            [TestMethod]
            public void DoesNotUpdate_WhenEmailAddressIsNull()
            {
                var command = new ChangeMyEmailSpellingCommand();
                Expression<Func<GetMyEmailAddressByNumberQuery, bool>> emailAddressQueryFromCommand = q =>
                    q.Principal == command.Principal && q.Number == command.Number;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(emailAddressQueryFromCommand)))
                    .Returns(null as EmailAddress);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                entities.Setup(m => m.Update(It.IsAny<EmailAddress>()));
                var handler = new ChangeMyEmailSpellingHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                entities.Verify(m => m.Update(It.IsAny<EmailAddress>()), Times.Never());
                command.ChangedState.ShouldEqual(false);
            }

            [TestMethod]
            public void DoesNotUpdate_WhenNewValue_IsSameAsOldSpelling()
            {
                const string value = "user@domain.tld";
                var command = new ChangeMyEmailSpellingCommand { NewValue = value };
                Expression<Func<GetMyEmailAddressByNumberQuery, bool>> emailAddressQueryFromCommand = q =>
                    q.Principal == command.Principal && q.Number == command.Number;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(emailAddressQueryFromCommand)))
                    .Returns(new EmailAddress { Value = value });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                entities.Setup(m => m.Update(It.IsAny<EmailAddress>()));
                var handler = new ChangeMyEmailSpellingHandler(queryProcessor.Object, entities.Object);

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
                var command = new ChangeMyEmailSpellingCommand { NewValue = newValue };
                Expression<Func<GetMyEmailAddressByNumberQuery, bool>> emailAddressQueryFromCommand = q =>
                    q.Principal == command.Principal && q.Number == command.Number;
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(emailAddressQueryFromCommand)))
                    .Returns(new EmailAddress { Value = oldValue });
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                entities.Setup(m => m.Update(It.Is<EmailAddress>(a => a.Value == newValue)))
                    .Callback((Entity entity) => updatedEntity = (EmailAddress)entity);
                var handler = new ChangeMyEmailSpellingHandler(queryProcessor.Object, entities.Object);

                handler.Handle(command);

                entities.Verify(m => m.Update(It.Is<EmailAddress>(a => a.Value == newValue)), Times.Once());
                command.ChangedState.ShouldEqual(true);
                updatedEntity.Value.ShouldEqual(newValue);
            }
        }
    }
}
