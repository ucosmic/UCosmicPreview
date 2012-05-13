using System;
using System.Web.Security;
using TechTalk.SpecFlow;

namespace UCosmic.Www.Mvc.Areas.Identity
{
    [Binding]
    public class UpdatePasswordEvents : BaseStepDefinition
    {
        private static readonly string[] PasswordTestMembers = new[]
        {
            UpdateNameEvents.Any1AtUsilDotEduDotPe,
            "any1@bjtu.edu.cn",
            "any1@napier.ac.uk",
        };

        [BeforeTestRun]
        [BeforeScenario("UsingFreshExamplePasswords")]
        [AfterScenario("UsingFreshExamplePasswords")]
        public static void ResetExamplePasswords()
        {
            foreach (var userName in PasswordTestMembers)
            {
                var member = Membership.GetUser(userName);
                if (member == null)
                    throw new InvalidOperationException(
                        "User '{0}' could not be found."
                            .FormatWith(userName));
                if (member.IsLockedOut) member.UnlockUser();
                member.ChangePassword(member.ResetPassword(), "asdfasdf");
            }
        }
    }
}
