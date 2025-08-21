using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlBoardController : MonoBehaviour
{

    public GLBoard gboard;
    public SceneChanger changeScene;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("Menu");
    }

    public void instantiateGlBoard(string userId)
    {
        gboard = new GLBoard("F6ypBJQWSCseX9hKNrNxsA", userId);
    }

    public void setUserData(string name, string birthday, string gender)
    {
        if (gender == "masculino" || gender == "Masculino")
        {
            gboard.SetPlayerData(name, birthday, GENDER.MASCULINO);
        }
        else if (gender == "feminino" || gender == "Feminino")
        {
            gboard.SetPlayerData(name, birthday, GENDER.FEMININO);
        }
        else
        {
            gboard.SetPlayerData(name, birthday, GENDER.OUTROS);
        }
        StartCoroutine(gboard.SEND_USER_DATA());
    }

    public async void setLastLogin()
    {
        await gboard.LOAD_USER_DATA();
        gboard.SetLastLogin(DateTime.Now);
        StartCoroutine(gboard.SEND_USER_DATA());
    }

}
