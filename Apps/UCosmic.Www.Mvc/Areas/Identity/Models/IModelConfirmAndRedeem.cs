using System;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public interface IModelConfirmAndRedeem
    {
        Guid Token { get; set; }
    }
}