using System;

public class HeroAPIResponse
{
    public HeroResponse[] response;
}

[Serializable]
public class HeroResponse
{
    public string name;

    public string id;

    public string intelligence;

    public string strength;

    public string speed;

    public string durability;

    public string power;

    public string combat;
}



