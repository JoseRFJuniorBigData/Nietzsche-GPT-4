
using UnityEngine;
using UnityEngine.UI;

namespace OpenAI
{
    public class ChatGPT : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private Text textArea;

        private OpenAIApi openai = new OpenAIApi();

        private string userInput;
        private string Instruction = "Always answer in English, with long responses.\nQ: ";

        private void Start()
        {
            inputField.onEndEdit.AddListener(delegate { SendReply(); });
        }

        private async void SendReply()
        {
            userInput = inputField.text;
            Instruction += $"{userInput}\nA: ";

            textArea.text = "...";
            inputField.text = "";

            button.enabled = false;
            inputField.enabled = false;

            // Complete the instruction
            var completionResponse = await openai.CreateCompletion(new CreateCompletionRequest()
            {
                Prompt = Instruction,
                Model = "text-davinci-003",
                MaxTokens = 4097 - Instruction.Length - 1
            });

            textArea.text = completionResponse.Choices[0].Text;
            Instruction += $"{completionResponse.Choices[0].Text}\nQ: ";

            button.enabled = true;
            inputField.enabled = true;
        }
    }
}