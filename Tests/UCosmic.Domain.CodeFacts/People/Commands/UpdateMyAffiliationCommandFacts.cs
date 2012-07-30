using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.People
{
    public static class UpdateMyAffiliationCommandFacts
    {
        [TestClass]
        public class TheChangedStateProperty
        {
            [TestMethod]
            public void IsTrue_WhenChangeCount_IsGreaterThanZero()
            {
                var command = new UpdateMyAffiliationCommand
                {
                    ChangeCount = 1,
                };

                command.ChangedState.ShouldBeTrue();
            }

            [TestMethod]
            public void IsFalse_WhenChangeCount_IsZero()
            {
                var command = new UpdateMyAffiliationCommand
                {
                    ChangeCount = 0,
                };

                command.ChangedState.ShouldBeFalse();
            }
        }
    }
}
