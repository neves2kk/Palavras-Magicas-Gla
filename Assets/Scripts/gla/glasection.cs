using System;

[Serializable]
public class GlaSection
{
    public string conclusion;
    public string status;
    public string dateTimeStart;
    public string dateTimeFinish;
    public GlaPathPlayer path_player = new GlaPathPlayer();
}