//using System;
//using System.Collections.Generic;
//using UCosmic.Domain.Establishments;
//using UCosmic.Domain.People;

//namespace UCosmic.Domain.Email
//{
//    public class EmailComposer
//    {
//        private readonly EmailTemplateFinder _emailTemplates;
//        private readonly IManageConfigurations _config;

//        public EmailComposer(IQueryEntities entityQueries, IManageConfigurations config)
//        {
//            _emailTemplates = new EmailTemplateFinder(entityQueries);
//            _config = config;
//        }

//        public EmailMessage ComposeEmail(string templateName, Establishment forEstablishment, IDictionary<string, string> variables)
//        {
//            var template = _emailTemplates.FindOne(EmailTemplateBy.Name(templateName, 
//                forEstablishment != null ? forEstablishment.RevisionId : (int?)null, 
//                true));
//            //if (forEstablishment != null)
//            //{
//            //    var templates = _emailTemplates.Current.Where(t =>
//            //        t.Name.Equals(templateName, StringComparison.OrdinalIgnoreCase)
//            //            && (t.EstablishmentId == forEstablishment.RevisionId || !t.EstablishmentId.HasValue)).ToList();
//            //    template = (templates.Count > 1)
//            //        ? templates.SingleOrDefault(t => t.EstablishmentId == forEstablishment.RevisionId)
//            //        : templates.Single();
//            //}

//            //// fall back to default template if no establishment-specific template could be found
//            //template = template ?? _emailTemplates.Current.SingleOrDefault(
//            //        t => t.Name.Equals(templateName, StringComparison.OrdinalIgnoreCase)
//            //            && t.Establishment == null);

//            // throw exception if template cannot be found
//            if (template == null)
//                throw new InvalidOperationException(string.Format("No email template found with name '{0}' for establishment '{1}'",
//                    templateName, (forEstablishment == null) ? "null" : forEstablishment.OfficialName));

//            // compose message
//            var message = new EmailMessage
//            {
//                // subject & body
//                Subject = template.SubjectFormat.FormatTemplate(variables),
//                Body = template.BodyFormat.FormatTemplate(variables),

//                // from address (has failsafe from address)
//                FromAddress = template.FromAddress ??
//                    _config.EmailDefaultFromAddress ?? "no-reply@ucosmic.com",
//                FromDisplayName = template.FromDisplayName ??
//                    _config.EmailDefaultFromDisplayName,

//                // replyto address
//                ReplyToAddress = template.ReplyToAddress ??
//                    _config.EmailDefaultReplyToAddress,

//                ReplyToDisplayName = template.ReplyToDisplayName ??
//                    _config.EmailDefaultReplyToDisplayName,

//                FromEmailTemplate = template.Name,
//                ComposedOnUtc = DateTime.UtcNow,
//            };

//            return message;
//        }

//    }
//}