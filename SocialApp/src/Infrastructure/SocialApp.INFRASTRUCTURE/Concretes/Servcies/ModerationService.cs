using SocialApp.APPLICATION.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.AI.Inference;
using Microsoft.Extensions.Configuration;


namespace SocialApp.INFRASTRUCTURE.Concretes.Servcies;

public class ModerationService : IModerationService
{
    private readonly string _prompt;
    private readonly string _key;

    public ModerationService(IConfiguration configuration)
    {
        _prompt = configuration["Moderation:ModerationPrompt"] ??"";
        _key = configuration["Moderation:AIKey"] ??"";
    }
    public Task<string> SendPrompt(string content = "Is this sentence safe?")
    {
    
        if (content is null)
        {
            return Task.FromResult("Enter valid content");
        }

        var endpoint = new Uri("https://models.github.ai/inference");
        var credential = new AzureKeyCredential("");
        var model = "openai/gpt-4.1";

        var client = new ChatCompletionsClient(
            endpoint,
            credential,
            new AzureAIInferenceClientOptions());

        var requestOptions = new ChatCompletionsOptions()
        {
            Messages =
    {
        new ChatRequestSystemMessage("You are a content moderation assistant. If the input you receive contains any inappropriate usage, unethical profanity, or threats in any language, return false; otherwise, return true."),
        new ChatRequestUserMessage(content),
    },
            Temperature = 1f,
            NucleusSamplingFactor = 1f,
            Model = model
        };

        Response<ChatCompletions> response = client.Complete(requestOptions);
        Console.WriteLine("------------------------------");
        Console.WriteLine(response.Value.Usage.TotalTokens);
        Console.WriteLine(response.Value.Usage.CompletionTokens);
        Console.WriteLine(response.Value.Usage.PromptTokens);

        return Task.FromResult(response.Value.Content);
    }
}
