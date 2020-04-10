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
        private TextMeshProUGUI[] heroSkillsText;

        [SerializeField]
        private RawImage heroRawImage;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

