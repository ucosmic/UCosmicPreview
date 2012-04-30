using System;

namespace UCosmic.Www.Mvc.Models
{
    public interface IModelEmailConfirmation
    {
        Guid Token { get; set; }
        string Intent { get; set; }
    }
}