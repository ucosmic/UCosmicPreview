using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
    // ReSharper disable UnusedMember.Global
    public class RedeemEmailConfirmationHandlerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheHandleMethod
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenCommandArgIsNull()
            {
                var handler = new RedeemEmailConfirmationHandler(null, null);
                ArgumentNullException exception = null;
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
            public void ExecutesQuery_ForEmailConfirmation()
            {
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = Guid.NewGuid(),
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(null as EmailConfirmation);
                var handler = new RedeemEmailConfirmationHandler(queryProcessor.Object, null);
                NullReferenceException exception = null;
                try
                {
                    handler.Handle(command);
                }
                catch (NullReferenceException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
            }

            [TestMethod]
            public void ExecutesUpdate_OnEmailConfirmation()
            {
                var confirmation = new EmailConfirmation();
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(confirmation);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                entities.Setup(m => m.Update(It.Is(ConfirmationEntity(confirmation))));
                var handler = new RedeemEmailConfirmationHandler(
                    queryProcessor.Object, entities.Object);

                handler.Handle(command);

                entities.Verify(m => m.Update(It.Is(ConfirmationEntity(confirmation))),
                    Times.Once());
            }

            [TestMethod]
            public void SetsConfirmationProperty_RedeemedOnUtc()
            {
                var confirmation = new EmailConfirmation();
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(confirmation);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                entities.Setup(m => m.Update(It.Is(ConfirmationEntity(confirmation))));
                var handler = new RedeemEmailConfirmationHandler(
                    queryProcessor.Object, entities.Object);

                handler.Handle(command);

                confirmation.RedeemedOnUtc.HasValue.ShouldBeTrue();
                // ReSharper disable PossibleInvalidOperationException
                confirmation.RedeemedOnUtc.Value.ShouldBeInRange(
                    DateTime.UtcNow.AddSeconds(-2), DateTime.UtcNow.AddSeconds(2));
                // ReSharper restore PossibleInvalidOperationException
            }

            [TestMethod]
            public void SetsConfirmationProperty_SecretCode_ToNull()
            {
                var confirmation = new EmailConfirmation
                {
                    SecretCode = "secret",
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(confirmation);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                entities.Setup(m => m.Update(It.Is(ConfirmationEntity(confirmation))));
                var handler = new RedeemEmailConfirmationHandler(
                    queryProcessor.Object, entities.Object);

                handler.Handle(command);

                confirmation.SecretCode.ShouldBeNull();
            }

            [TestMethod]
            public void SetsConfirmationProperty_Ticket()
            {
                var confirmation = new EmailConfirmation();
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(confirmation);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                entities.Setup(m => m.Update(It.Is(ConfirmationEntity(confirmation))));
                var handler = new RedeemEmailConfirmationHandler(
                    queryProcessor.Object, entities.Object);

                handler.Handle(command);

                confirmation.Ticket.ShouldNotBeNull();
                confirmation.Ticket.Length.ShouldEqual(256);
            }

            [TestMethod]
            public void CopiesTicketValue_ToCommandProperty()
            {
                var confirmation = new EmailConfirmation();
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(ConfirmationQueryBasedOn(command))))
                    .Returns(confirmation);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict);
                entities.Setup(m => m.Update(It.Is(ConfirmationEntity(confirmation))));
                var handler = new RedeemEmailConfirmationHandler(
                    queryProcessor.Object, entities.Object);

                handler.Handle(command);

                command.Ticket.ShouldNotBeNull();
                command.Ticket.Length.ShouldEqual(256);
                command.Ticket.ShouldEqual(confirmation.Ticket);
            }
        }

        private static Expression<Func<GetEmailConfirmationQuery, bool>> ConfirmationQueryBasedOn(RedeemEmailConfirmationCommand command)
        {
            Expression<Func<GetEmailConfirmationQuery, bool>> queryBasedOn = q =>
                q.Token == command.Token
            ;
            return queryBasedOn;
        }

        private static Expression<Func<EmailConfirmation, bool>> ConfirmationEntity(EmailConfirmation entity)
        {
            Expression<Func<EmailConfirmation, bool>> entityBasedOn = e =>
                e == entity
            ;
            return entityBasedOn;
        }
    }
}
