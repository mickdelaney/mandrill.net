using System;
using System.Net.Http;
using Machine.Specifications;
using Mandrill.Net.Core;

namespace Mandrill.Net.Specs
{
    [Subject("Sending an email")]
    public class When_context_a_templated_email_is_sent_via_the_mandrill_api
    {
        const string MandrillRoot = "https://mandrillapp.com/api/1.0/";
        const string TemplateName = "Basic Elevate Template";

        static MandrillApi _mandrill;
        static HttpClient _client;
        static string _apiResponse = string.Empty;

        Establish context = () =>
        {
            _client = new HttpClient();
            _mandrill = new MandrillApi(_client, MandrillRoot, "28617efc-423c-453f-ae46-b50bdbe326a2");

            var templateContent = new {};
            var result = _mandrill.SendEmail(TemplateName, "mick@elevatedirect.com", "mick", templateContent).Result;
            result.EnsureSuccessStatusCode();
            _apiResponse = result.Content.ReadAsStringAsync().Result;
            Console.WriteLine(_apiResponse);
        };

        Because of = () => { };

        It should_validate_the_api_correctly = () => _apiResponse.ShouldNotBeEmpty();
    }

}
