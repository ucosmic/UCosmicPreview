using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    public static class UpdateMyEmailValueHandlerFacts
    {
        [TestClass]
        public class TheHandleMethod
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenCommandArgIsNull()
            {
                ArgumentNullException exception = null;
                var handler = new UpdateMyEmailValueHandler(null);
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
                var princial = "".AsPrincipal();
                var command = new UpdateMyEmailValueCommand { Principal = princial, };
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get<EmailAddress>()).Returns(new EmailAddress[] { }.AsQueryable);
                var handler = new UpdateMyEmailValueHandler(entities.Object);

                handler.Handle(command);

                //queryProcessor.Verify(m => m.Execute(It.Is(emailAddressQueryFromCommand)), Times.Once());
                entities.Verify(m => m.Get<EmailAddress>(), Times.Once());
            }

            [TestMethod]
            public void DoesNotUpdate_WhenEmailAddressIsNull()
            {
                var princial = "".AsPrincipal();
                var command = new UpdateMyEmailValueCommand { Principal = princial, };
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get<EmailAddress>()).Returns(new EmailAddress[] { }.AsQueryable);
                entities.Setup(m => m.Update(It.IsAny<EmailAddress>()));
                var handler = new UpdateMyEmailValueHandler(entities.Object);

                handler.Handle(command);

                entities.Verify(m => m.Update(It.IsAny<EmailAddress>()), Times.Never());
                command.ChangedState.ShouldEqual(false);
            }

            [TestMethod]
            public void DoesNotUpdate_WhenNewValue_IsSameAsOldSpelling()
            {
                var princial = "".AsPrincipal();
                const string value = "user@domain.tld";
                var command = new UpdateMyEmailValueCommand { NewValue = value, Principal = princial, };
                var emailAddress = new EmailAddress
                {
                    Value = value,
                    Person = new Person { User = new User { Name = princial.Identity.Name } },
                };
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get<EmailAddress>()).Returns(new[] { emailAddress }.AsQueryable);
                entities.Setup(m => m.Update(It.IsAny<EmailAddress>()));
                var handler = new UpdateMyEmailValueHandler(entities.Object);

                handler.Handle(command);

                entities.Verify(m => m.Update(It.IsAny<EmailAddress>()), Times.Never());
                command.ChangedState.ShouldEqual(false);
            }

            [TestMethod]
            public void ChangesEmailSpelling_WhenNewValue_IsDifferentFromOldSpelling()
            {
                const string newValue = "User@Domain.Tld";
                const string oldValue = "user@domain.tld";
                var princial = "".AsPrincipal();
                var emailAddress = new EmailAddress
                {
                    Value = oldValue,
                    Person = new Person { User = new User { Name = princial.Identity.Name, } }
                };
                EmailAddress updatedEntity = null;
                var command = new UpdateMyEmailValueCommand { NewValue = newValue, Principal = princial, };
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get<EmailAddress>()).Returns(new[] { emailAddress }.AsQueryable);
                entities.Setup(m => m.Update(It.Is<EmailAddress>(a => a.Value == newValue)))
                    .Callback((Entity entity) => updatedEntity = (EmailAddress)entity);
                var handler = new UpdateMyEmailValueHandler(entities.Object);

                handler.Handle(command);

                entities.Verify(m => m.Update(It.Is<EmailAddress>(a => a.Value == newValue)), Times.Once());
                command.ChangedState.ShouldEqual(true);
                updatedEntity.Value.ShouldEqual(newValue);
            }
        }
    }
}
