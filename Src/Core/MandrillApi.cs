using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Mandrill.Net.Core
{
    public class MandrillApi
    {
        readonly HttpClient _client;
        readonly string _rootUrl;
        readonly string _key;

        public MandrillApi(HttpClient client, string rootUrl, string key)
        {
            _client = client;
            _rootUrl = rootUrl;
            _key = key;
        }

        public Task<HttpResponseMessage> Ping()
        {
            var path = string.Format("{0}{1}", _rootUrl, "users/ping.json");
            var uri = new Uri(path);
            var request = new HttpRequestMessage(HttpMethod.Post, uri)
                              {
                                  Content = new StringContent(JsonConvert.SerializeObject(new {key = _key})),
                              };
            
            return _client.SendAsync(request);
        }

        public Task<HttpResponseMessage> SendEmail(string templateName, string emailAddress, string name, object templateContent)
        {
            var path = string.Format("{0}{1}", _rootUrl, "messages/send-template.json");
            var uri = new Uri(path);

            var payload = new
                              {
                                  key = _key,
                                  template_name = templateName,
                                  template_content = templateContent,
                                  message = new
                                                {
                                                    text = "example text",
                                                    subject = "example subject",
                                                    from_email = "test@elevatedirect.com",
                                                    from_name = "search engine",

                                                    to = new[] { new { email = emailAddress, name = name } }
                                                }

                              };
            var request = new HttpRequestMessage(HttpMethod.Post, uri)
                              {
                                  Content = new StringContent(JsonConvert.SerializeObject(payload)),
                              };
            
            return _client.SendAsync(request);
        }
    }
}