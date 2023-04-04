using System;
namespace IntusWindows.Sales.Order.Web.Services.Services;

public  class ProgressiveHttpClient:HttpClient
{
    readonly IProgress<long> progress;

    public ProgressiveHttpClient(IProgress<long> progress)
    {
        this.progress = progress;
        this.BaseAddress = new Uri(ApiEndpoints.Order);

    }

    public  async Task<HttpResponseMessage> GetWithProgressAsync(string requestUri)
    {
        var response = await GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);
        if (response.IsSuccessStatusCode)
        {
            var contentLength = response.Content.Headers.ContentLength;
            if (contentLength.HasValue)
            {
                var totalBytes = contentLength.Value;
                using var stream = await response.Content.ReadAsStreamAsync();
                var buffer = new byte[4096];
                var bytesRead = 0L;
                var totalRead = 0L;
                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    totalRead += bytesRead;
                    this.progress?.Report(totalRead * 100 / totalBytes);
                }
            }
        }
        return response;
    }


  

  
}

