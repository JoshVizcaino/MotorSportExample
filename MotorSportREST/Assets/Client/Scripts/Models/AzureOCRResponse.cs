using System;


    [Serializable]
    public class AzureOCRResponse
    {
        public string language;

        public decimal textAngle;

        public string orientation;

        public AzureOCRRegion[] regions;
    }

    [Serializable]
    public class AzureOCRRegion
    {
        public string boundingBox;
        public AzureOCRLine[] lines;
    }

    [Serializable]
    public class AzureOCRLine
    {
        public string boundingBox;

        public AzureOCRWord[] words;
    }


    [Serializable]
    public class AzureOCRWord
    {
        public string boundingBox;

        public string text;
    }

