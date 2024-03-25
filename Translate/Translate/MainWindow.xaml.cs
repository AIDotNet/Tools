using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using System.Net.Http;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Translate;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddWpfBlazorWebView();
        serviceCollection.AddMasaBlazor();

        serviceCollection.AddSingleton(OpenAIConfig.GetOpenAiConfig());

        serviceCollection.AddSingleton((services) =>
        {
            var openAIConfig = services.GetRequiredService<OpenAIConfig>();

            return Kernel.CreateBuilder()
                .AddOpenAIChatCompletion(
                    modelId: openAIConfig!.Model,
                    apiKey: openAIConfig.ApiKey,
                    httpClient: new HttpClient(new OpenAiHttpClientHandler(openAIConfig)))
                .Build();
        });

#if DEBUG
        serviceCollection.AddBlazorWebViewDeveloperTools();
#endif

        Resources.Add("services", serviceCollection.BuildServiceProvider());
    }
}