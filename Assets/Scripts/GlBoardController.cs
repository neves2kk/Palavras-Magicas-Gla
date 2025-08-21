using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlBoardController : MonoBehaviour
{
    // --- NOVO: Singleton Instance ---
    public static GlBoardController instance;

    public GLBoard gboard;
    public SceneChanger changeScene;

    // --- NOVO: Variáveis de Captura de Dados ---
    private GlaGameData gameData;
    private GlaSection currentSection;
    private GlaPhase currentPhase;
    private float sessionStartTime;


    void Awake()
    {
        // --- NOVO: Lógica de Singleton ---
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            // SceneManager.LoadScene("Menu"); // Movido para o Start para evitar carregamento duplo
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // --- NOVO: Start é mais seguro para carregar a cena inicial ---
    void Start()
    {
        // Garante que o menu só seja carregado uma vez, no início do jogo.
        if (SceneManager.GetActiveScene().name == "InitialLoadingScene") // Supondo que você tenha uma cena de carregamento inicial
        {
             SceneManager.LoadScene("Menu");
        }
    }

    public void instantiateGlBoard(string userId)
    {
        gboard = new GLBoard("F6ypBJQWSCseX9hKNrNxsA", userId);
        
        // --- NOVO: Inicializa a estrutura de dados GLA ---
        InitializeGlaData();
    }

    // --- NOVO: Métodos de Captura de Dados ---

    private void InitializeGlaData()
    {
        gameData = new GlaGameData();
        
        // Pre-popula a estrutura de fases
        string[] phaseNames = { "Tutorial", "Fase 1", "Fase 2", "Fase 3" }; // Adapte se os nomes das suas cenas forem diferentes
        foreach (var name in phaseNames)
        {
            gameData.phases.Add(new GlaPhase { phase_id = name });
            gameData.tentativas_por_fase[name] = 0;
        }
    }

    public void StartSection(string phaseId)
    {
        if (gboard == null) return; 

        currentPhase = gameData.phases.Find(p => p.phase_id == phaseId);
        if (currentPhase == null)
        {
            Debug.LogError("GLA: Fase '" + phaseId + "' não encontrada. Verifique se o nome da cena corresponde aos nomes em InitializeGlaData.");
            return;
        }
        
        gameData.nivel_jogo_iniciado = phaseId;
        gameData.tentativas_por_fase[phaseId]++;
        
        currentSection = new GlaSection();
        currentSection.dateTimeStart = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        currentSection.status = "INCOMPLETO";

        sessionStartTime = Time.time;
    }

    public void TrackCorrectWord(string word)
    {
        if (currentSection != null)
        {
            currentSection.path_player.palavras_formadas_corretas.Add(word);
        }
    }

    public void TrackIncorrectWord(string word)
    {
        if (currentSection != null)
        {
            currentSection.path_player.palavras_formadas_incorretas.Add(word);
        }
    }

    public void EndSection(string conclusion)
    {
        if (gboard == null || currentSection == null) return;

        currentSection.conclusion = conclusion;
        currentSection.dateTimeFinish = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        currentSection.status = "COMPLETO";
        
        if(conclusion == "VITORIA")
        {
            gameData.nivel_jogo_concluido = currentPhase.phase_id;
        }

        currentPhase.sections.Add(currentSection);

        float sessionDurationMinutes = (Time.time - sessionStartTime) / 60.0f;
        gameData.player_minutes_in_game += sessionDurationMinutes;

        currentSection = null;
        
        SendData();
    }

    private void SendData()
    {
        string gameDataJson = JsonUtility.ToJson(gameData, true); // O 'true' formata o JSON para ser mais legível
        gboard.SetCustomReport(gameDataJson);
        StartCoroutine(gboard.SEND_USER_DATA());
        Debug.Log("GLA: Dados enviados.\n" + gameDataJson);
    }
    
    // --- SEUS MÉTODOS ORIGINAIS (sem alterações) ---
    public void setUserData(string name, string birthday, string gender)
    {
        GENDER g;
        if (gender.ToLower() == "masculino") g = GENDER.MASCULINO;
        else if (gender.ToLower() == "feminino") g = GENDER.FEMININO;
        else g = GENDER.OUTROS;
        
        gboard.SetPlayerData(name, birthday, g);
        StartCoroutine(gboard.SEND_USER_DATA());
    }

    public async void setLastLogin()
    {
        await gboard.LOAD_USER_DATA();
        gboard.SetLastLogin(DateTime.Now);
        StartCoroutine(gboard.SEND_USER_DATA());
    }
}