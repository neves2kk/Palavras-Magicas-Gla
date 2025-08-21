using System;
using System.Collections.Generic;

[Serializable]
public class GlaGameData
{
    public int number_phases = 4; // Tutorial + 3 fases
    public float player_minutes_in_game = 0;
    public string nivel_jogo_iniciado = "";
    public string nivel_jogo_concluido = "";
    public Dictionary<string, int> tentativas_por_fase = new Dictionary<string, int>();
    public List<GlaPhase> phases = new List<GlaPhase>();
}