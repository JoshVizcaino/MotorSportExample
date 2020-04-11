using System;

[Serializable]
public class HeroAPIResponse
{
    public string name;

    public string id;

    public string intelligence;

    public string strength;

    public string speed;

    public string durability;

    public string power;

    public string combat;

    public string url;
}



//powerstats json example
//{
//    "response": "success",
//    "id": "150",
//    "name": "Captain Atom",
//    "intelligence": "81",
//    "strength": "93",
//    "speed": "83",
//    "durability": "90",
//    "power": "100",
//    "combat": "80"
//}

// image json example    
//{
//    "response": "success",
//    "id": "150",
//    "name": "Captain Atom",
//    "url": "https://www.superherodb.com/pictures2/portraits/10/100/1007.jpg"
//}



