using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

namespace OpenAI
{
    public class ChatGPT : MonoBehaviour
    {
        [SerializeField] private Button truth;
        [SerializeField] private Button dare;
        [SerializeField] private Button exit;
        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;
        [SerializeField] private TextMeshProUGUI promptText;

        private float height;
        private OpenAIApi openai = new OpenAIApi();
        private List<ChatMessage> messages = new List<ChatMessage>();
        private string instruction = "Ask truth or dare questions";
        private string prompt = "Truth or dare?";
        public RectTransform canvas;
        private void Start()
        {
            truth.onClick.AddListener(() => SendReply("truth"));
            dare.onClick.AddListener(() => SendReply("dare"));
            exit.onClick.AddListener(()=>ExitGame());
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
                GameObject.Find("Received Message(Clone)").transform.localPosition = new Vector3(100, 0, 100);
                GameObject.Find("Received Message(Clone)").GetComponent<RectTransform>().sizeDelta = new Vector2(300, 300);
            }
        }

        private async void SendReply(string choice)
        {
            string userMessageContent;

            if (choice.ToLower() == "truth")
            {
                userMessageContent = "Ask me a truth question";
            }
            else if (choice.ToLower() == "dare")
            {
                userMessageContent = "Give me a dare";
            }
            else
            {
                Debug.LogWarning($"Invalid choice: {choice}");
                return;
            }

            var newMessage = new ChatMessage()
            {
                Role = "user",  
                Content = userMessageContent
            };

            AppendMessage(newMessage);
            messages.Add(newMessage);

    

            truth.enabled = false;
            dare.enabled = false;
            exit.enabled=true;

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

            truth.enabled = true;
            dare.enabled = true;
        }

    private async void ExitGame(){
        SceneManager.LoadScene("Start");
        }
    }
}
