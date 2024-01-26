using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using OpenAI_API;
using UnityEngine.UI;
using System;
using OpenAI_API.Chat;
using System.Threading.Tasks;

public class OpenAIController : MonoBehaviour
{
    public Button Truth;
    public Button Dare;
    public TMP_Text randomtext;

    private OpenAIAPI api;
    private List<ChatMessage> messages;

    private void Start()
    {
        api = new OpenAIAPI("YOUR_API_KEY");
        Truth.onClick.AddListener(() => GetResponse_Truth());
        Dare.onClick.AddListener(() => GetResponse_Dare());
    }

    private async void GetResponse_Dare()
    {
        string randomDare = await GetRandomMessage("Dare");
        AddMessage(new ChatMessage(ChatMessageRole.System, randomDare));
        Dare.enabled = false;
        Truth.enabled = false;
    }

    private async void GetResponse_Truth()
    {
        string randomTruth = await GetRandomMessage("Truth");
        AddMessage(new ChatMessage(ChatMessageRole.System, randomTruth));
        Dare.enabled = false;
        Truth.enabled = false;
    }

    private async Task<string> GetRandomMessage(string category)
    {
        // Generate a prompt for the specific category (Dare or Truth)
        string prompt = $"Give me a random {category.ToLower()}.";

        // Call the OpenAI API to get a response
        OpenAIChatCompletionResponse response = await api.CompleteAsync(prompt);

        // Extract and return the generated message from the API response
        return response?.Choices?[0]?.Message?.Content;
    }

    private void AddMessage(ChatMessage message)
    {
        messages.Add(message);
        // Add logic to display or process the messages.
    }
}
