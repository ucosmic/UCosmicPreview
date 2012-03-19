using System.Web.Security;

namespace UCosmic
{
    public class DotNetFormsAuthentication : ISignUsers
    {
        public string DefaultSignedOnUrl
        {
            get { return FormsAuthentication.DefaultUrl; }
        }

        public void SignOn(string userName, bool remember = false, string scope = null)
        {
            if (string.IsNullOrWhiteSpace(scope))
                FormsAuthentication.SetAuthCookie(userName, remember);
            else
                FormsAuthentication.SetAuthCookie(userName, remember, scope);
        }
    }
}
