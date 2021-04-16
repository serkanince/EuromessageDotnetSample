using EuromessageDotnetSample.Dto;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EuromessageDotnetSample
{
    /// <summary>
    /// API Endpoint https://relateddigital.atlassian.net/wiki/spaces/RMCKBT/pages/428802257/RMC+REST+API
    /// Author Serkan İnce https://github.com/serkanince
    /// </summary>
    class Program
    {
        const string LoginEndpoint = "https://api.relateddigital.com/resta/api/auth/login";
        const string PostHtmlEndpoint = "https://api.relateddigital.com/resta/api/post/PostHtml";

        static void Main(string[] args)
        {
            SendMail();

            Console.ReadLine();
        }

        public async static void SendMail()
        {
            var loginResult = await Login();

            if (loginResult.Success)
            {
                Console.WriteLine("Succesfully Login !");

                var postResult = await PostRequest(loginResult.ServiceTicket);

                if (postResult.Success)
                    Console.WriteLine("Mail Send Completed !");
                else
                    Console.WriteLine(string.Format("{0} - {1}", postResult.Errors[0].Code, postResult.Errors[0].Message));
            }
            else
            {
                Console.WriteLine(string.Format("{0} - {1}", loginResult.Errors[0].Code, loginResult.Errors[0].Message));
            }
        }

        public async static Task<LoginResponseDto> Login()
        {

            return await Task.Run(async () =>
            {
                var client = new HttpClient();

                //in web apps get it from appsettings
                var dto = new LoginRequestDto()
                {
                    UserName = "euromessage_user_name", 
                    Password = "euromessage_password",
                };

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string json = JsonConvert.SerializeObject(dto, Formatting.Indented);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var content = await client.PostAsync(LoginEndpoint, httpContent);

                return JsonConvert.DeserializeObject<LoginResponseDto>(await content.Content.ReadAsStringAsync());
            });

        }

        public async static Task<PostHtmlResponseDto> PostRequest(string serviceTicket)
        {
            return await Task.Run(async () =>
            {
                var client = new HttpClient();

                var dto = new PostHtmlRequestDto()
                {

                    FromName = "Your Company",
                    FromAddress = "hi@email",
                    ReplyAddress = "hi@email",
                    Subject = "Serkan Test",
                    HtmlBody = "<html><head></head><body>Hello World!!</body></html>",
                    Charset = "iso-8859-9",
                    ToName = "New User",
                    ToEmailAddress = "newuser@mail.com",
                    KeyId = "",
                    Attachments = null,
                    PostType = "",
                    CustomParams = "",
                };

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", serviceTicket);

                string json = JsonConvert.SerializeObject(dto, Formatting.Indented);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var content = await client.PostAsync(PostHtmlEndpoint, httpContent);

                return JsonConvert.DeserializeObject<PostHtmlResponseDto>(await content.Content.ReadAsStringAsync());
            });
        }
    }
}
