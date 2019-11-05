using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FileIO
{
    public class FileDownload
    {
        HttpClient _client;

        public FileDownload(HttpClient client)
        {
            if (client != null)
                _client = client;
            else
                _client = new HttpClient();
        }

        public async Task<string> DownloadFileAsync()
        {

            string url = "https://github.com/Ravindra-a/largefile/blob/master/README.md";
            using (HttpResponseMessage response = await _client.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }
    }
}
