using System;

public class HeroAPIResponse
{
    public HeroResponseResults[] results;
}

[Serializable]
public class HeroResponseResults
{
    public string name;

    public string id;

    public HeroResponsePowerstats[] stats;
}

[Serializable]
public class HeroResponsePowerstats
{
    public HeroResponseStatNames[] statnames;
}

[Serializable]
public class HeroResponseStatNames
{
    public string text;
}


