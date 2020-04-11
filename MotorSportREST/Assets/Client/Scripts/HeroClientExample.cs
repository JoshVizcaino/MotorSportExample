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

        private string heroImageUrl;

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
                HeroAPIResponse heroAPIresponse = JsonUtility.FromJson<HeroAPIResponse>(response.Data);
                heroNameText.text = $"Name: {heroAPIresponse.name}";
                heroIdText.text = $"ID: {heroAPIresponse.id}";
                heroSkillsTextArray[0].text = $"Intelligence: {heroAPIresponse.intelligence}";
                heroSkillsTextArray[1].text = $"Strength: {heroAPIresponse.strength}";
                heroSkillsTextArray[2].text = $"Speed: {heroAPIresponse.speed}";
                heroSkillsTextArray[3].text = $"Durability: {heroAPIresponse.durability}";
                heroSkillsTextArray[4].text = $"Power: {heroAPIresponse.power}";
                heroSkillsTextArray[5].text = $"Combat: {heroAPIresponse.combat}";


            }

        }

        IEnumerator OnRequestComplete2(Response response)
        {

            Debug.Log($"Status Code: {response.StatusCode}");
            Debug.Log($"Data: {response.Data}");
            Debug.Log($"Error: {response.Error}");

            if (string.IsNullOrEmpty(response.Error) && !string.IsNullOrEmpty(response.Data))
            {
                HeroAPIResponse heroAPIresponse = JsonUtility.FromJson<HeroAPIResponse>(response.Data);              
                string url = heroAPIresponse.url;

                UnityWebRequest r = UnityWebRequestTexture.GetTexture(url);
                yield return r.SendWebRequest();

                if (r.isNetworkError || r.isHttpError)
                {
                    Debug.Log(r.error);
                    yield break;
                }

                heroRawImage.texture = DownloadHandlerTexture.GetContent(r);
                heroRawImage.texture.filterMode = FilterMode.Point;

            }

        }

        public void OnRandomButtonPress()
        {
            int randomHeroIDindex = Random.Range(1, 732);

            string heroURL1 = baseUrl + randomHeroIDindex.ToString() + "/powerstats";

            string heroURL2 = baseUrl + randomHeroIDindex.ToString() + "/image";


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
            StartCoroutine(InterviewClient.Instance.HttpPost(heroURL1, JsonUtility.ToJson(heroURL1),(r) => OnRequestComplete(r)));
            StartCoroutine(InterviewClient.Instance.HttpPost(heroURL2,JsonUtility.ToJson(heroURL2), (r) => StartCoroutine(OnRequestComplete2(r))));
        }

        public class HeroUrl
        {
            public string Url { get; set; }
        }

    }
}

