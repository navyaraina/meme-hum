using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

namespace OpenAI
{
    public class Drawing_Prompt : MonoBehaviour
    {
        [SerializeField] private Button Generate;
        [SerializeField] private RectTransform sent;    
        [SerializeField] private RectTransform received;

        private float height;
        private OpenAIApi openai = new OpenAIApi();
        private List<ChatMessage> messages = new List<ChatMessage>();
        private string instruction = "tell me something i can draw which might seem funny";
        private string prompt = "Let's start drawing!";
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
                GameObject.Find("Received Message(Clone)").transform.localPosition = new Vector3(10, 200, 100);
                GameObject.Find("Received Message(Clone)").GetComponent<RectTransform>().sizeDelta = new Vector2(300, 150);

                Touch touch = new Touch();
                if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended){
                    SceneManager.LoadScene("Drawing_scene");
                }
            }
        }

        private async void SendReply(string choice)
        {
            string userMessageContent;
            userMessageContent = "tell me something i can draw which might seem funny and respond with just the prompt";

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
