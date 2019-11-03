using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Calculator
{
    public class MathClass
    {
        public int Divide(int numerator, int? denominator)
        {
            if (denominator == 0)
            {
                throw new DivideByZeroException();
            }
            else if (!denominator.HasValue)
            {
                throw new ArgumentNullException();
            }
            else
            {
                //return numerator / denominator;
                int remainder;
                return Math.DivRem(numerator, denominator.Value, out remainder);
            }
        }

        public async Task<int> DivideAsync(int numerator, int denominator)
        {
            try
            {
                var t = Task.Run(() =>
                {
                    return Divide(numerator, denominator);
                });
                return await t;
                //return await t.ConfigureAwait(false);
            }
            catch
            {
                throw;
            }
        }

        private async Task DownloadLargeFilAsync()
        {
            byte[] buffer = new byte[8192];
            using (HttpClient client = new HttpClient())
            {

                string url = "https://github.com/Ravindra-a/largefile/archive/master.zip";
                using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();
                    long totalLength = response.Content.Headers.ContentLength.HasValue ? response.Content.Headers.ContentLength.Value : 34632982; //Once in a while github returns response without content length header 
                    //hence in that case defaulting to actual file size
                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    {
                            int dataToRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    }
                }
            }
        }


    }
}
