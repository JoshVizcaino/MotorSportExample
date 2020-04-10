using System.Collections;
using System.Collections.Generic;
using System.Text;
using Client.Core;
using Client.Core.Models;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine;




namespace Client
{
    
    public class HeroClientExample : MonoBehaviour
    {

        [SerializeField]
        private string baseUrl = "https://superheroapi.com/api/3991167154234731/";

        [SerializeField]
        private string clientId;

        [SerializeField]
        private string clientSecret;

        [SerializeField]
        private TextMeshProUGUI heroNameText, heroIdText;

        [SerializeField]
        private TextMeshProUGUI[] heroSkillsTextArray;

        [SerializeField]
        private RawImage heroRawImage;

        private string jsonurl;

        void Start()
        {
       

        }

        void OnRequestComplete(Response response)
        {

            Debug.Log($"Status Code: {response.StatusCode}");
            Debug.Log($"Data: {response.Data}");
            Debug.Log($"Error: {response.Error}");

            if (string.IsNullOrEmpty(response.Error) && !string.IsNullOrEmpty(response.Data))
            {
                
                string skills = string.Empty;
                skills = response.Data.ToString();
                string[] words = skills.Split('"');
                heroNameText.text = $"Name: {words[11]}";
                heroIdText.text = $"ID: {words[7]}";
                for (int i = 0; i < words.Length; i++)
                {
                    Debug.Log("WORD: " + words[i] + " POWERS" + i.ToString());

                    //heroSkillsTextArray[i].text = words[i + 15];
                }
                foreach (var skill in heroSkillsTextArray)
                {
                    for (int i = 0; i < words.Length; i++)
                    {
                        skill.text = words[i + 15];
                    }
                }


            }

        }

        void OnRequestComplete2(Response response)
        {

            Debug.Log($"Status Code: {response.StatusCode}");
            Debug.Log($"Data: {response.Data}");
            Debug.Log($"Error: {response.Error}");

            if (string.IsNullOrEmpty(response.Error) && !string.IsNullOrEmpty(response.Data))
            {

                string skills = string.Empty;
                skills = response.Data.ToString();
                string[] words = skills.Split('"');
                UnityWebRequest heroSpriteRequest = UnityWebRequestTexture.GetTexture(words[15]);
                heroRawImage.texture = DownloadHandlerTexture.GetContent(heroSpriteRequest);
                heroRawImage.texture.filterMode = FilterMode.Point;

            }

        }

        public void OnRandomButtonPress()
        {
            int randomHeroIDindex = Random.Range(1, 732);

            string heroURL1 = baseUrl + randomHeroIDindex.ToString() + "/powerstats";

            string heroURL2 = baseUrl + randomHeroIDindex.ToString() + "/image";

            jsonurl = heroURL1;

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

            // send a get request
            StartCoroutine(InterviewClient.Instance.HttpGet(heroURL1, (r) => OnRequestComplete(r)));
            //StartCoroutine(InterviewClient.Instance.HttpGet(heroURL2, (r) => OnRequestComplete2(r)));
        }

        public class HeroUrl
        {
            public string Url { get; set; }
        }

    }
}

