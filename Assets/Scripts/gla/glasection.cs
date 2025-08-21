using System;

[Serializable]
public class GlaSection
{
    public string conclusion; // "VITORIA" ou "DERROTA"
    public string status; // "COMPLETO" ou "INCOMPLETO"
    public string dateTimeStart;
    public string dateTimeFinish;
    public GlaPathPlayer path_player = new GlaPathPlayer();
}