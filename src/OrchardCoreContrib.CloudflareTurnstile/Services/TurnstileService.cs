using Microsoft.Extensions.Options;
using OrchardCoreContrib.CloudflareTurnstile.Configuration;
using System.Net.Http.Json;

namespace OrchardCoreContrib.CloudflareTurnstile.Services;

public class TurnstileService(
    IHttpClientFactory httpClientFactory,
    IOptions<TurnstileOptions> options)
{
    private readonly TurnstileOptions options = options.Value;

    public async Task<bool> ValidateAsync(string token)
    {
        var client = httpClientFactory.CreateClient();
        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["secret"] = options.SecretKey,
            ["response"] = token
        });

        var response = await client.PostAsync(Constants.TurnstileApiUri, content);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadFromJsonAsync<TurnstileResponse>();
            
            return json.Success;
        }

        return false;
    }
}
