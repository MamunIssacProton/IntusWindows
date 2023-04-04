using IntusWindows.Sales.Order.Web.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http.Json;

namespace IntusWindows.Sales.Order.Web.Services.Services;

public  class ProgressiveHttpClient:HttpClient
{
    readonly ProgressService progressService;
    public ProgressiveHttpClient( ProgressService progressService)
    {
      
        this.BaseAddress = new Uri(ApiEndpoints.Order);
       this.progressService = progressService;
    }
 
    public  async Task<HttpResponseMessage> GetWithProgressAsync(string requestUri)
    {
        var response = await GetAsync(requestUri, HttpCompletionOption.ResponseContentRead);
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
                    var progress = totalRead * 100 / totalBytes;
                    progressService.Report(progress);
                  
                }
            }
        }
        return response;
    }

}

