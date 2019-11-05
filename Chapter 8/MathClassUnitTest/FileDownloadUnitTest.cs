using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CalculatorUnitTest
{
    public class FileDownloadUnitTest
    {
        [Fact]
        public async Task DownloadFileSuccess()
        {
            var messageHandler = FakeHttpMsgHandler.GetHttpMsgHandler();
            var httpClient = new HttpClient(messageHandler);
            var mathClass = new FileIO.FileDownload(httpClient);
            string expectedResult = "Response from fake httpclient";

            char[] charArray = expectedResult.ToCharArray();
            Array.Reverse(charArray);

            var result = await mathClass.DownloadFileAsync();
            Assert.Equal(new string(charArray), result);
        }
    }

    public class FakeHttpMsgHandler : HttpMessageHandler
    {
        private HttpResponseMessage _response;

        public static HttpMessageHandler GetHttpMsgHandler()
        {
            HttpResponseMessage response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Response from fake httpclient")
            };
            var msgeHandler = new FakeHttpMsgHandler(response);

            return msgeHandler;
        }

        public FakeHttpMsgHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var taskCompletionSource = new TaskCompletionSource<HttpResponseMessage>();

            taskCompletionSource.SetResult(_response);

            return taskCompletionSource.Task;
        }
    }
}
