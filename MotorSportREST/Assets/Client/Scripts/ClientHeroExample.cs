using System.Collections;
using System.Collections.Generic;
using System.Text;
using Client.Core;
using Client.Core.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class ClientHeroExample : MonoBehaviour
    {
        [SerializeField]
        private string baseUrl = "https://superheroapi.com/api/3991167154234731";

        [SerializeField]
        private string clientId;

        [SerializeField]
        private string clientSecret;

        [SerializeField]
        private string imageToOCR = "";

        [SerializeField]
        private TextMeshProUGUI heroNameText, heroIdText;

        [SerializeField]
        private TextMeshProUGUI[] heroStatsTextArray;

        [SerializeField]
        private RawImage heroRawImage;

        void Start()
        {

            heroRawImage.texture = Texture2D.blackTexture;

            heroNameText.text = "";
            heroIdText.text = "";

            foreach(var heroStatsText in heroStatsTextArray)
            {
                heroStatsText.text = "";
            }


            // build image url required by Azure Vision OCR
            ImageUrl imageUrl = new ImageUrl { Url = imageToOCR };

            
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
                            words += word.text + "\n";
                        }
                    }
                }
                wordsCapture.text = words;
            }
        }

        public class ImageUrl
        {
            public string Url;
        }

        void OnGetHero()
        {

            int randomHeroIndex = Random.Range(1,731);

           

            RequestHeader contentTypeHeader = new RequestHeader
            {
                Key = "Content-Type",
                Value = "application/json"
            };

            // build image url required by Azure Vision OCR
            ImageUrl imageUrl = new ImageUrl { Url = imageToOCR };

            // send a post request
            StartCoroutine(InterviewClient.Instance.HttpPost(baseUrl, JsonUtility.ToJson(imageUrl), (r) => OnRequestComplete(r), new List<RequestHeader>
            {
                //clientSecurityHeader,
                contentTypeHeader
            }));
        }
    }
}
