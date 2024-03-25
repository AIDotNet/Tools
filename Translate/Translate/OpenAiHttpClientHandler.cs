using System.Net.Http;

namespace Translate;

public sealed class OpenAiHttpClientHandler(OpenAIConfig openAiConfig) : HttpClientHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        UriBuilder uriBuilder;
        if (!string.IsNullOrWhiteSpace(openAiConfig.OpenAIUrl) &&
            request.RequestUri?.LocalPath == "/v1/chat/completions")
        {
            uriBuilder =
                new UriBuilder(openAiConfig.OpenAIUrl.TrimEnd('/') + "/v1/chat/completions");
            request.RequestUri = uriBuilder.Uri;
        }
        else if (!string.IsNullOrWhiteSpace(openAiConfig.OpenAIUrl) &&
                 request.RequestUri?.LocalPath == "/v1/embeddings")
        {
            uriBuilder = new UriBuilder(openAiConfig.OpenAIUrl.TrimEnd('/') + "/v1/embeddings");
            request.RequestUri = uriBuilder.Uri;
        }

        if (string.IsNullOrWhiteSpace(openAiConfig.ApiKey)) return await base.SendAsync(request, cancellationToken);
        request.Headers.Remove("Authorization");
        request.Headers.Add("Authorization", $"Bearer {openAiConfig.ApiKey}");

        return await base.SendAsync(request, cancellationToken);
    }
}