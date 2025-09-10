using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlBoardController : MonoBehaviour
{
    public static GlBoardController instance;
    public GLBoard gboard;

    private Dictionary<string, string> phaseNameMapping = new Dictionary<string, string>
    {
        { "Tutorial", "Tutorial - Infinitivo" },
        { "Fase 1", "Fase 1 - Presente indicativo" },
        { "Fase 2", "Fase 2 - Preterito indicativo" },
        { "Fase 3", "Fase 3 - Futuro indicativo" }
    };

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void instantiateGlBoard(string userId)
    {
        gboard = new GLBoard("F6ypBJQWSCseX9hKNrNxsA", userId);
        InitializeGlaPhases();
    }

    private void InitializeGlaPhases()
    {
        if (gboard == null) return;
        
        gboard.SetQuantPhaseGame(phaseNameMapping.Count);

        foreach (var analyticsName in phaseNameMapping.Values)
        {
            gboard.AddPhaseGame(analyticsName);
        }
        
        StartCoroutine(gboard.SEND_USER_DATA());
    }

    public string GetAnalyticsPhaseName(string sceneName)
    {
        if (phaseNameMapping.ContainsKey(sceneName))
        {
            return phaseNameMapping[sceneName];
        }
        return sceneName;
    }
    
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