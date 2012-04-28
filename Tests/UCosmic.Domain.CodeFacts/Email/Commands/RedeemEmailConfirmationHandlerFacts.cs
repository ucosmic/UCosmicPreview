using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Identity;
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

                queryProcessor.Verify(m => m.Execute(It.Is(ConfirmationQueryBasedOn(command))),
                    Times.Once());
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
                queryProcessor.Setup(m => m.Execute(It.Is(RandomSecretGeneration(256))))
                    .Returns(null as string);
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
                queryProcessor.Setup(m => m.Execute(It.Is(RandomSecretGeneration(256))))
                    .Returns(null as string);
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
                queryProcessor.Setup(m => m.Execute(It.Is(RandomSecretGeneration(256))))
                    .Returns(null as string);
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
                queryProcessor.Setup(m => m.Execute(It.Is(RandomSecretGeneration(256))))
                    .Returns(TwoFiftySixLengthString2);
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
                queryProcessor.Setup(m => m.Execute(It.Is(RandomSecretGeneration(256))))
                    .Returns(TwoFiftySixLengthString1);
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

        private static Expression<Func<GenerateRandomSecretQuery, bool>> RandomSecretGeneration(int length)
        {
            Expression<Func<GenerateRandomSecretQuery, bool>> randomSecretGeneration = q =>
                q.MinimumLength == length &&
                q.MaximumLength == length
            ;
            return randomSecretGeneration;
        }

        private const string TwoFiftySixLengthString1 = "x5F6TiLb92DeYm3q8X7Gow4R3Scj7NJk96ZtKr4z8CAg2p5PQd2s5MBf6y4W7EnHa98Fyf3KWd74Geo5R2Bpk9NJn8z3Q6Mwr3T8Pis5A9Lmx4X7DgZa6t2Y4HbCj89EqSc75Xjt6HRg2s3AEe24Gom6NMd8z7B9Spc3DKn54Twi9C3JkWf8q5ZFa27Qrx6LYb54PyRi6k3W9YeTd7y8N2BnXf64JqFg27Sxo8K3CmPa59QbMz85HjLr69DpAt2c";

        private const string TwoFiftySixLengthString2 = "Qo7c6YZg89Drf3W2BqGz45Kjy8RSk9p4N7TeHw35EbAi2s6C6Ptd2LJm9x7FXa53Mnm8B4Pcp4Q8Etf7WSe9z3TMj6b2KJd55RyGk3w9ZHn6i2D8AqNr4x7XFs37Coa4Y8LgKa56QmZt29Bio5A2SnPy4s8G3YbNx96XfJe72Wgd4REw7z5C8Mkq9T3DcFp6r7HLj48Qrn3KZf9q6D5LjFi22Jzk9X5Sxc6N8Hey4PYd3p7WGs92RgTm5w8MBa43";
    }
}
