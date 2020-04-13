using System.Collections;
using System.Collections.Generic;
using System.Text;
using Client.Core;
using Client.Core.Models;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Client
{
    public class ClientExample : MonoBehaviour
    {
        [Header ("Links")]
        [SerializeField]
        private string baseUrl = "https://uksouth.api.cognitive.microsoft.com/vision/v2.0/ocr?language=unk&detectOrientation=true";

        [SerializeField]
        private string clientId;

        [SerializeField]
        private string clientSecret;

        [SerializeField]
        private string imageToOCR = "";

        [Header ("UI")]
        [SerializeField]
        private TextMeshProUGUI header;

        [SerializeField]
        private TextMeshProUGUI wordsCapture;

        [SerializeField]
        private Button submitLinkButton;

        [SerializeField]
        private TMP_InputField linkInputField;



        void Start()
        {
            header.text = "";
            wordsCapture.text = "";
            
        }

        void OnRequestComplete(Response response)
        {
            Debug.Log($"Status Code: {response.StatusCode}");
            Debug.Log($"Data: {response.Data}");
            Debug.Log($"Error: {response.Error}");

            if (string.IsNullOrEmpty(response.Error) && !string.IsNullOrEmpty(response.Data))
            {
                AzureOCRResponse azureOCRResponse = JsonUtility.FromJson<AzureOCRResponse>(response.Data);

                header.text = $"Orientation: {azureOCRResponse.orientation} Language: {azureOCRResponse.language} Text Angle: {azureOCRResponse.textAngle}";

                string words = string.Empty;
                foreach (var region in azureOCRResponse.regions)
                {
                    foreach (var line in region.lines)
                    {
                        foreach (var word in line.words)
                        {
                            words += word.text + " : ";
                        }
                    }
                }
                wordsCapture.text = words;
            }
            else
            {
                header.text = "PLEASE SUBMIT A VALID LINK TO AN IMAGE";
                wordsCapture.text = "";
            }
        }

        public void PostLink()
        {
            // setup the request header
            RequestHeader clientSecurityHeader = new RequestHeader
            {
                Key = clientId,
                Value = clientSecret
            };

            // setup the request header
            RequestHeader contentTypeHeader = new RequestHeader
            {
                Key = "Content-Type",
                Value = "application/json"
            };

            // validation
            if (string.IsNullOrEmpty(imageToOCR))
            {
                Debug.LogError("imageToOCR needs to be set through the inspector...");
                return;
            }

            // build image url required by Azure Vision OCR
            ImageUrl imageUrl = new ImageUrl { Url = imageToOCR };

            // send a post request
            StartCoroutine(InterviewClient.Instance.HttpPost(baseUrl, JsonUtility.ToJson(imageUrl), (r) => OnRequestComplete(r), new List<RequestHeader>
        {
            clientSecurityHeader,
            contentTypeHeader
        }));
        }

        public class ImageUrl
        {
            public string Url;
        }

        public void SubmitLink()
        {
            if (linkInputField.text != "")
            {
                imageToOCR = linkInputField.text;
                PostLink();
            }
            else
            {
                header.text = "PLEASE SUBMIT A VALID LINK TO AN IMAGE";
                wordsCapture.text = "";
            }
            
        }

        public void ChangeScene()
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

        private void Update()
        {
            if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
        }

    }
}
