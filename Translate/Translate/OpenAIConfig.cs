using System.IO;
using System.Text.Json;

namespace Translate;

public class OpenAIConfig
{
    public string Model { get; set; }

    public string ApiKey { get; set; }

    public string OpenAIUrl { get; set; }

    public static OpenAIConfig GetOpenAiConfig()
    {
        if (File.Exists("openai.json") == false)
        {
            return new OpenAIConfig();
        }

        var json = File.ReadAllText("openai.json");

        return JsonSerializer.Deserialize<OpenAIConfig>(json);
    }

    public void Save()
    {

        var json = JsonSerializer.Serialize(this);

        File.WriteAllText("openai.json", json);
    }
}
