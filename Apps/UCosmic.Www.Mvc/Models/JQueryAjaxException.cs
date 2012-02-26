using System;

namespace UCosmic.Www.Mvc.Models
{
    public class JQueryAjaxException : Exception
    {
        public string ReadyState { get; set; }
        public string Status { get; set; }
        public string Url { get; set; }
        public string ErrorThrown { get; set; }

        public override string Message
        {
            get
            {
                return string.Format(
                    "JQuery ajax method encountered an error:\r\nReadyState={0}\r\nStatus={1}\r\nUrl={2}\r\nErrorThrown={3}", 
                        ReadyState, Status, Url, ErrorThrown);
            }
        }
    }
}