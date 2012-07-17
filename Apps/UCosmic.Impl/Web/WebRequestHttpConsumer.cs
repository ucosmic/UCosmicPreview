using System.IO;
using System.Net;
using UCosmic.Domain;

namespace UCosmic.Impl
{
    public class WebRequestHttpConsumer : IConsumeHttp
    {
        public string Get(string url)
        {
            string content = null;
            var request = WebRequest.Create(url);
            var response = request.GetResponse();
            var stream = response.GetResponseStream();
            if (stream != null)
            {
                var reader = new StreamReader(stream);
                content = reader.ReadToEnd();
                reader.Close();
                stream.Close();
            }
            response.Close();
            return content;
        }
    }
}