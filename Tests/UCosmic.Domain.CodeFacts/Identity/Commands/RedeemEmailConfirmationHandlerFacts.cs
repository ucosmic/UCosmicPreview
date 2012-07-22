using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Identity
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
                var handler = new RedeemEmailConfirmationHandler(null);
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
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<EmailConfirmation>())
                    .Returns(Enumerable.Empty<EmailConfirmation>().AsQueryable);
                var handler = new RedeemEmailConfirmationHandler(entities.Object);
                NullReferenceException exception = null;
                try
                {
                    handler.Handle(command);
                }
                catch (NullReferenceException ex)
                {
                    exception = ex;
                }

                entities.Verify(m => m.Get2<EmailConfirmation>(), Times.Once());
                exception.ShouldNotBeNull();
            }

            [TestMethod]
            public void ExecutesUpdate_OnEmailConfirmation_WhenNotRedeeed()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.CreatePassword)
                {
                    EmailAddress = new EmailAddress(),
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(RandomSecretGeneration(256))))
                    .Returns(null as string);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<EmailConfirmation>()).Returns(new[] { confirmation }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(ConfirmationEntity(confirmation))));
                var handler = new RedeemEmailConfirmationHandler(entities.Object);

                handler.Handle(command);

                entities.Verify(m => m.Update(It.Is(ConfirmationEntity(confirmation))),
                    Times.Once());
            }

            [TestMethod]
            public void ExecutesNoUpdate_OnEmailConfirmation_WhenAlreadyRedeeed()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword)
                {
                    EmailAddress = new EmailAddress(),
                    RedeemedOnUtc = DateTime.UtcNow.AddSeconds(-5),
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(RandomSecretGeneration(256))))
                    .Returns(null as string);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<EmailConfirmation>()).Returns(new[] { confirmation }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(ConfirmationEntity(confirmation))));
                var handler = new RedeemEmailConfirmationHandler(entities.Object);

                handler.Handle(command);

                entities.Verify(m => m.Update(It.Is(ConfirmationEntity(confirmation))),
                    Times.Never());
            }

            [TestMethod]
            public void SetsEmailAddressProperty_IsConfirmed_ToTrue_WhenNotRedeemed()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword)
                {
                    EmailAddress = new EmailAddress(),
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<EmailConfirmation>()).Returns(new[] { confirmation }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(ConfirmationEntity(confirmation))));
                queryProcessor.Setup(m => m.Execute(It.Is(RandomSecretGeneration(256))))
                    .Returns(null as string);
                var handler = new RedeemEmailConfirmationHandler(entities.Object);

                confirmation.EmailAddress.IsConfirmed.ShouldBeFalse();
                handler.Handle(command);

                confirmation.EmailAddress.IsConfirmed.ShouldBeTrue();
            }

            [TestMethod]
            public void SetsNoEmailAddressProperty_IsConfirmed_WhenAlreadyRedeeed()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.CreatePassword)
                {
                    EmailAddress = new EmailAddress(),
                    RedeemedOnUtc = DateTime.UtcNow.AddSeconds(-5),
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(RandomSecretGeneration(256))))
                    .Returns(null as string);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<EmailConfirmation>())
                    .Returns(new[] { confirmation }.AsQueryable);
                var handler = new RedeemEmailConfirmationHandler(entities.Object);

                confirmation.EmailAddress.IsConfirmed.ShouldBeFalse();
                handler.Handle(command);

                confirmation.EmailAddress.IsConfirmed.ShouldBeFalse();
            }

            [TestMethod]
            public void SetsConfirmationProperty_RedeemedOnUtc_WhenNotRedeeed()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword)
                {
                    EmailAddress = new EmailAddress(),
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<EmailConfirmation>()).Returns(new[] { confirmation }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(ConfirmationEntity(confirmation))));
                queryProcessor.Setup(m => m.Execute(It.Is(RandomSecretGeneration(256))))
                    .Returns(null as string);
                var handler = new RedeemEmailConfirmationHandler(entities.Object);

                confirmation.RedeemedOnUtc.HasValue.ShouldBeFalse();
                handler.Handle(command);

                confirmation.RedeemedOnUtc.HasValue.ShouldBeTrue();
                // ReSharper disable PossibleInvalidOperationException
                confirmation.RedeemedOnUtc.Value.ShouldBeInRange(
                    DateTime.UtcNow.AddSeconds(-2), DateTime.UtcNow.AddSeconds(2));
                // ReSharper restore PossibleInvalidOperationException
            }

            [TestMethod]
            public void SetsNoConfirmationProperty_RedeemedOnUtc_WhenAlreadyRedeeed()
            {
                var redeemedOnUtc = DateTime.UtcNow.AddSeconds(-5);
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.CreatePassword)
                {
                    EmailAddress = new EmailAddress(),
                    RedeemedOnUtc = redeemedOnUtc,
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(RandomSecretGeneration(256))))
                    .Returns(null as string);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<EmailConfirmation>()).Returns(new[] { confirmation }.AsQueryable);
                var handler = new RedeemEmailConfirmationHandler(entities.Object);

                handler.Handle(command);

                confirmation.RedeemedOnUtc.HasValue.ShouldBeTrue();
                // ReSharper disable PossibleInvalidOperationException
                confirmation.RedeemedOnUtc.Value.ShouldEqual(redeemedOnUtc);
                // ReSharper restore PossibleInvalidOperationException
            }

            [TestMethod]
            public void SetsConfirmationProperty_Ticket_WhenNotRedeeed()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword)
                {
                    EmailAddress = new EmailAddress(),
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(RandomSecretGeneration(256))))
                    .Returns(TwoFiftySixLengthString2);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<EmailConfirmation>()).Returns(new[] { confirmation }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(ConfirmationEntity(confirmation))));
                var handler = new RedeemEmailConfirmationHandler(entities.Object);

                confirmation.Ticket.ShouldBeNull();
                handler.Handle(command);

                confirmation.Ticket.ShouldNotBeNull();
                confirmation.Ticket.Length.ShouldEqual(256);
            }

            [TestMethod]
            public void SetsNoConfirmationProperty_Ticket_WhenAlreadyRedeeed()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.CreatePassword)
                {
                    EmailAddress = new EmailAddress(),
                    RedeemedOnUtc = DateTime.UtcNow.AddSeconds(-5),
                    Ticket = TwoFiftySixLengthString1,
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(RandomSecretGeneration(256))))
                    .Returns(TwoFiftySixLengthString2);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<EmailConfirmation>()).Returns(new[] { confirmation }.AsQueryable);
                var handler = new RedeemEmailConfirmationHandler(entities.Object);

                confirmation.Ticket.ShouldEqual(TwoFiftySixLengthString1);
                handler.Handle(command);

                confirmation.Ticket.ShouldNotBeNull();
                confirmation.Ticket.ShouldEqual(TwoFiftySixLengthString1);
                confirmation.Ticket.Length.ShouldEqual(256);
            }

            [TestMethod]
            public void CopiesTicketValue_ToCommandProperty_WhenNotRedeeed()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.ResetPassword)
                {
                    EmailAddress = new EmailAddress(),
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(RandomSecretGeneration(256))))
                    .Returns(TwoFiftySixLengthString1);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<EmailConfirmation>()).Returns(new[] { confirmation }.AsQueryable);
                entities.Setup(m => m.Update(It.Is(ConfirmationEntity(confirmation))));
                var handler = new RedeemEmailConfirmationHandler(entities.Object);

                command.Ticket.ShouldBeNull();
                handler.Handle(command);

                command.Ticket.ShouldNotBeNull();
                command.Ticket.Length.ShouldEqual(256);
                command.Ticket.ShouldEqual(confirmation.Ticket);
                command.Ticket.Length.ShouldEqual(256);
            }

            [TestMethod]
            public void CopiesTicketValue_ToCommandProperty_WhenAlreadyRedeeed()
            {
                var confirmation = new EmailConfirmation(EmailConfirmationIntent.CreatePassword)
                {
                    EmailAddress = new EmailAddress(),
                    RedeemedOnUtc = DateTime.UtcNow.AddSeconds(-5),
                    Ticket = TwoFiftySixLengthString2,
                };
                var command = new RedeemEmailConfirmationCommand
                {
                    Token = confirmation.Token,
                };
                var queryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);
                queryProcessor.Setup(m => m.Execute(It.Is(RandomSecretGeneration(256))))
                    .Returns(TwoFiftySixLengthString1);
                var entities = new Mock<ICommandEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Get2<EmailConfirmation>()).Returns(new[] { confirmation }.AsQueryable);
                var handler = new RedeemEmailConfirmationHandler(entities.Object);

                handler.Handle(command);

                command.Ticket.ShouldNotBeNull();
                command.Ticket.Length.ShouldEqual(256);
                command.Ticket.ShouldEqual(confirmation.Ticket);
                command.Ticket.ShouldEqual(TwoFiftySixLengthString2);
            }
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
