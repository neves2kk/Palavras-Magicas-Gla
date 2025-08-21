using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManagerController : MonoBehaviour
{

    public static UiManagerController instance;
    public GameObject loginUI;
    public GameObject RegisterUI;
    public GameObject MenuPrincipal;

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // Instancia a classe UiManagerController
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
    }


    public void loginScreen()
    {
        Debug.Log("Exibindo Login UI");
        loginUI.SetActive(true);
        RegisterUI.SetActive(false);
        MenuPrincipal.SetActive(false);
    }

    public void registerScreen()
    {
        Debug.Log("Exibindo Register UI");
        loginUI.SetActive(false);
        RegisterUI.SetActive(true);
        MenuPrincipal.SetActive(false);
    }

    public void MenuPrincipalScreen()
    {
        Debug.Log("Exibindo Menu Principal");
        loginUI.SetActive(false);
        RegisterUI.SetActive(false);
        MenuPrincipal.SetActive(true);
    }
}
