using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace OpenAI
{
    public class MEME_GENERATOR : MonoBehaviour
    {
        [SerializeField] private Button Generate;
        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;

        private float height;
        private OpenAIApi openai = new OpenAIApi();
        private List<ChatMessage> messages = new List<ChatMessage>();
        private string instruction = "give a random sentence to make a meme on";
        private string prompt = "Let's make some memes!";
        public RectTransform canvas;
        private void Start()
        {
            Generate.onClick.AddListener(() => SendReply("Generate"));
        }

        private void AppendMessage(ChatMessage message)
        {
            var item = Instantiate(message.Role == "user" ? sent : received);
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content;
            item.anchoredPosition = new Vector2(0, -height);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            height += item.sizeDelta.y;
        }
        private void Update()
        {
            if(GameObject.Find("Received Message(Clone)")!=null)
            {
                GameObject.Find("Received Message(Clone)").transform.SetParent(canvas);
                GameObject.Find("Received Message(Clone)").transform.localScale = new Vector3(1, 1, 1);
                GameObject.Find("Received Message(Clone)").transform.localPosition = new Vector3(200, 300, 100);
                GameObject.Find("Received Message(Clone)").GetComponent<RectTransform>().sizeDelta = new Vector2(500, 300);
            }
        }

        private async void SendReply(string choice)
        {
            string userMessageContent;
            userMessageContent = "Give me a random sentence to make a meme on";

            var newMessage = new ChatMessage()
            {
                Role = "user",  
                Content = userMessageContent
            };

            AppendMessage(newMessage);
            messages.Add(newMessage);

    

            Generate.enabled = false;

            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0613",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();

                messages.Add(message);
                AppendMessage(message);
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            Generate.enabled = true;
        }
    }
}
