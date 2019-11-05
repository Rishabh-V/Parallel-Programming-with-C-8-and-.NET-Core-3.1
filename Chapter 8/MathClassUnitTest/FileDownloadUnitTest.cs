using System;
using System.Collections.Generic;
using System.IO;
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
            // Dummy response
            HttpResponseMessage mockResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Response from fake httpclient")
            };
            var msgeHandler = new FakeHttpMsgHandler(mockResponse);
            var httpClient = new HttpClient(msgeHandler);
            var mathClass = new FileIO.FileDownload(httpClient);

            string expectedResult = "Response from fake httpclient";
            //string reversal logic
            char[] charArray = expectedResult.ToCharArray();
            Array.Reverse(charArray);
            //Call to method
            var result = await mathClass.DownloadFileAsync();
            Assert.Equal(charArray.Length, result.Length);//assertion
            Assert.Equal(new string(charArray), result);//assertion            
        }

        [Fact]
        public async Task DownloadFileNotFound()
        {
            // Dummy response
            HttpResponseMessage mockResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
            };
            var msgeHandler = new FakeHttpMsgHandler(mockResponse);
            var httpClient = new HttpClient(msgeHandler);
            var mathClass = new FileIO.FileDownload(httpClient);

            var result = mathClass.DownloadFileAsync();
            await Assert.ThrowsAsync<FileNotFoundException>(async () => await result);
        }
    }
}
