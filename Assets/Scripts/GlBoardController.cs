using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlBoardController : MonoBehaviour
{
    // SINGLETON
    public static GlBoardController instance;

    // REFERÊNCIA GLBOARD
    public GLBoard gboard;

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
        
        gboard.SetQuantPhaseGame(4); // TUTORIAL + 3 FASES

        // NOME EXATO DE CADA FASE
        string[] phaseNames = { "Tutorial", "Fase 1", "Fase 2", "Fase 3" };

        // PERCORRENDO E REGISTRANDO AS FASES
        foreach (var name in phaseNames)
        {
            gboard.AddPhaseGame(name);
        }
    }
    
    public void setUserData(string name, string birthday, string gender)
    {
        GENDER g;
        if (gender.ToLower() == "masculino")
        {
            g = GENDER.MASCULINO;
        }
        else if (gender.ToLower() == "feminino")
        {
            g = GENDER.FEMININO;
        }
        else
        {
            g = GENDER.OUTROS;
        }
        
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