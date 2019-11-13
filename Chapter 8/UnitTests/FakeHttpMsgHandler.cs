using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class FakeHttpMsgHandler : HttpMessageHandler
    {
        private HttpResponseMessage _response;
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
