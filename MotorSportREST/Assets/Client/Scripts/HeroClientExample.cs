using System.Collections;
using System.Collections.Generic;
using System.Text;
using Client.Core;
using Client.Core.Models;
using TMPro;
using UnityEngine.UI;
using UnityEngine;



namespace Client
{
    public class HeroClientExample : MonoBehaviour
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
        private TextMeshProUGUI[] heroSkillsTextArray;

        [SerializeField]
        private RawImage heroRawImage;

        void Start()
        {

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
                HeroAPIResponse heroAPIresponse = JsonUtility.FromJson<HeroAPIResponse>(response.Data);

                heroNameText.text = $"{heroAPIresponse.name}";

                for (int i = 0; i < length; i++)
                {

                }

                string skills = string.Empty;
                foreach (var result in heroAPIresponse.results)
                {
                    foreach (var statistic in result.stats)
                    {
                        foreach (var statname in statistic.statnames)
                        {
                            skills += statname.text;
                        }
                    }
                    heroNameText.text = $"{result.name}";
                    heroIdText.text = $"{result.id}";
                }
                
                heroSkillsTextArray[0].text = skills;
            }
        }

        void OnRandomButtonPress()
        {
            int randomHeroIDindex = Random.Range(1, 732);

            heroRawImage.texture = Texture2D.blackTexture;

            heroNameText.text = "Loading...";

            heroIdText.text = "#";

            foreach (var heroSkillsText in heroSkillsTextArray)
            {
                heroSkillsText.text = "";
            }

            RequestHeader contentTypeHeader = new RequestHeader
            {
                Key = "Content-Type",
                Value = "application/json"
            };

            // send a post request
            StartCoroutine(InterviewClient.Instance.HttpPost(baseUrl, JsonUtility.ToJson(baseUrl), (r) => OnRequestComplete(r), new List<RequestHeader>
            {
                contentTypeHeader
            }));

        }

        public class ImageUrl
        {
            public string Url;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

