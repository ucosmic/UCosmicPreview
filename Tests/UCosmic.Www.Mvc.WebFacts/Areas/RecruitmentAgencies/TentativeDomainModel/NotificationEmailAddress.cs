using UCosmic.Domain;

// ReSharper disable ClassNeverInstantiated.Global
namespace UCosmic.Www.Mvc.Areas.RecruitmentAgencies.TentativeDomainModel
{
    public class NotificationEmailAddress : RevisableEntity
    {
        // primary key consists of both ForEstablishmentId and Value
        public int ForEstablishmentId { get; set; }
        public string Value { get; set; }
    }
}
// ReSharper restore ClassNeverInstantiated.Global
