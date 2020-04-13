using System.Collections;
using System.Collections.Generic;
using System.Text;
using Client.Core;
using Client.Core.Models;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;




namespace Client
{
    
    public class HeroClientExample : MonoBehaviour
    {
        [Header ("Link")]
        [SerializeField]
        private string baseUrl = "https://superheroapi.com/api/3991167154234731/";

        [Header("UI")]
        [SerializeField]
        private TextMeshProUGUI heroNameText, heroIdText;
        [SerializeField]
        private TextMeshProUGUI[] heroSkillsTextArray;
        [SerializeField]
        private RawImage heroRawImage;
        [SerializeField]
        private TMP_InputField idInputField;
        [SerializeField]
        private Button searchButton;
        [SerializeField]
        private Button randomButton;
        [SerializeField]
        private TextMeshProUGUI heroNameText2, heroIdText2;
        [SerializeField]
        private TextMeshProUGUI[] heroSkillsTextArray2;
        [SerializeField]
        private RawImage heroRawImage2;
        //[SerializeField]
        //private TMP_InputField idInputField2;
        [SerializeField]
        private Button randomButton2;
        [SerializeField]
        private TextMeshProUGUI winnerText;
        [SerializeField]
        private Button fightButton;
        [SerializeField]
        private TextMeshProUGUI heroList;

        private string heroImageUrl;
        private int count = 1;
        private List<int> skillnumsList;
        private int intel;
        private int str;
        private int speed;
        private int dur;
        private int pwr;
        private int cmb;
        private int intel2;
        private int str2;
        private int speed2;
        private int dur2;
        private int pwr2;
        private int cmb2;
        private float score1;
        private float score2;
        private bool b_hasScore = false;
        private bool b_hasScore2 = false;

        void Start()
        {
            winnerText.text = "";
        }

        void OnRequestComplete(Response response)
        {

            Debug.Log($"Status Code: {response.StatusCode}");
            Debug.Log($"Data: {response.Data}");
            Debug.Log($"Error: {response.Error}");
            
            if (string.IsNullOrEmpty(response.Error) && !string.IsNullOrEmpty(response.Data) && count == 1)
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
                int.TryParse(heroAPIresponse.intelligence, out intel);
                int.TryParse(heroAPIresponse.strength, out str);
                int.TryParse(heroAPIresponse.speed, out speed);
                int.TryParse(heroAPIresponse.durability, out dur);
                int.TryParse(heroAPIresponse.power, out pwr);
                int.TryParse(heroAPIresponse.combat, out cmb);
                float[] scores = new float[] { intel, str, speed, dur, pwr, cmb };
                for (int i = 0; i < scores.Length; i++)
                {
                    if (scores[i] <= 25)
                    {

                    }
                    else if (scores[i] > 25 && scores[i] <= 50)
                    {
                        float score;
                        score = scores[i] * Random.Range(1.2f, 1.7f);
                        scores[i] = score;
                    }
                    else if (scores[i] > 50 && scores[i] <= 75)
                    {
                        float score;
                        score = scores[i] * Random.Range(1.8f, 2.3f);
                        scores[i] = score;
                    }
                    else
                    {
                        float score;
                        score = scores[i] * Random.Range(2.3f, 2.8f);
                        scores[i] = score;
                    }
                }
                foreach (var num in scores)
                {
                    score1 += num;
                }
                b_hasScore = true;
            }
            else if (string.IsNullOrEmpty(response.Error) && !string.IsNullOrEmpty(response.Data) && count == 2)
            {   
                HeroAPIResponse heroAPIresponse = JsonUtility.FromJson<HeroAPIResponse>(response.Data);
                heroNameText2.text = $"Name: {heroAPIresponse.name}";
                heroIdText2.text = $"ID: {heroAPIresponse.id}";
                heroSkillsTextArray2[0].text = $"Intelligence: {heroAPIresponse.intelligence}";
                heroSkillsTextArray2[1].text = $"Strength: {heroAPIresponse.strength}";
                heroSkillsTextArray2[2].text = $"Speed: {heroAPIresponse.speed}";
                heroSkillsTextArray2[3].text = $"Durability: {heroAPIresponse.durability}";
                heroSkillsTextArray2[4].text = $"Power: {heroAPIresponse.power}";
                heroSkillsTextArray2[5].text = $"Combat: {heroAPIresponse.combat}";
                int.TryParse(heroAPIresponse.intelligence, out intel2);
                int.TryParse(heroAPIresponse.strength, out str2);
                int.TryParse(heroAPIresponse.speed, out speed2);
                int.TryParse(heroAPIresponse.durability, out dur2);
                int.TryParse(heroAPIresponse.power, out pwr2);
                int.TryParse(heroAPIresponse.combat, out cmb2);
                float[] scores = new float[] {intel2, str2, speed2, dur2, pwr2, cmb2};
                for (int i = 0; i < scores.Length; i++)
                {
                    if (scores[i] <= 25)
                    {

                    }
                    else if (scores[i] > 25 && scores[i] <= 50)
                    {
                        float score;
                        score = scores[i] * Random.Range(1.2f, 1.7f);
                        scores[i] = score;
                    }
                    else if (scores[i] > 50 && scores[i] <= 75)
                    {
                        float score;
                        score = scores[i] * Random.Range(1.8f, 2.3f);
                        scores[i] = score;
                    }
                    else
                    {
                        float score;
                        score = scores[i] * Random.Range(2.3f, 2.8f);
                        scores[i] = score;
                    }
                }
                foreach (var num in scores)
                {
                    score2 += num;
                }
                b_hasScore2 = true;
            };

        }

        IEnumerator OnRequestComplete2(Response response)
        {

            Debug.Log($"Status Code: {response.StatusCode}");
            Debug.Log($"Data: {response.Data}");
            Debug.Log($"Error: {response.Error}");
            
            if (string.IsNullOrEmpty(response.Error) && !string.IsNullOrEmpty(response.Data) && count == 1)
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
            else if (string.IsNullOrEmpty(response.Error) && !string.IsNullOrEmpty(response.Data) && count == 2)
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

                heroRawImage2.texture = DownloadHandlerTexture.GetContent(r);
                heroRawImage2.texture.filterMode = FilterMode.Point;
            }

        }

        public void OnRandomButtonPress()
        {
            int randomHeroIDindex = Random.Range(1, 732);

            string heroURL1 = baseUrl + randomHeroIDindex.ToString() + "/powerstats";

            string heroURL2 = baseUrl + randomHeroIDindex.ToString() + "/image";

            if (count == 1)
            {
                heroRawImage.texture = Texture2D.blackTexture;

                heroNameText.text = "Loading...";

                heroIdText.text = "#";

                foreach (var heroSkillsText in heroSkillsTextArray)
                {
                    heroSkillsText.text = "";
                }
            }
            else if (count == 2)
            {
                heroRawImage2.texture = Texture2D.blackTexture;

                heroNameText2.text = "Loading...";

                heroIdText2.text = "#";

                foreach (var heroSkillsText2 in heroSkillsTextArray2)
                {
                    heroSkillsText2.text = "";
                }
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

        public void DisplayIDs()
        {

        }

        public void WhichButton1()
        {
            count = 1;
        }
        public void WhichButton2()
        {
            count = 2;
        }

        public void Fight()
        {
            if (b_hasScore && b_hasScore2)
            {
                if (score1 > score2)
                {
                    winnerText.text = heroNameText.text + " IS THE WINNER!";
                }
                else if (score2 > score1)
                {
                    winnerText.text = heroNameText2.text + " IS THE WINNER!";
                }
                else
                {
                    winnerText.text = "DRAW";
                }
            }
            else
            {
                winnerText.text = "Please select combatants!";
            }
            
        }

        public void ChangeScene()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
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

