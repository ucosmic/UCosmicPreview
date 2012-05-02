using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public interface IModelSigningEmail : IReturnUrl
    {
        string EmailAddress { get; set; }
        bool RememberMe { get; set; }
    }
}