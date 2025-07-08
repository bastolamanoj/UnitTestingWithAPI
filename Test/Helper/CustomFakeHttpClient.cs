using FakeItEasy;
using System.Net;
using System.Text.Json;

namespace Test.Helper
{
    public class CustomFakeHttpClient
    {
        public static HttpClient FakeHttpClient<T>(T content)
        {
            var response = new HttpResponseMessage
            {
                //StatusCode = System.Net.HttpStatusCode.OK,
                //Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(content), Encoding.UTF8, "application/json")
                Content = new StringContent(JsonSerializer.Serialize(content))
            };

            var handler = A.Fake<FakeableHttpMessageHandler>();
            A.CallTo(() => handler.FakeSendAsync(A<HttpRequestMessage>.Ignored, A<CancellationToken>.Ignored))
            .Returns(response);
            return new HttpClient(handler);

        }

    }
}
