using SharpToken;

namespace Translate;
public static class TokenHelper
{
    private static GptEncoding? _encoding;

    /// <summary>
    /// 获取GptEncoding
    /// </summary>
    /// <returns></returns>
    public static GptEncoding GetGptEncoding()
    {
        _encoding ??= GptEncoding.GetEncodingForModel("gpt-4");

        return _encoding;
    }

    /// <summary>
    /// 计算token
    /// </summary>
    /// <param name="tokens"></param>
    /// <returns></returns>
    public static int ComputeToken(params string[] tokens)
    {
        return tokens.Where(token => !string.IsNullOrWhiteSpace(token)).Sum(token => GetGptEncoding().Encode(token).Count);
    }
}